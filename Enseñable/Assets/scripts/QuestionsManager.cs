using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class QuestionsManager : MonoBehaviour
{

    [Header("References")]
    public GameObject door;
    public Material transparentMat;
    public string invisLayer;

    [Header("Values")]
    public int correctAnswers;
    public int asertedAnswers;
    public int missedAnswers;

    [Header("Correct Selection")]
    public bool A1;
    public bool A2;
    public bool A3;
    public bool A4;

    [Header("Texts")]
    public string sQ;
    public string sA1;
    public string sA2;
    public string sA3;
    public string sA4;

    [Header("Exit References")]
    public GameObject[] thingsToRestart;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addMissed()
    {
        missedAnswers++;
    }

    public void addAserted()
    {
        asertedAnswers++;
        if (asertedAnswers >= correctAnswers)
        {
            completed();
        }
    }

    public void completed()
    {
        door.GetComponent<MeshRenderer>().material = transparentMat;
        door.GetComponent<BoxCollider>().enabled = false;
        int currentLayer = LayerMask.NameToLayer(invisLayer);
        door.layer = currentLayer;
    }

    public void fetchQuestion()
    {

    }

    public void Restart()
    {
        for (int i = 0; i < thingsToRestart.Length; i++)
        {
            thingsToRestart[i].GetComponent<RestartManager>().restart();
        }
        correctAnswers = 1;
        asertedAnswers = 0;
        missedAnswers = 0;
        A1 = false;
        A2 = false;
        A3 = true;
        A4 = false;
        sQ = "";
        sA1 = "";
        sA2 = "";
        sA3 = "";
        sA4 = "";
        fetchQuestion();
    }
}
