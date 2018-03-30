using UnityEngine;

public class MovingPlayer : MonoBehaviour {

    public float dragSpeed = 1;

    bool isDragging;
    Plane dragPlane;
    Vector3 dragGrabPosition;
    Vector2 lastMousePosition;

    Quaternion targetRotation = Quaternion.identity;
    Quaternion initialRotation;
    public float rotationSnapDegrees = 45;
    public float rotationSnapSpeed = 10;

    public float zoomSpeed = 1;

    Rotator rotator;

    void Start ()
	{
        dragPlane = new Plane(Vector3.up, 0);
        initialRotation = transform.rotation;
        rotator = FindObjectOfType<Rotator>();
	}

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.layer != LayerMask.NameToLayer("Interactive"))
                {
                    float grabDistance;
                    if (dragPlane.Raycast(ray, out grabDistance))
                    {
                        isDragging = true;
                        dragGrabPosition = ray.GetPoint(grabDistance);
                        rotator.activeTransformer = null;
                    }
                }
            }
        }

        if (isDragging && Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distance;
            if (dragPlane.Raycast(ray, out distance))
            {
                Vector3 newGrab = ray.GetPoint(distance);
                Vector3 delta = dragGrabPosition - newGrab;
                transform.position += delta * dragSpeed;
                Debug.DrawLine(transform.position, newGrab);
            }
        }

        if (Input.mouseScrollDelta != Vector2.zero)
        {
            transform.position += Vector3.up * Input.mouseScrollDelta.y * zoomSpeed;
        }

        if (isDragging)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            float distance;
            if (dragPlane.Raycast(ray, out distance))
            {
                dragGrabPosition = ray.GetPoint(distance);
            }
        }

        if (rotator.activeTransformer == null)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                targetRotation *= Quaternion.AngleAxis(rotationSnapDegrees, Vector3.up);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                targetRotation *= Quaternion.AngleAxis(-rotationSnapDegrees, Vector3.up);
            }
        }

        Quaternion slerpTarget = initialRotation * targetRotation;
        transform.rotation = Quaternion.Slerp(transform.rotation, slerpTarget, Time.deltaTime * rotationSnapSpeed);
    }
}