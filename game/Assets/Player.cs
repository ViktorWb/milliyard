using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public float acceleration = 10;
    public float angularVelocity = 2;
    public float torque = 0.3f;

    Rigidbody rigidBody;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void HandleMove()
    {
        Vector3 force = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            force += gameObject.transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            force -= gameObject.transform.forward;
        }
        if (Input.GetKey(KeyCode.D))
        {
            force += gameObject.transform.right;
        }
        if (Input.GetKey(KeyCode.A))
        {
            force -= gameObject.transform.right;
        }
        if (Input.GetKey(KeyCode.Space))
        {
            force += gameObject.transform.up;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            force -= gameObject.transform.up;
        }
        rigidBody.AddForce(acceleration * force);
    }

    void HandleRotate()
    {
        float x = (Input.mousePosition.x - Screen.width / 2) / Screen.width;
        float y = (Input.mousePosition.y - Screen.height / 2) / Screen.height;

        Vector3 right = rigidBody.rotation * Vector3.right;
        Vector3 up = rigidBody.rotation * Vector3.up;

        Vector3 targetAngularVelocity = up * x - right * y;
        if (Input.GetKey(KeyCode.Q))
        {
            Vector3 forward = rigidBody.rotation * Vector3.forward;
            targetAngularVelocity += forward;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            Vector3 forward = rigidBody.rotation * Vector3.forward;
            targetAngularVelocity -= forward;
        }

        Vector3 torqueTowardsTarget = targetAngularVelocity * angularVelocity - rigidBody.angularVelocity;
        if (torqueTowardsTarget.magnitude > 1)
        {
            torqueTowardsTarget = torqueTowardsTarget.normalized;
        }

        rigidBody.AddTorque(torqueTowardsTarget * torque);
    }

    void FixedUpdate()
    {
        HandleMove();
        HandleRotate();
    }
}
