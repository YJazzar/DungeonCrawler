// using System.Collections;
// using System.Collections.Generic;
// using System.Collections.Specialized;
// using System.Threading;
// using System.IO;
// using UnityEditor;
// using UnityEngine;

// public class WallandPlayerScript : MonoBehaviour
// {

// 	// SHARED VARIABLES
// 	public float rotationOne, rotationTwo;
// 	public Vector3 positionOne, positionTwo;

// 	public int lives = 3;
// 	public bool gameOver = false;


// 	// WALL VARIABLES
// 	public List<GameObject> prefabWalls;

//     GameObject WallOne, WallTwo;

//     Transform transformOne, transformTwo;

//     bool checkOne, checkTwo;

//     int wallNumberOne, wallNumberTwo;

//     public Vector3 wallSpeed = new Vector3(0,0,-15);


//     // PLAYER VARIABLES
//     public List<GameObject> prefabPlayers;

//     private int currModelNumber;

//     private GameObject currentModel;

// 	// Keymappings used for different actions: 
//     KeyCode changeModelButton = KeyCode.Space;
//     KeyCode rotateRightButton = KeyCode.E;
//     KeyCode rotateLeftButton = KeyCode.Q;

//     private float tiltAngle = 0.0f;
//     private bool rotatingLeft;
//     private bool rotatingRight;
//     public float tiltIncrement = 3.0f;

//     public Vector3 speed = new Vector3(10, 10, 0);
//     public Vector3 maxspeed = new Vector3(30, 30, 0);


//     private KeyCode[] numberKeyCodes = {
//         KeyCode.Alpha0,
//         KeyCode.Alpha1,
//         KeyCode.Alpha2,
//         KeyCode.Alpha3,
//         KeyCode.Alpha4,
//         KeyCode.Alpha5,
//         KeyCode.Alpha6,
//         KeyCode.Alpha7,
//         KeyCode.Alpha8,
//         KeyCode.Alpha9,
//      };

//     // Start is called before the first frame update
//     void Start()
//     {
//         // Create the initial player object
//         currModelNumber = 0;
//         loadPlayerModels();
//         changePlayerModel();

//         // Get the prefab objects and places them inside the "prefabWalls" List<>
//         loadWallModels();
//         changeWall(1);
//         // changeWall(2);
//     }


//     public int time = 0;
//     // Update is called once per frame
//     void Update()
//     {
//     	if (gameOver) {
//     		gameOverFunc();
//     		return;
//     	}

//     	// PLAYER STUFF
//     	handleRotation();
//         handleModelChange();


//         // WALL STUFF
//         if (WallOne.transform.position.z < -25) {
//         	// swap wall 1 and reset position
//         	checkOne = false;
//         	changeWall(1);
//         }
//         else if (WallOne.transform.position.z > -22 && WallOne.transform.position.z < -17 && checkOne == false) {
//         	WallPlayerCheck(1);
//         }
//         // if (WallTwo.transform.position.z < -43) {
//         // 	changeWall(2);
//         // }

//         if (time == 240) {
//         	Debug.Log(currentModel.transform.rotation.eulerAngles.z);
//         	time = 0;
//         }
//         else time++;

//     }	

//     public void FixedUpdate()
//     {
//     	if (gameOver) {
//     		gameOverFunc();
//     		return;
//     	}

//     	// PLAYER STUFF
//     	handleMovement();


//     	// WALL STUFF

//         // Move the walls: 
//         transformOne.Translate(wallSpeed * Time.deltaTime, Space.World);
//         // transformTwo.Translate(wallSpeed * Time.deltaTime, Space.World);
//     }


//     public void gameOverFunc() 
//     {
//     	Debug.Log("Game Over");
//     }
//     /*
//      *  SHARED UPDATE FUNCTIONS
//     **/ 

//     public void WallPlayerCheck(int wall) 
//     {
//     	if (wall == 1) {				
// 			float currentPlayerRotation = currentModel.transform.rotation.eulerAngles.z;
// 			// checking position of player relative to wall
// 			if ( (transformOne.position.x-1 < transform.position.x && transform.position.x < transformOne.position.x+1) &&
// 				 (transformOne.position.x-1 < transform.position.x && transform.position.x < transformOne.position.x+1)) {
// 				// if the rotation of the player matches the wall
// 				if ((rotationOne-10 < currentPlayerRotation && currentPlayerRotation < rotationOne+10) ||
// 					(rotationOne+180-10 < currentPlayerRotation && currentPlayerRotation < rotationOne+180+10)) {
// 					checkOne = true;
// 				}
// 				else {
// 					if (lives != 0) lives--;
// 					else gameOver = true;
// 				}
// 			}
// 			else {
// 				if (lives != 0) lives--;
// 				else gameOver = true;
// 			}
//     	}
//     	if (wall == 2) {
//     		float currentPlayerRotation = currentModel.transform.rotation.eulerAngles.z;
// 			// checking position of player relative to wall
// 			if ( (transformTwo.position.x-1 < transform.position.x && transform.position.x < transformTwo.position.x+1) &&
// 				 (transformTwo.position.x-1 < transform.position.x && transform.position.x < transformTwo.position.x+1)) {
// 				// if the rotation of the player matches the wall
// 				if ((rotationTwo-10 < currentPlayerRotation && currentPlayerRotation < rotationTwo+10) ||
// 					(rotationTwo+180-10 < currentPlayerRotation && currentPlayerRotation < rotationTwo+180+10)) {
// 					checkTwo = true;
// 				}
// 				else {
// 					if (lives != 0) lives--;
// 					else gameOver = true;
// 				}
// 			}
// 			else {
// 				if (lives != 0) lives--;
// 				else gameOver = true;
// 			}
//     	}
//     	Debug.Log("in wall check");
//     	Debug.Log(lives);
//     }

//     /*
//      * PLAYER MODEL UPDATE FUNCTIONS
//     **/
//     public void handleModelChange()
//     {

//         if (Input.GetKeyDown(changeModelButton))
//         {
//             currModelNumber = (++currModelNumber) % prefabPlayers.Count;
//             changePlayerModel();
//         }


//         for (int i = 0; i < numberKeyCodes.Length; i++)
//         {
//             if (Input.GetKeyDown(numberKeyCodes[i]) && i < prefabPlayers.Count)
//             {
//                 currModelNumber = i;
//                 changePlayerModel();
//                 break;
//             }
//         }

//     }

//     void handleMovement()
//     {
//         float inputX = Input.GetAxis("Horizontal");
//         float inputY = Input.GetAxis("Vertical");

//         Vector3 movement = new Vector3(0, 0, 0);

//         if (inputX != 0 || inputY != 0) {   
//             movement = new Vector3(speed.x * inputX, speed.y * inputY, 0);           
//         }
//         // else  {

//         //     if (transform.position.x < 0) {
//         //         movement.x += 3;
//         //     }
//         //     else if (transform.position.x > 0)
//         //     {
//         //         movement.x -= 3;
//         //     }
//         //     else if (transform.position.x == 0)
//         //     {
//         //         movement.x -= 0.1f;
//         //     }

//         //     if (transform.position.y < 6)
//         //     {
//         //         movement.y += 3;
//         //     }
//         //     else if (transform.position.y > 6)
//         //     {
//         //         movement.y -= 3;
//         //     }
//         //     else if (transform.position.y == 6)
//         //     {
//         //         movement.y -= 0.1f;
//         //     }
//         // }

//         movement *= Time.deltaTime;
//         if (movement.x > maxspeed.x)
//         {
//             movement.x = 5;
//         }
//         if (movement.y > maxspeed.y)
//         {
//             movement.y = 5;
//         }

//         transform.Translate(movement, Space.World);
//     }

//     public void handleRotation()
//     {
//         // Left rotation
//         if (Input.GetKeyDown(rotateLeftButton))
//         {
//             rotatingLeft = true;
//             rotatingRight = false;
//         }

//         // Right rotation
//         if (Input.GetKeyDown(rotateRightButton))
//         {
//             rotatingLeft = false;
//             rotatingRight = true;
//         }

//         if (Input.GetKeyUp(rotateLeftButton) || Input.GetKeyUp(rotateRightButton))
//         {
//             rotatingLeft = false;
//             rotatingRight = false;
//         }

//         // Rotate to the left if needed
//         if (rotatingLeft)
//         {
//             // Rotate the cube by converting the angles into a quaternion.
//             Quaternion target = Quaternion.Euler(0, 0, tiltAngle);
//             transform.rotation = target;

//             Debug.Log("Rotating NOW = " + tiltAngle);
//             tiltAngle += tiltIncrement;
//         }


//         // Rotate to the left if needed
//         else if (rotatingRight)
//         {
//             // Rotate the cube by converting the angles into a quaternion.
//             Quaternion target = Quaternion.Euler(0, 0, tiltAngle);
//             transform.rotation = target;

//             Debug.Log("Rotating NOW = " + tiltAngle);
//             tiltAngle -= tiltIncrement;
//         }
//     }

//     public void changePlayerModel()
//     {
//         // Debug.Log("Changing model to model number = " + currModelNumber);
//         GameObject thisModel = Instantiate(prefabPlayers[currModelNumber], transform.position, transform.rotation) as GameObject;
//         Destroy(currentModel);
//         thisModel.transform.parent = transform;
//        	thisModel.transform.Translate(0,0,-20);
//         currentModel = thisModel;
//     }

//     public void loadPlayerModels()
//     {
//         string sAssetFolderPath = "Assets/Resources/PlayerModels/";
//         string[] aux = sAssetFolderPath.Split(new char[] { '/' });
//         string onlyFolderPath = aux[0] + "/" + aux[1] + "/";

//         string[] aFilePaths = Directory.GetFiles(sAssetFolderPath);

//         GameObject temp;
        
//         foreach (string sFilePath in aFilePaths)
//         {
//             // Debug.Log("Path: [" + sFilePath + "] paths");
//             if (Path.GetExtension(sFilePath) == ".prefab")
//             {
//                 string[] path = sFilePath.Split(new char[] { '/' });
//                 int n = path.Length; 

//                 string finalResPath = path[n - 2] + "/" + Path.GetFileNameWithoutExtension(path[n - 1]);

//                 // temp = AssetDatabase.LoadAssetAtPath(sFilePath, typeof(Object)) as GameObject;
//                 temp = Resources.Load(finalResPath, typeof(GameObject)) as GameObject;
//                 // Debug.Log("Adding the model: " +  finalResPath + " is null?: " + (temp == null? "yes" : "no"));

//                 prefabPlayers.Add(temp);
//             }
//         }
//         // Debug.Log("End Paths: [" + aFilePaths.ToString() + "] paths");
//         // Debug.Log("Added [" + models.Count + "] Objects");
//     }





//     /*
//      * WALL MODEL UPDATE FUNCTIONS
//     **/
//     public void loadWallModels()
//     {
//         string sAssetFolderPath = "Assets/Resources/WallModels/";
//         string[] aux = sAssetFolderPath.Split(new char[] { '/' });
//         string onlyFolderPath = aux[0] + "/" + aux[1] + "/";

//         string[] aFilePaths = Directory.GetFiles(sAssetFolderPath);

//         GameObject temp;

//         foreach (string sFilePath in aFilePaths)
//         {
//             // Debug.Log("Path: [" + sFilePath + "] paths");
//             if (Path.GetExtension(sFilePath) == ".prefab")
//             {
//                 string[] path = sFilePath.Split(new char[] { '/' });
//                 int n = path.Length;

//                 string finalResPath = path[n - 2] + "/" + Path.GetFileNameWithoutExtension(path[n - 1]);

//                 // temp = AssetDatabase.LoadAssetAtPath(sFilePath, typeof(Object)) as GameObject;
//                 temp = Resources.Load(finalResPath, typeof(GameObject)) as GameObject;
//                 // Debug.Log("Adding the model: " +  finalResPath + " is null?: " + (temp == null? "yes" : "no"));

//                 prefabWalls.Add(temp);
//             }
//         }
// 	}

// 	public void changeWall(int wall) 
// 	{
// 		if (wall == 1) {
// 			// Create the transform objects and move them as needed:
// 	        transformOne = new GameObject().transform;

// 	        positionOne = new Vector3(Random.Range(-7.5f, 7.5f), Random.Range(1.5f, 14.1f), 0.0f);
// 	        rotationOne = Random.Range(-0.5f, 90.5f);

// 	        wallNumberOne = (int)Random.Range(-0.5f, 2.5f);
// 	        transformOne.Translate(new Vector3(positionOne.x, positionOne.y, 50), Space.World);		
// 	        transformOne.rotation = Quaternion.Euler(0,0,rotationOne);
// 	        Destroy(WallOne);
// 	        WallOne = Instantiate(prefabWalls[wallNumberOne], transformOne.position, transformOne.rotation) as GameObject;
// 	        WallOne.transform.parent = transformOne;
// 		}
// 		else if (wall == 2) {
// 			// transformTwo = new GameObject().transform;
// 			// positionTwo = new Vector3(Random.Range(-8, 8), Random.Range(1.5, 14.1), 0);
//          	// rotationTwo = Quaternion.Euler(0, 0, Random.Range(-0.5, 90.5));
//         	// transformTwo.Translate(new Vector3(0, 0, 50), Space.World);
//         	// destroy(WallTwo)
//         	// WallTwo = Instantiate(prefabWalls[1], transformTwo.position, transformTwo.rotation) as GameObject;
//         	// WallTwo.transform.parent = transformTwo;
//         }
// 	}
// }
