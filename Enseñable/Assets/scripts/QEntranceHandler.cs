using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QEntranceHandler : MonoBehaviour
{
    public GameObject door;
    public GameObject freelook;
    public Material solidMat;
    public string defLayer;

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
            freelook.SetActive(true);
            door.GetComponent<BoxCollider>().enabled = true;
            door.GetComponent<MeshRenderer>().material = solidMat;
            int currentLayer = LayerMask.NameToLayer(defLayer);
            door.layer = currentLayer;
        }
    }
}
