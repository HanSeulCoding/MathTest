using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCube : MonoBehaviour
{
    public GameObject targetCube;
    public float moveSpeed;
    public Vector3 direction;
    // Start is called before the first frame update
    void Start()
    {
        direction = new Vector3(Mathf.Cos(180.0f/Mathf.PI), Mathf.Sin(180.0f / Mathf.PI), Mathf.Sin(180.0f / Mathf.PI));
        direction = direction.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(targetCube.transform.position, Vector3.up, Time.fixedDeltaTime * moveSpeed);    
    }

    //void 
}
