using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
public class ButtonFollowVisual : MonoBehaviour
{
    private Vector3 offset;
    private Transform pokeAttachTransform;
    public Vector3 localAxis;
    private Vector3 initialLocalPos;
    public float resetSpeed = 5;
	public Vector3 rotationSpeed = new Vector3(0, 50, 0);
	public float followAngleThreshold = 45;
    [SerializeField] GameObject tank;
    private TankController tankController;
    public bool moveFWD;
    public bool moveBWD;
    public bool rotateRight;
    public bool rotateLeft;
    public bool fire;
    public bool shield;


    private bool freeze = false;

    private XRBaseInteractable interactable;
    bool isFollowing = false;
    public Transform visualTarget;
    
    // Start is called before the first frame update
    void Start()
    {
        initialLocalPos = visualTarget.localPosition;
        interactable = GetComponent<XRBaseInteractable>();
        interactable.hoverEntered.AddListener(Follow);
        interactable.hoverExited.AddListener(ResetButton);
        interactable.selectEntered.AddListener(Freeze);
        tankController = tank.GetComponent<TankController>();
    }

    public void Follow(BaseInteractionEventArgs hover)
    {
        if(hover.interactorObject is XRPokeInteractor)
        {
            XRPokeInteractor interactor = (XRPokeInteractor)hover.interactorObject;
            isFollowing = true;
            freeze = false;

            pokeAttachTransform = interactor.attachTransform;
            offset = visualTarget.position - pokeAttachTransform.position;

            float pokeAngle = Vector3.Angle(offset, visualTarget.TransformDirection(localAxis));
            if(pokeAngle < followAngleThreshold)
            {
                isFollowing = true;
                freeze = false;
            }
        }
    }

	public void ResetButton(BaseInteractionEventArgs hover)
	{
		if(hover.interactorObject is XRPokeInteractor)
        {
            isFollowing = false;
            freeze = false;
        }
	}

    public void Freeze(BaseInteractionEventArgs hover)
    {
		if (hover.interactorObject is XRPokeInteractor)
		{
			freeze = true;
		}
	}

	// Update is called once per frame
	void Update()
    {
        //if (freeze) return;

        if(isFollowing)
        {
            Vector3 localTargetPosiiton = visualTarget.InverseTransformPoint(pokeAttachTransform.position + offset);
            Vector3 contraintLocalTargetPosition = Vector3.Project(localTargetPosiiton, localAxis);

            visualTarget.position = visualTarget.TransformPoint(contraintLocalTargetPosition);
            if(moveFWD == true)
            {
			    tank.transform.position = tank.transform.position + (tank.transform.forward * 4 * Time.deltaTime);
            }
            else if(moveBWD == true)
            {
				tank.transform.position = (tank.transform.position + tank.transform.forward * -4 * Time.deltaTime);
			}
            else if(rotateRight == true)
            {
				tank.transform.Rotate(rotationSpeed * Time.deltaTime);
			}
			else if (rotateLeft == true)
			{
				tank.transform.Rotate((rotationSpeed * -1) * Time.deltaTime);
			}
            else if(fire == true)
            {
                tankController.Fire();
            }
			else if (shield == true)
			{
				tankController.SpawnShield();
			}
		}
        else
        {
            visualTarget.localPosition = Vector3.Lerp(visualTarget.localPosition, initialLocalPos, Time.deltaTime * resetSpeed);
        }
    }
}
