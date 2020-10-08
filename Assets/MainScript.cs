using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;



public class MainScript : MonoBehaviour
{
    public List<GameObject> models;

    private int currModelNumber;

    private GameObject currentModel;


    // Keymappings used for different actions: 
    KeyCode changeModel = KeyCode.Space;
    KeyCode rotateRight = KeyCode.E;
    KeyCode rotateLeft = KeyCode.Q;


    float tiltAngle = 0.0f;
    float smooth = 5.0f;
    bool rotatingLeft;
    bool rotatingRight;

    void Start()
    {
        Debug.Log("main script running");

        currModelNumber = 0;

        loadPlayerModels();

        ChangeModel();
    }

    public void Update()
    {
        // Check if we need to change the model of the player
        if (Input.GetKeyDown(changeModel))
        {
            currModelNumber = (++currModelNumber) % models.Count;
            ChangeModel();
        }

        handleRotation();

    }

    // Rotates the player model as needed
    public void handleRotation() {
        // Left rotation
        if (Input.GetKeyDown(rotateLeft))
        {
            rotatingLeft = true;
            rotatingRight = false; 
        }

        if (Input.GetKeyUp(rotateLeft))
        {
            rotatingLeft = false;
            rotatingRight = false;
        }

        // Right rotation
        if (Input.GetKeyDown(rotateRight))
        {
            rotatingLeft = false;
            rotatingRight = true;
        }

        if (Input.GetKeyUp(rotateRight))
        {
            rotatingLeft = false;
            rotatingRight = false;
        }

        // Rotate to the left if needed
        if (rotatingLeft)
        {
            // Rotate the cube by converting the angles into a quaternion.
            Quaternion target = Quaternion.Euler(0, 0, tiltAngle);

            // Dampen towards the target rotation
            // transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
            transform.rotation = target;

            Debug.Log("Rotating NOW = " + tiltAngle);
            tiltAngle+= 1;
        }


        // Rotate to the left if needed
        else if (rotatingRight)
        {
            // Rotate the cube by converting the angles into a quaternion.
            Quaternion target = Quaternion.Euler(0, 0, tiltAngle);

            // Dampen towards the target rotation
            // transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smooth);
            transform.rotation = target;

            Debug.Log("Rotating NOW = " + tiltAngle);
            tiltAngle-= 1;
        }
    }

    public void ChangeModel()
    {
        Debug.Log("Changing model to model number = " + currModelNumber);
        GameObject thisModel = Instantiate(models[currModelNumber], transform.position, transform.rotation) as GameObject;
        Destroy(currentModel);
        thisModel.transform.parent = transform;
        currentModel = thisModel;
    }


    public void loadPlayerModels() {
        string sAssetFolderPath = "Assets/PlayerModels";
        string[] aux = sAssetFolderPath.Split(new char[] { '/' });
        string onlyFolderPath = aux[0] + "/" + aux[1] + "/";

        string[] aFilePaths = Directory.GetFiles(onlyFolderPath);

        GameObject temp;

        foreach (string sFilePath in aFilePaths)
        {
            if (Path.GetExtension(sFilePath) == ".FBX" || Path.GetExtension(sFilePath) == ".prefab")
            {
                Debug.Log(Path.GetExtension(sFilePath));

                temp = AssetDatabase.LoadAssetAtPath(sFilePath, typeof(Object)) as GameObject;

                models.Add(temp);
            }
        }

    }

}
