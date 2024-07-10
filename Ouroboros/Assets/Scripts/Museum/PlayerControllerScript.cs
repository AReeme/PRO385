using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using UnityEngine.EventSystems;
using CommonUsages = UnityEngine.XR.CommonUsages;
using System.Collections.Generic;

public class PlayerControllerScript : MonoBehaviour
{
    public float speed = 3.0f;
    public float gravity = 9.81f;
    public Transform cameraTransform; // Reference to the camera transform

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;

    private XRNode inputSource;
    private Vector2 inputAxis;
    private bool isVR;

    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction interactAction;

    private Camera mainCamera;
    private PointerEventData pointerEventData;
    private EventSystem eventSystem;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        mainCamera = Camera.main;
        eventSystem = EventSystem.current;

        // Initialize the move action
        moveAction = new InputAction(type: InputActionType.Value, binding: "<Gamepad>/leftStick");
        moveAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");
        moveAction.Enable();

        // Initialize the look action
        lookAction = new InputAction(type: InputActionType.Value, binding: "<Mouse>/delta");
        lookAction.Enable();

        // Initialize the interact action
        interactAction = new InputAction(binding: "<Mouse>/leftButton");
        interactAction.Enable();

        // Check if the application is running in VR mode
        isVR = XRSettings.isDeviceActive;
        if (isVR)
        {
            inputSource = XRNode.LeftHand; // or RightHand based on your preference
        }
    }

    void Update()
    {
        if (isVR)
        {
            UpdateVR();
        }
        else
        {
            UpdateNonVR();
        }
    }

    void UpdateVR()
    {
        UnityEngine.XR.InputDevice device = InputDevices.GetDeviceAtXRNode(inputSource);
        device.TryGetFeatureValue(CommonUsages.primary2DAxis, out inputAxis);

        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;

        forward.y = 0;
        right.y = 0;

        moveDirection = forward * inputAxis.y + right * inputAxis.x;
        moveDirection *= speed;
        moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);
    }

    void UpdateNonVR()
    {
        inputAxis = moveAction.ReadValue<Vector2>();

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0;
        right.y = 0;

        moveDirection = forward * inputAxis.y + right * inputAxis.x;
        moveDirection *= speed;
        moveDirection.y -= gravity * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);

        // Handle camera rotation
        Vector2 lookInput = lookAction.ReadValue<Vector2>();
        float lookSpeed = 0.1f;
        cameraTransform.Rotate(Vector3.up, lookInput.x * lookSpeed);
        cameraTransform.Rotate(Vector3.left, lookInput.y * lookSpeed);

        // Handle UI interaction
        HandleUIInteraction();
    }

    void HandleUIInteraction()
    {
        pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Mouse.current.position.ReadValue();

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerEventData, results);

        foreach (RaycastResult result in results)
        {

            if (interactAction.triggered)
            {
                ExecuteEvents.Execute(result.gameObject, pointerEventData, ExecuteEvents.pointerClickHandler);
            }
        }
    }
}
