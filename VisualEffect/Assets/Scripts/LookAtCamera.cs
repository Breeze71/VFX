using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Camera cam;

    [SerializeField] private CameraMode cameraMode;
    private enum CameraMode
    {
        LookAt,
        LookAtInverted,
        CameraForward, 
        CameraForwardInverted,
    }

    private void Start() 
    {
        cam = Camera.main;
    }

    private void Update() 
    {
        switch(cameraMode)
        {
            // 面對 cam </
            case CameraMode.LookAt:
                transform.LookAt(Camera.main.transform);
                break;
                        
            // 背對 cam />
            case CameraMode.LookAtInverted:
                Vector3 dirFromCamera = transform.position - Camera.main.transform.position;
                transform.LookAt(transform.position + dirFromCamera);
                break;

            /* 非動態 */
            case CameraMode.CameraForward:
                transform.forward = cam.transform.forward;
                break;
            case CameraMode.CameraForwardInverted:
                transform.forward = - cam.transform.forward;
                break; 
        }
    }
}
