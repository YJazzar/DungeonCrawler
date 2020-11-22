using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Surrounding : MonoBehaviour
{
    public GameObject playerObject;
    public List<GameObject> walls;
    public GameObject cameraObject;
    public Text gameOverText, scoreText;


    // Private variables for player object
    PlayerScript ps;
    public MovingWalls mw;
    public Vector3 playerSpeed = new Vector3(10, 10, 0);
    private Vector3 playerRecoverySpeed;

    float initial_wall_z, initial_camera_z;


    // Private vars for the env speed
    public Vector3 envSpeed = new Vector3(0, 0, 20);

    public bool gameOver = false;


    // Start is called before the first frame update
    void Start()
    {
        // Error checking
        if (walls.Count != 4)
        {
            Debug.LogError("[ERROR]: Make sure to set the list wall object references for the Surrounding.cs script!");
        }

        playerRecoverySpeed = new Vector3(0, 0, 5);

        initial_wall_z = walls[0].transform.position.z;
        initial_camera_z = cameraObject.transform.position.z;

        ps = playerObject.GetComponent<PlayerScript>();

        InvokeRepeating("decreaseRecSpeed", 4f, 1f);  //1s delay, repeat every 1s
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(mw.score < -2) {
            gameOver = true;
        }
     

        scoreText.text = "Score: " + mw.score;

        if (cameraObject.transform.position.z > playerObject.transform.position.z) {
            gameOver = true;
        }
       
        if (!gameOver) {
            ps.handleMovement(playerSpeed + envSpeed + playerRecoverySpeed);

            // Move the camera and the walls
            cameraObject.transform.Translate(envSpeed * Time.deltaTime);
            for (int i = 0; i < walls.Count; i++)
            {
                walls[i].transform.Translate(envSpeed * Time.deltaTime);
            }
            gameOverText.text = "";
        }
        else {
            gameOverText.text = "Game Over";
        }
    }

    public void Update() {

        if (Input.GetKeyDown(KeyCode.R))
        {
            mw.Reset();
            Reset();
            ps.Reset();

            gameOver = false;
        }
    }

    public void decreaseRecSpeed() {
        if (playerRecoverySpeed.z > 0) {
            playerRecoverySpeed *= 0.95f;
        } else {
            playerRecoverySpeed.z = 0f;
            if (envSpeed.z < 30) {
                envSpeed *= 1.5f;
            }
        }
    }
   
   public void Reset() {
       for (int i = 0; i < walls.Count; i++) {
           walls[i].transform.position = new Vector3(walls[i].transform.position.x, walls[i].transform.position.y, initial_wall_z);
       }
       cameraObject.transform.position = new Vector3(cameraObject.transform.position.x, cameraObject.transform.position.y, initial_camera_z);
       scoreText.text = "Score: " + mw.score;
       gameOverText.text = "";
       gameOver = false;
   }

}
