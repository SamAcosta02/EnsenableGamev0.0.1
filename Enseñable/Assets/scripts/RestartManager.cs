using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartManager : MonoBehaviour
{
    public GameObject obj;

    public bool collider;
    public bool activeMaterial;

    public bool material;
    public Material originalMat;

    public bool Layer;
    public string originalLayer;


    // Update is called once per frame
    void Update()
    {
        
    }

    public void restart()
    {
        if (material)
        {
            gameObject.GetComponent<MeshRenderer>().material = originalMat;
        }
        if (collider)
        {
            gameObject.GetComponent<BoxCollider>().enabled = activeMaterial;
        }
        if (Layer)
        {
            int currentLayer = LayerMask.NameToLayer(originalLayer);
            gameObject.layer = currentLayer;
        }
    }
}
