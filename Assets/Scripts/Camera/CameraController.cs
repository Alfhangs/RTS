using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float borderSize = 1f;
    [SerializeField] private float panSpeed = 10f;
    [SerializeField] private Vector2 panLimit = new Vector2(30f, 35f);
    [SerializeField] private float scrollSpeed = 1000f;
    [SerializeField] private Vector2 scrollLimit = new Vector2(5f, 10f);
    //[SerializeField] private bool disableCameraMovement = false;

    private Vector3 initialPosition = Vector3.zero;
    private Camera myCamera = null;

    private void Start()
    {
        // Store the initial position to use it in the movement calculation
        initialPosition = transform.position;
        // Getting the camera component to update the orthographic size
        myCamera = GetComponent<Camera>();
    }

    private void Update()
    {
        // Disables the camera movement if the property is true but only in the Unity Editor
        //        if (_disableCameraMovement)
        //        {
        //#if UNITY_EDITOR
        //            return;
        //#endif
        //        }
        UpdateZoom();
        UpdatePan();
    }

    private void UpdateZoom()
    {
        // Using the Unity API to get the mouse scroll wheel movement value
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        // The scroll value now is multiplied by the scroll speed and the deltaTime
        scroll = scroll * scrollSpeed * Time.deltaTime;
        // Setting the camera orthographic size to the new zoom by adding the scroll value
        myCamera.orthographicSize += scroll;
        // Using the Clamp method to make sure the orthographic size stays between the limits
        myCamera.orthographicSize = Mathf.Clamp(myCamera.orthographicSize, scrollLimit.x, scrollLimit.y);
    }

    private void UpdatePan()
    {
        // Store the currect position so it can be modified
        Vector3 position = transform.position;

        // Checks if the mouse cursor position is within the border size
        if (Input.mousePosition.y >= Screen.height - borderSize)
        {
            position.z += panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.y <= borderSize)
        {
            position.z -= panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x >= Screen.width - borderSize)
        {
            position.x += panSpeed * Time.deltaTime;
        }
        if (Input.mousePosition.x <= borderSize)
        {
            position.x -= panSpeed * Time.deltaTime;
        }

        // Ignore the calculation if the camera did not change position
        if (position == transform.position)
        {
            return;
        }

        // Clamp both values between the pan limit also considering the initial position
        // We do not update the Y position because it does not affect the camera
        position.x = Mathf.Clamp(position.x, -panLimit.x + initialPosition.x, panLimit.x + initialPosition.x);
        position.z = Mathf.Clamp(position.z, -panLimit.y + initialPosition.z, panLimit.y + initialPosition.z);

        // Set the updated position to the camera transform
        transform.position = position;
    }
}