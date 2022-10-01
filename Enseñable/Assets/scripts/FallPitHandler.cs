using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallPitHandler : MonoBehaviour
{
    public Transform respawnLoc;
    public GameObject freelook;
    public float yAdd;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            freelook.SetActive(false);
            other.transform.position = respawnLoc.position + new Vector3(0f, yAdd, 0f);
        }
    }
}
