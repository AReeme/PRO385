using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    public float lookSpeed = 2.0f;
    private Vector2 rotation = Vector2.zero;

    private InputAction lookAction;

    void Awake()
    {
        // Initialize the look action
        lookAction = new InputAction(type: InputActionType.Value, binding: "<Mouse>/delta");
        lookAction.Enable();
    }

    void Update()
    {
        Vector2 lookInput = lookAction.ReadValue<Vector2>();
        rotation.x += lookInput.x * lookSpeed;
        rotation.y -= lookInput.y * lookSpeed;
        rotation.y = Mathf.Clamp(rotation.y, -90f, 90f);

        transform.localRotation = Quaternion.Euler(rotation.y, rotation.x, 0);
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
