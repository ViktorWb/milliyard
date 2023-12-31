using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniverseBuilder : MonoBehaviour
{
    public GameObject player;
    public GameObject planet;

    void Start()
    {
        Instantiate(player);
        Instantiate(planet, new Vector3(0, 2, 0), Quaternion.identity);
        Instantiate(planet, new Vector3(0, -2, 0), Quaternion.identity);
    }
}
