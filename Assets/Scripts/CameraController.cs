using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController inst;

    [SerializeField] Transform target;

    [SerializeField] float distance = 5.0f;
    [SerializeField] float xSpeed = 120.0f;
    [SerializeField] float ySpeed = 120.0f;

    [SerializeField] float yMinLimit = -20f;
    [SerializeField] float yMaxLimit = 80f;

    float x = 0.0f;
    float y = 0.0f;

    private void Awake()
    {
        inst = this;
    }

    void Start()
    {
        Vector3 angles = transform.eulerAngles;

        x = angles.y;
        y = angles.x;
    }

    void LateUpdate()
    {
        if (target)
        {
            x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            y = ClampAngle(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);

            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }
}