using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;
using CommonUsages = UnityEngine.XR.CommonUsages;

public class PlayerControllerScript : MonoBehaviour
{
    public float speed = 3.0f;
    public float gravity = 9.81f;
    public Transform cameraTransform;

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;

    private XRNode inputSource;
    private Vector2 inputAxis;
    private bool isVR;

    private InputAction moveAction;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();

        // Initialize the move action
        moveAction = new InputAction(type: InputActionType.Value, binding: "<Gamepad>/leftStick");
        moveAction.AddCompositeBinding("2DVector")
            .With("Up", "<Keyboard>/w")
            .With("Down", "<Keyboard>/s")
            .With("Left", "<Keyboard>/a")
            .With("Right", "<Keyboard>/d");
        moveAction.Enable();

        // Check if the application is running in VR mode
        isVR = XRSettings.isDeviceActive;
        if (isVR)
        {
            inputSource = XRNode.RightHand;
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
    }
}
