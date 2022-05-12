using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeMoving : MonoBehaviour
{
    private Transform CubeTransform;
    public float speed = 1.3f;
    void Start()
    {
        CubeTransform = GetComponent<Transform>();
    }

    void Update()
    {
        CubeTransform.position = new Vector3 (CubeTransform.position.x, CubeTransform.position.y, CubeTransform.position.z - (Time.deltaTime * speed));
        CubeTransform.rotation = Quaternion.identity;
    }
}
