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
    public int currModelNumber;
    public GameObject currentModel;


    // Keymappings used for different actions: 
    KeyCode changeModelButton = KeyCode.Space;
    KeyCode rotateRightButton = KeyCode.E;
    KeyCode rotateLeftButton = KeyCode.Q;


    private Rigidbody rb;
    private float tiltAngle = 0.0f;
    private bool rotatingLeft;
    private bool rotatingRight;
    public float tiltIncrement = 3.0f;


    public Vector3 maxspeed = new Vector3(30, 30, 30);

    // Save initial position to reload every time the game is reset
    Vector3 resetPos;

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
    public void Start()
    {
        // Create the initial object
        currModelNumber = 0;
        if (models.Count == 0)
        {
            models = Utils.loadPlayerModels();
        }

        changeModel();

        resetPos = transform.position;
        // InvokeRepeating("setRecoverySpeed", 4f, 2f);  //1s delay, repeat every 1s
    }

    public void Reset()
    {
        transform.position = resetPos;
    }

    public void Update()
    {
        handleRotation();
        handleModelChange();
    }

    public void FixedUpdate()
    {

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

    public void handleMovement(Vector3 speed)
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");

        // Vector3 movement = new Vector3(speed.x * inputX, speed.y * inputY, 0);
        Vector3 movement = new Vector3(speed.x * inputX, speed.y * inputY, speed.z);

        movement *= Time.deltaTime;
        if (movement.x > maxspeed.x)
        {
            movement.x = 5;
        }
        if (movement.y > maxspeed.y)
        {
            movement.y = 5;
        }
        if (movement.z > maxspeed.z)
        {
            movement.z = 5;
        }

        rb = GetComponent(typeof(Rigidbody)) as Rigidbody;
        rb.MovePosition(transform.position + movement * Time.deltaTime);
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
            rb = GetComponent(typeof(Rigidbody)) as Rigidbody;
            rb.MoveRotation(target);
            tiltAngle += tiltIncrement;
        }


        // Rotate to the left if needed
        else if (rotatingRight)
        {
            // Rotate the cube by converting the angles into a quaternion.
            Quaternion target = Quaternion.Euler(0, 0, tiltAngle);
            rb = GetComponent(typeof(Rigidbody)) as Rigidbody;
            rb.MoveRotation(target);
            tiltAngle -= tiltIncrement;
        }
    }

    public void changeModel()
    {
        Debug.Log("Changing model to model number = " + currModelNumber);

        // Get the object ready for the swap
        GameObject thisModel = Instantiate(models[currModelNumber], transform.position, transform.rotation) as GameObject;

        Destroy(currentModel);
        thisModel.transform.parent = transform;
        currentModel = thisModel;
    }
}