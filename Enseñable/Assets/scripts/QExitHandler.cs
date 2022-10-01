using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QExitHandler : MonoBehaviour
{
    public QuestionsManager manager;
    public GameObject freelook;
    public Transform respawnLoc;

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
            other.transform.position += respawnLoc.position - gameObject.transform.position;
        }
        manager.Restart();
    }
}
