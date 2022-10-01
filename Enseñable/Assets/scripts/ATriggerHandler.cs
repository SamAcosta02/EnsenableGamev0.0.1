using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ATriggerHandler : MonoBehaviour
{
    public QuestionsManager Manager;
    public int answerNumber;
    public GameObject floor;
    public Material correctMaterial;
    public Material incorrectMaterial;

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
            switch (answerNumber) {
                case 1:
                    if (Manager.A1)
                    {
                        answeredCorrect();
                    }
                    else
                    {
                        answeredIncorrect();
                    }
                    break;

                case 2:
                    if (Manager.A2)
                    {
                        answeredCorrect();
                    }
                    else
                    {
                        answeredIncorrect();
                    }
                    break;
                case 3:
                    if (Manager.A3)
                    {
                        answeredCorrect();
                    }
                    else
                    {
                        answeredIncorrect();
                    }
                    break;
                case 4:
                    if (Manager.A4)
                    {
                        answeredCorrect();
                    }
                    else
                    {
                        answeredIncorrect();
                    }
                    break;
            }
        }
    }

    public void answeredCorrect()
    {
        floor.GetComponent<MeshRenderer>().material = correctMaterial;
        Manager.addAserted();
    }

    public void answeredIncorrect()
    {
        floor.GetComponent<BoxCollider>().enabled = false;
        floor.GetComponent<MeshRenderer>().material = incorrectMaterial;
        Manager.addMissed();
    }

}
