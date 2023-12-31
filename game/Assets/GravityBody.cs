using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Constants
{
    public static float gravityConstant = 5;
}

public static class PhysicsSettings
{
    public static float MaxForce = 25;
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
            var distance = Vector3.Distance(rigidBody.position, other.rigidBody.position);

            if (distance is 0)
            {
                continue;
            }

            force += (other.rigidBody.position - rigidBody.position) * other.mass / Mathf.Pow(distance, 2);
        }

        force = Vector3.ClampMagnitude(force, PhysicsSettings.MaxForce);

        rigidBody.AddForce(mass * Constants.gravityConstant * force);
    }
}
