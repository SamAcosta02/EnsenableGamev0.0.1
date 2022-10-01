using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lookAtCamera: MonoBehaviour
{
    public float xOffset = 0f;
    public float yOffset = 0f;
    public float zOffset = 0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(Camera.main.transform.position, Vector3.up);
    }
}
