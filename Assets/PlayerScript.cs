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
    KeyCode changeModel = KeyCode.Space;
    KeyCode rotateRight = KeyCode.E;
    KeyCode rotateLeft = KeyCode.Q;


    float tiltAngle = 0.0f;
    float smooth = 5.0f;
    bool rotatingLeft;
    bool rotatingRight;

    public Vector3 speed = new Vector3(10, 10, 0);
    public Vector3 maxspeed = new Vector3(30, 30, 0);
    public double jumpTime = 1.5;
    public bool jump = false;
    public float jumpHeight = 1;

    public GameObject g;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("From the player script");

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

    // Update is called once per frame
    void FixedUpdate()
    {
        float inputX = Input.GetAxis("Horizontal");
        float inputY = Input.GetAxis("Vertical");



        Vector3 movement = new Vector3(speed.x * inputX, speed.y * inputY, 0);

        movement *= Time.deltaTime;
        if (movement.x > maxspeed.x) {
            movement.x = 5;
        }
        if (movement.y > maxspeed.y) {
            movement.y = 5;
        }


        // position  -9.5 < x < 6
        //             0 < y < 9.7

        // if (-9.5 < transform.Position.x < 6) {

        // }
        // Debug.Log(transform.position.x);

        // if (Input.GetKey(KeyCode.Space)) {
        //     g = GameObject.Find("Player");
        //     Rigidbody r = GameObject.Find("Player").GetComponent<Rigidbody>();
        //     if (r.velocity.y > maxspeed.y) { r.velocity = new Vector3 (r.velocity.x, r.velocity.y, r.velocity.z); }
        //     else { r.velocity = new Vector3 (r.velocity.x, r.velocity.y + jumpHeight*inputY, r.velocity.z); }
            // .getComponent<Rigidbody>();
            // r.velocity = new Vector3(r.velocity.x, 8, r.velocity.z);
        // }

        // if (inputY > 0) { jump = true; }

        // if (transform.position.y < 1) {
        //     jumpTime = 1.5;
        // }
        // else {
        //     jumpTime = 0;
        // }
        // if (jump) {
        //     movement.y = speed.y * inputY * Time.deltaTime;
        //     jumpTime -= Time.deltaTime;
        //     if (jumpTime < 0) { jump = false; }
        // }

        // Debug.Log(movement.x);
        // Debug.Log(movement.z);
        transform.Translate(movement, Space.World);
    }

    void OnCollisionEnter(Collision collision) 
     {
         if(collision.gameObject.name == "Wall (Left)" || collision.gameObject.name == "Wall (Right)" || collision.gameObject.name == "Wall (Top)")  // or if(gameObject.CompareTag("YourWallTag"))
         {
            Debug.Log("Collision");
            transform.Translate(Vector3.zero, Space.World);
         }
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
