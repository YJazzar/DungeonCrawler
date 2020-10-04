using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MainScript : MonoBehaviour
{

    public GameObject model0;
    public GameObject model1;

    private int model = 1;

    private GameObject currentModel;
    // private bool temp = false;

    void Start()
    {
        Debug.Log("main script running");

        currentModel = Instantiate(model0, transform.position, transform.rotation) as GameObject;
        currentModel.transform.parent = transform;


        // model1.SetActive(false);
        // model2.SetActive(false);
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            model++; 
            model %= 2;

            ChangeModel();
        }
    }

    public void ChangeModel()
    {
        Debug.Log("model = " + model);
        if (model == 0)
        {
            Debug.Log("Changing to model 1");
            GameObject thisModel = Instantiate(model1, transform.position, transform.rotation) as GameObject;
            Destroy(currentModel);
            thisModel.transform.parent = transform;
            currentModel = thisModel;
        }
        else if (model == 1)
        {
            Debug.Log("Changing to model 0");
            GameObject thisModel = Instantiate(model0, transform.position, transform.rotation) as GameObject;
            Destroy(currentModel);
            thisModel.transform.parent = transform;
            currentModel = thisModel;
        }
        else 
        {
            Debug.Log("ERROR: mode = " + model);
        }
    }
}
