using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using System.IO;
using UnityEditor;
using UnityEngine;


public class MovingWalls : MonoBehaviour
{

    public List<GameObject> wallObjects;
    public GameObject playerObject;

    int wallOne, wallTwo;   // used as indices to the wall Objects
    List<int> unusedWalls;

    // Constants
    public Vector3 wallSpeed = new Vector3(0, 0, -15);
    Vector3 resetPosition = new Vector3(0, 60, 130);
    float wallDistanceOffset = 40;
    float fadeSpeed = 3;

    void Start()
    {
        // Error checking
        if (wallObjects.Count != 3) 
        {
            Debug.LogError("[ERROR]: Make sure to set the list wall object references for the MovingWalls.cs script!");
        }

        if (playerObject == null)
        {
            Debug.LogError("[ERROR]: Make sure to set the player object reference in the MovingWalls.cs script!");
        }

        // Shuffle the set of walls
        int swapIndex; 
        GameObject tempObj;
        for (int i = 0; i < wallObjects.Count; i ++) {
            swapIndex = Random.Range(0, wallObjects.Count);
            tempObj = wallObjects[i]; 
            wallObjects[i] = wallObjects[swapIndex];
            wallObjects[swapIndex] = tempObj;
        }


        // Set the correct indices for wallOne and wallTwo. Then transform them into their initial positions
        wallOne = 0; 
        wallTwo = 1;
        wallObjects[wallOne].transform.position = new Vector3(0, 4.4F, 0 * wallDistanceOffset);
        wallObjects[wallTwo].transform.position = new Vector3(0, 4.4F, 2 * wallDistanceOffset);


        // Init the list of unused walls to keep track of and hide them from the user
        unusedWalls = new List<int>(); 
        for (int i = 2; i < wallObjects.Count; i ++) {
            unusedWalls.Add(i);
            wallObjects[i].transform.position = resetPosition;
        }

        InvokeRepeating("OutputTime", 2f, 1f);  //1s delay, repeat every 1s
    }


    public void FixedUpdate()
    {
        // Check if the walls need to be swapped
        if (wallObjects[wallOne].transform.position.z < -65)
        {
            // Reset the wall
            wallObjects[wallOne].transform.position = resetPosition;
            resetWallFade(wallOne);

            // Get the new wall that wallOne will be swapped out with
            int temp = Random.Range(0, unusedWalls.Count);
            int newWall = unusedWalls[temp];
            unusedWalls.RemoveAt(temp);

            // Swap out the walls since wallTwo is now closer and wallOne will be reset
            unusedWalls.Add(wallOne);
            wallOne = wallTwo; 
            wallTwo = newWall;

            // Set the position of the newWall to be correctly displaced
            wallObjects[wallTwo].transform.position = wallObjects[wallOne].transform.position + new Vector3(0, 0, 2 * wallDistanceOffset);
        }

        // Check if any of the walls need to become transparent
        for (int i = 0; i < wallObjects.Count; i ++) {
            if (wallObjects[i].transform.position.z - 10 < playerObject.transform.position.z) {
                fadeWall(i);
            }
        }


        // Move the walls
        wallObjects[wallOne].transform.Translate(wallSpeed * Time.deltaTime, Space.World);
        wallObjects[wallTwo].transform.Translate(wallSpeed * Time.deltaTime, Space.World);              
    }

    public void fadeWall(int wallIndex) {
        Color objColor = wallObjects[wallIndex].GetComponent<Renderer>().material.color;
        float fadeAmount = objColor.a - (fadeSpeed * Time.deltaTime);

        if (objColor.a > 0)
        {
            objColor = new Color(objColor.r, objColor.g, objColor.b, fadeAmount);
            wallObjects[wallIndex].GetComponent<Renderer>().material.color = objColor;
        }
    }

    public void resetWallFade(int wallIndex) {
        Color objColor = wallObjects[wallIndex].GetComponent<Renderer>().material.color;
        objColor = new Color(objColor.r, objColor.g, objColor.b, 1);
        wallObjects[wallIndex].GetComponent<Renderer>().material.color = objColor;
    }


    void OutputTime()
    {
        // Debug.Log(Time.time);
        wallSpeed *= 1.03f;
        if (wallSpeed.z < -24) {
            wallSpeed.z = -24;
        }
    }
  
}
