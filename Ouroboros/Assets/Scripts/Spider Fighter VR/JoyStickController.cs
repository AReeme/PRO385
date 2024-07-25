using System;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class JoyStickController : MonoBehaviour
{
    public XRGrabInteractable grabInteractable;
    bool isReturning;
    public Transform topOfJoyStick;
    public Transform resetPosition;

    [SerializeField] float forwardBackTilt = 0;
    [SerializeField] float sideTilt = 0;
	[SerializeField] GameObject tank;

	private Quaternion initialRotation;
	public Vector3 rotationSpeed = new Vector3(0, 50, 0);

	public void Start()
	{
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

		forwardBackTilt = topOfJoyStick.rotation.eulerAngles.x;
        //front and back movement
        if (forwardBackTilt < 355 && forwardBackTilt > 290)
        {
            forwardBackTilt = Math.Abs(forwardBackTilt - 360);
            Debug.Log("Right" + forwardBackTilt);
			RotateRight();
        }

        else if(forwardBackTilt > 5 && forwardBackTilt < 74)
        {
            Debug.Log("Left");
			RotateLeft();
        }

        sideTilt = topOfJoyStick.rotation.eulerAngles.z;

		if (sideTilt < 355 && sideTilt > 290)
		{
			sideTilt = Math.Abs(sideTilt - 360);
			Debug.Log("Backward");
			tank.transform.position = MoveTankBackward();
		}
		else if (sideTilt > 5 && sideTilt < 74)
		{
			Debug.Log("Forward?");
			tank.transform.position = MoveTankForward();
		}
	}

    public Vector3 MoveTankForward()
    {
        Vector3 move = tank.transform.position + (tank.transform.forward * 4 * Time.deltaTime);
        return move;
    }

	public Vector3 MoveTankBackward()
	{
		Vector3 move = (tank.transform.position + tank.transform.forward * -4 * Time.deltaTime);
		return move;
	}

	public void RotateRight()
	{
		tank.transform.Rotate(rotationSpeed * Time.deltaTime);
		//return move;
	}

	public void RotateLeft()
	{
		tank.transform.Rotate((rotationSpeed * -1) * Time.deltaTime);
	}

	void OnRelease(SelectExitEventArgs args)
	{
		// Start the return process
		isReturning = true;
	}
}
