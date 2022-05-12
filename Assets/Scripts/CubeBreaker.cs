using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBreaker : MonoBehaviour
{
    public int Score;
    private GameObject Cube;
    public bool Wall = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CubeComponent")
        {
            Cube = other.transform.parent.gameObject;
            var CubeMove = Cube.GetComponent<CubeMoving>();
            CubeMove.speed = 0;
            var Kilka = Cube.GetComponent<Rigidbody>();
            Kilka.useGravity = true;
            if (!Wall)
            {
                Destroy(Cube, 0.5f);
                Score++;
            }
            else
            {
                Destroy(Cube, 0.1f);
            }
            
        }
    }
    
}
