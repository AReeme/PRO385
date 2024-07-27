using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit;

public class RotationStick : MonoBehaviour
{
	public XRGrabInteractable grabInteractable;
	bool isReturning;
	string state = "None";
	public HingeJoint joint;
	public GameObject movementResetAnchor;
	public GameObject movementResetPosiiton;
	public GameObject movementLever;
	public GameObject rotationLeverResetPosition;
	public GameObject rotationLeverResetAnchor;
	public GameObject MovementLookAt;
	public GameObject RotationLookAt;
	public Transform lever;
	[SerializeField] GameObject tank;
	private Quaternion initialRotation;
	public Vector3 rotationSpeed = new Vector3(0, 50, 0);


	public void Start()
	{
		joint = gameObject.GetComponent<HingeJoint>();
		grabInteractable = GetComponent<XRGrabInteractable>();
		if (grabInteractable != null)
		{
			grabInteractable.selectExited.AddListener(OnRelease);
		}
		initialRotation = transform.rotation;
	}

	// Update is called once per frame
	void Update()
	{

		if (isReturning)
		{
			// Smoothly interpolate the lever back to its initial position
			transform.rotation = Quaternion.Lerp(transform.rotation, initialRotation, Time.deltaTime * 1000);

			// Stop returning when the lever is close to the initial rotation
			if (Quaternion.Angle(transform.rotation, initialRotation) < 0.1f)
			{
				transform.rotation = initialRotation;
				isReturning = false;
			}
		}

		if (state == "RL")
		{
			RotateLeft();
		}
		else if (state == "RR")
		{
			RotateRight();
		}

		updateLeverRotation();
	}

	private void OnTriggerStay(Collider other)
	{
		Debug.Log("rotate entered Collision");
		if (other.gameObject.tag == "RL")
		{
			Debug.Log("rotate left");
			state = "RL";
		}
		if (other.gameObject.tag == "RR")
		{
			Debug.Log("rotate right");
			state = "RR";
		}
	}

	private void OnTriggerExit(Collider other)
	{
		Debug.Log("ExitedCollision");
		state = "None";
	}

	public void RotateRight()
	{
		tank.transform.Rotate(rotationSpeed * Time.deltaTime);
		resetLeverPosition();
		resetRotationLeverPosition();
	}

	public void RotateLeft()
	{
		tank.transform.Rotate((rotationSpeed * -1) *  Time.deltaTime);
		resetLeverPosition();
		resetRotationLeverPosition();
	}

	void OnRelease(SelectExitEventArgs args)
	{
		// Start the return process
		resetLeverPosition();
		resetRotationLeverPosition();
		isReturning = true;
	}

	public void resetLeverPosition()
	{
		movementLever.transform.position = movementResetPosiiton.transform.position;
		//movementLever.transform.rotation = rotationLeverResetPosition.transform.rotation;
		movementLever.GetComponent<HingeJoint>().connectedAnchor = movementResetAnchor.transform.position;
	}

	public void resetRotationLeverPosition()
	{
		gameObject.transform.position = rotationLeverResetPosition.transform.position;
		//movementLever.transform.rotation = rotationLeverResetPosition.transform.rotation;
		joint.connectedAnchor = rotationLeverResetAnchor.transform.position;
	}

	public void updateLeverRotation()
	{
		lever.localRotation = Quaternion.identity;
	}
}
