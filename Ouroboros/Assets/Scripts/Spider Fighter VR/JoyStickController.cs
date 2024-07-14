using System;
using System.Runtime.CompilerServices;
using UnityEngine;

public class JoyStickController : MonoBehaviour
{
    public Transform topOfJoyStick;

    [SerializeField] float forwardBackTilt = 0;

    [SerializeField] float sideTilt = 0;

    // Update is called once per frame
    void Update()
    {
        forwardBackTilt = topOfJoyStick.rotation.eulerAngles.x;
        //front and back movement
        if (forwardBackTilt < 355 && forwardBackTilt > 290)
        {
            forwardBackTilt = Math.Abs(forwardBackTilt - 360);
            Debug.Log("Backward" + forwardBackTilt);
            //movement code goes here using forward back tilt as speed
        }

        else if(forwardBackTilt > 5 && forwardBackTilt < 74)
        {
            Debug.Log("Forward");
            //movement code goes here using forward back tilt as speed
        }

        sideTilt = topOfJoyStick.rotation.eulerAngles.z;
        if (sideTilt < 355 && sideTilt > 290)
        {
            sideTilt = Math.Abs(sideTilt - 360);
            Debug.Log("Right");
        }
        else if (sideTilt > 5 && sideTilt < 74)
        {
            Debug.Log("Left");
        }
    }

	private void OnTriggerStay(Collider other)
	{
		if(other.CompareTag("PlayerHand"))
        {
            transform.LookAt(other.transform.position, transform.up);
        }
	}
}
