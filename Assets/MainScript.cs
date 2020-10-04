using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MainScript : MonoBehaviour
{

    public GameObject model1;
    public GameObject model2;


    private GameObject currentModel;
    // private bool temp = false;

    void Start()
    {
        Debug.Log("main script running");

        currentModel = Instantiate(model2, transform.position, transform.rotation) as GameObject;
        currentModel.transform.parent = transform;


        // model1.SetActive(false);
        // model2.SetActive(false);
    }

    public void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.I))
        {
            Debug.Log("Changing model");
            ChangeModel();
        }
    }

    public void ChangeModel()
    {
        if (currentModel == model1)
        {
            GameObject thisModel = Instantiate(model2, transform.position, transform.rotation) as GameObject;
            Destroy(currentModel);
            thisModel.transform.parent = transform;
            currentModel = thisModel;
        }
        else
        {
            GameObject thisModel = Instantiate(model1, transform.position, transform.rotation) as GameObject;
            Destroy(currentModel);
            thisModel.transform.parent = transform;
            currentModel = thisModel;
        }
    }
}
