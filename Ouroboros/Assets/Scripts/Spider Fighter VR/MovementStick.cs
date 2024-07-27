using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class MovementStick : MonoBehaviour
{
	public XRGrabInteractable grabInteractable;
	bool isReturning;
	string state = "None";
	public HingeJoint joint;
	public GameObject resetAnchor;
	public GameObject resetPosiiton;
	public GameObject rotationLever;
	public GameObject rotationLeverResetPosition;
	public GameObject rotationLeverResetAnchor;
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

		if(state == "Forward")
		{
			MoveTankForward();
		}
		else if(state == "Backward")
		{
			MoveTankBackward();
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.tag == "MoveForward")
		{
			Debug.Log("Moved Forward");
			state = "Forward";
		}
		if (other.gameObject.tag == "MoveBackward")
		{
			Debug.Log("MoveBackward");
			state = "Backward";
		}
	}

	private void OnTriggerExit(Collider other)
	{
		Debug.Log("ExitedCollision");
		state = "None";
	}

	public void MoveTankForward()
	{
		tank.transform.position = tank.transform.position + (tank.transform.forward * 4 * Time.deltaTime);
		resetLeverPosition();
		resetRotationLeverPosition();
	}

	public void MoveTankBackward()
	{
		tank.transform.position = (tank.transform.position + tank.transform.forward * -4 * Time.deltaTime);
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
		gameObject.transform.position = resetPosiiton.transform.position;
		joint.connectedAnchor = resetAnchor.transform.position;
	}

	public void resetRotationLeverPosition()
	{
		rotationLever.transform.position = rotationLeverResetPosition.transform.position;
		rotationLever.GetComponent<HingeJoint>().connectedAnchor = rotationLeverResetAnchor.transform.position;
	}
}
