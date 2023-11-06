using Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{

    [Header("Camera Sensitivity")]
    [SerializeField] public float xSensitivity = 100f;
    [SerializeField] public float ySensitivity = 2f;

    [Header("Camera Reference")]
    [SerializeField] public GameObject cameraObject;

    private CinemachineFreeLook freelook;

    private void Awake()
    {
        // Subscribe to Right Mouse Click Actions
        InputManager.Instance.inputActions.NormalMode.RightMouseClick.started += EnableCameraMovement;
        InputManager.Instance.inputActions.NormalMode.RightMouseClick.canceled += DisableCameraMovement;
    }

    private void OnDestroy()
    {
        // Unsubscribe from Right Mouse Click Actions
        InputManager.Instance.inputActions.NormalMode.RightMouseClick.started -= EnableCameraMovement;
        InputManager.Instance.inputActions.NormalMode.RightMouseClick.canceled -= DisableCameraMovement;
    }

    void Start()
    {
        freelook = cameraObject.GetComponent<CinemachineFreeLook>();
        if (freelook != null)
        {
            freelook.m_XAxis.m_MaxSpeed = 0;
            freelook.m_YAxis.m_MaxSpeed = 0;
        }
    }

    void EnableCameraMovement(InputAction.CallbackContext context)
    {
        freelook.m_XAxis.m_MaxSpeed = xSensitivity;
        freelook.m_YAxis.m_MaxSpeed = ySensitivity;
    }

    void DisableCameraMovement(InputAction.CallbackContext context)
    {
        freelook.m_XAxis.m_MaxSpeed = 0;
        freelook.m_YAxis.m_MaxSpeed = 0;
    }
}