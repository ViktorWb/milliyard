using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Constants
{
    public static float gravityConstant = 5;
}

[RequireComponent(typeof(Rigidbody))]
public class GravityBody : MonoBehaviour
{
    public float mass;
    Rigidbody rigidBody;
    HashSet<GravityBody> others = new();

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        foreach (GameObject obj in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            if (obj != gameObject && obj.GetComponent<GravityBody>() is GravityBody other)
            {
                others.Add(other);
                other.others.Add(this);
            }
        }
    }

    void OnDestroy()
    {
        foreach (GameObject obj in SceneManager.GetActiveScene().GetRootGameObjects())
        {
            if (obj != gameObject && obj.GetComponent<GravityBody>() is GravityBody other)
            {
                other.others.Remove(this);
            }
        }
    }

    void FixedUpdate()
    {
        Vector3 force = Vector3.zero;
        foreach (GravityBody other in others)
        {
            force += (other.rigidBody.position - rigidBody.position) * other.mass / Mathf.Pow(Vector3.Distance(rigidBody.position, other.rigidBody.position), 2);
        }
        rigidBody.AddForce(mass * Constants.gravityConstant * force);
    }
}
