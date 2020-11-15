using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using System.IO;
using UnityEditor;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{

    public List<GameObject> models;

    private int currModelNumber;

    private GameObject currentModel;


    // Keymappings used for different actions: 
    KeyCode changeModelButton = KeyCode.Space;
    KeyCode rotateRightButton = KeyCode.E;
    KeyCode rotateLeftButton = KeyCode.Q;


    private float tiltAngle = 0.0f;
    private bool rotatingLeft;
    private bool rotatingRight;
    public float tiltIncrement = 3.0f;

    public Vector3 speed = new Vector3(10, 10, 0);
    public Vector3 maxspeed = new Vector3(30, 30, 0);


    private KeyCode[] numberKeyCodes = {
        KeyCode.Alpha0,
        KeyCode.Alpha1,
        KeyCode.Alpha2,
        KeyCode.Alpha3,
        KeyCode.Alpha4,
        KeyCode.Alpha5,
        KeyCode.Alpha6,
        KeyCode.Alpha7,
        KeyCode.Alpha8,
        KeyCode.Alpha9,
     };


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("From the player script");

        // Create the initial object
        currModelNumber = 0;
        loadPlayerModels();
        changeModel();
    }

    public void Update()
    {
        handleRotation();
        handleModelChange();
    }

    public void FixedUpdate()
    {
        handleMovement();
    }

    // Check if we need to change the model of the player
    // If the player did issue a command to change the model, then call the function ChangeModel()
    public void handleModelChange()
    {

        if (Input.GetKeyDown(changeModelButton))
        {
            currModelNumber = (++currModelNumber) % models.Count;
            changeModel();
        }


        for (int i = 0; i < numberKeyCodes.Length; i++)
        {
            if (Input.GetKeyDown(numberKeyCodes[i]) && i < models.Count)
            {
                currModelNumber = i;
                changeModel();
                break;
            }
        }

    }

    void handleMovement()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(speed.x * inputX, speed.y * inputY, 0);

        movement *= Time.deltaTime;
        if (movement.x > maxspeed.x)
        {
            movement.x = 5;
        }
        if (movement.y > maxspeed.y)
        {
            movement.y = 5;
        }

        transform.Translate(movement, Space.World);
    }


    // Rotates the player model as needed
    public void handleRotation()
    {
        // Left rotation
        if (Input.GetKeyDown(rotateLeftButton))
        {
            rotatingLeft = true;
            rotatingRight = false;
        }

        // Right rotation
        if (Input.GetKeyDown(rotateRightButton))
        {
            rotatingLeft = false;
            rotatingRight = true;
        }

        if (Input.GetKeyUp(rotateLeftButton) || Input.GetKeyUp(rotateRightButton))
        {
            rotatingLeft = false;
            rotatingRight = false;
        }

        // Rotate to the left if needed
        if (rotatingLeft)
        {
            // Rotate the cube by converting the angles into a quaternion.
            Quaternion target = Quaternion.Euler(0, 0, tiltAngle);
            transform.rotation = target;

            Debug.Log("Rotating NOW = " + tiltAngle);
            tiltAngle += tiltIncrement;
        }


        // Rotate to the left if needed
        else if (rotatingRight)
        {
            // Rotate the cube by converting the angles into a quaternion.
            Quaternion target = Quaternion.Euler(0, 0, tiltAngle);
            transform.rotation = target;

            Debug.Log("Rotating NOW = " + tiltAngle);
            tiltAngle -= tiltIncrement;
        }
    }

    public void changeModel()
    {
        Debug.Log("Changing model to model number = " + currModelNumber);
        GameObject thisModel = Instantiate(models[currModelNumber], transform.position, transform.rotation) as GameObject;
        Destroy(currentModel);
        thisModel.transform.parent = transform;
        currentModel = thisModel;
    }


    public void loadPlayerModels()
    {
        string sAssetFolderPath = "Assets/Resources/PlayerModels";
        string[] aux = sAssetFolderPath.Split(new char[] { '/' });
        string onlyFolderPath = aux[0] + "/" + aux[1] + "/";

        string[] aFilePaths = Directory.GetFiles(onlyFolderPath);

        GameObject temp;

        foreach (string sFilePath in aFilePaths)
        {
            if (Path.GetExtension(sFilePath) == ".fbx" || Path.GetExtension(sFilePath) == ".prefab")
            {
                Debug.Log(Path.GetExtension(sFilePath));

                // temp = AssetDatabase.LoadAssetAtPath(sFilePath, typeof(Object)) as GameObject;

                Resources.Load<TextAsset>(sFilePath);

                models.Add(temp);
            }
        }

    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Wall (Left)" || collision.gameObject.name == "Wall (Right)" || collision.gameObject.name == "Wall (Top)")  // or if(gameObject.CompareTag("YourWallTag"))
        {
            Debug.Log("Collision");
            transform.Translate(Vector3.zero, Space.World);
        }

        if (collision.gameObject.name == "test")  // or if(gameObject.CompareTag("YourWallTag"))
        {
            Debug.Log("Collision 2");
            transform.Translate(Vector3.zero, Space.World);
        }
    }

}
