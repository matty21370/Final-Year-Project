using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Destructable : MonoBehaviour
{
    [SerializeField] private GameObject[] elements;

    public void Destruct()
    {
        foreach (var element in elements)
        {
            element.GetComponent<Rigidbody>().isKinematic = false;
        }

        GetComponent<NavMeshObstacle>().enabled = false;
    }
}
