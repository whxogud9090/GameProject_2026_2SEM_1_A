using UnityEngine;
using UnityEngine.InputSystem;

public class ThirdPersonCamera : MonoBehaviour
{
    [Header("대상 설정")]
    [SerializeField] private Transform target;
    [SerializeField] private float targetHeight = 1.4f;
    [SerializeField] private float distance = 5f;

    [Header("회전 설정")]
    [SerializeField] private float mouseSensitivity = 0.15f;
    [SerializeField] private float minimumPitch = -30f;
    [SerializeField] private float maximumPitch = 60f;

    private float yaw;
    private float pitch = 15f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (target != null)
        {
            yaw = target.eulerAngles.y;
        }
    }

    private void Update()
    {
        if (Mouse.current == null)
        {
            return;
        }

        Vector2 mouseDelta = Mouse.current.delta.ReadValue();
        yaw += mouseDelta.x * mouseSensitivity;
        pitch = Mathf.Clamp(pitch - mouseDelta.y * mouseSensitivity, minimumPitch, maximumPitch);
    }

    private void LateUpdate()
    {
        if (target == null)
        {
            return;
        }

        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0f);
        Vector3 lookTarget = target.position + Vector3.up * targetHeight;

        transform.position = lookTarget - rotation * Vector3.forward * distance;
        transform.rotation = rotation;
    }
}
