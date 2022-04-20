using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBreaker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Cube")
        {
            other.gameObject.tag = "Destroy";
            Destroy(other.gameObject, 0.1f);
        }
    }
}
