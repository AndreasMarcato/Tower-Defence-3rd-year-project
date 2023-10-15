using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour
{
    public Transform cameraBounds;
    public float panSpeed = 10f;
    public float zoomSpeed = 5f;
    public float minZoom = 5f;
    public float maxZoom = 20f;
    public float rotationSpeed = 90f; // Degrees per second for rotation

    private Vector3 panLimit;
    [SerializeField] private Camera mainCamera;

    private Quaternion targetRotation;
    private bool isRotating = false;

    private Vector3 lastMousePosition;




    public static CameraManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

    }

    private void Start()
    {
        panLimit = cameraBounds.GetComponent<BoxCollider>().bounds.extents;
        targetRotation = transform.rotation;
    }


    private void Update()
    {
        // Camera Panning
        if (Input.GetMouseButtonDown(0))
        {
            lastMousePosition = Input.mousePosition;
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 deltaMouse = Input.mousePosition - lastMousePosition;
            Vector3 panDirection = new Vector3(-deltaMouse.x, 0, -deltaMouse.y);
            panDirection = transform.TransformDirection(panDirection); // Convert to world space

            Vector3 newPosition = transform.position + panDirection * panSpeed * Time.deltaTime;
            newPosition.x = Mathf.Clamp(newPosition.x, -panLimit.x, panLimit.x);
            newPosition.z = Mathf.Clamp(newPosition.z, -panLimit.z, panLimit.z);
            transform.position = newPosition;

            lastMousePosition = Input.mousePosition;
        }



        // Camera Zooming
        float zoomInput = Input.GetAxis("Mouse ScrollWheel");
        float newZoom = mainCamera.fieldOfView - zoomInput * zoomSpeed;
        mainCamera.fieldOfView = Mathf.Clamp(newZoom, minZoom, maxZoom);

        // Camera Rotation
        if (Input.GetMouseButtonDown(1) && !isRotating) // Right mouse button for rotation
        {
            targetRotation *= Quaternion.Euler(Vector3.up * -90); // Set the target rotation to rotate counterclockwise
            StartCoroutine(RotateCamera(targetRotation, 0.7f));
        }
        if (Input.GetMouseButtonDown(2) && !isRotating) // Middle mouse button for rotation
        {
            targetRotation *= Quaternion.Euler(Vector3.up * 90); // Set the target rotation to rotate clockwise
            StartCoroutine(RotateCamera(targetRotation, 0.7f));
        }
    }
  

    private IEnumerator RotateCamera(Quaternion targetRotation, float duration)
    {
        isRotating = true;
        float startTime = Time.time;
        Quaternion initialRotation = transform.rotation;

        while (Time.time - startTime < duration)
        {
            float t = (Time.time - startTime) / duration;
            transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, t);
            yield return null;
        }

        transform.rotation = targetRotation;
        isRotating = false;
    }

}