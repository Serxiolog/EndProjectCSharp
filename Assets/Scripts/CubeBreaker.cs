using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBreaker : MonoBehaviour
{
    public int Score;
    private GameObject Cube;
    public bool Wall = false;
    public int SwordID = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "CubeComponent")
        {
            
            Cube = other.transform.parent.gameObject;
            var CubeMove = Cube.GetComponent<CubeMoving>();
            int CubeColor = CubeMove.Color;
            if (CubeColor == 2 || CubeColor == SwordID || Wall)
            {
                CubeMove.speed = 0;
                var Kilka = Cube.GetComponent<Rigidbody>();
                Kilka.useGravity = true;
                Score++;
                if (!Wall)
                {
                    Destroy(Cube, 0.5f);
                }
                else
                {
                    Destroy(Cube, 0.1f);
                }
            }
        }
    }
    
}
