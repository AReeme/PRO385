using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class CameraController : MonoBehaviour
{
    public float lookSpeed = 2.0f;
    private Vector2 rotation = Vector2.zero;

    private XRNode inputSource;

    private InputAction lookAction;
    private bool isVR;

    void Awake()
    {
        // Initialize the look action
        lookAction = new InputAction(type: InputActionType.Value, binding: "<Mouse>/delta");
        lookAction.Enable();

        // Check if the application is running in VR mode
        isVR = XRSettings.isDeviceActive;
    }

    void Update()
    {
        if (!isVR)
        {

            Vector2 lookInput = lookAction.ReadValue<Vector2>();
            rotation.x += lookInput.x * lookSpeed;
            rotation.y -= lookInput.y * lookSpeed;
            rotation.y = Mathf.Clamp(rotation.y, -90f, 90f);

            transform.localRotation = Quaternion.Euler(rotation.y, rotation.x, 0);
        }
        else
        {
            
        }
    }

    void OnEnable()
    {
        lookAction.Enable();
    }

    void OnDisable()
    {
        lookAction.Disable();
    }
}
