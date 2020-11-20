using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using System.IO;
using UnityEditor;
using UnityEngine;


public class MovingWalls : MonoBehaviour
{

    public List<GameObject> prefabWalls;
    public GameObject playerObject;

    GameObject WallOne, WallTwo;
    Transform transformOne, transformTwo;

    // Constants
    Vector3 wallSpeed = new Vector3(0, 0, -15);
    float wallDistanceOffset = 60;
    float fadeSpeed = 3;

    void Start()
    {
        // Get the prefab objects and places them inside the "prefabWalls" List<>
        prefabWalls = Utils.loadPreFabWalls();

        // Create the transform object for wallOne
        GameObject temp = new GameObject();
        transformOne = temp.transform;
        transformOne.Translate(new Vector3(0, 4.4F, 0 * wallDistanceOffset), Space.World);
        // Create the wallOne object
        WallOne = Instantiate(prefabWalls[0], transformOne.position, transformOne.rotation) as GameObject;
        WallOne.transform.parent = transformOne;
        WallOne.AddComponent(typeof(MeshCollider));
        WallOne.GetComponent<MeshRenderer>().material = Utils.getWallOneMaterial();

        // Create the transform object for wallTwo
        temp = new GameObject();
        transformTwo = temp.transform;
        transformTwo.Translate(new Vector3(0, 4.4F, wallDistanceOffset), Space.World);
        // Create the wallTwo object
        WallTwo = Instantiate(prefabWalls[1], transformTwo.position, transformTwo.rotation) as GameObject;
        WallTwo.transform.parent = transformTwo;
        WallTwo.AddComponent(typeof(MeshCollider));
        WallTwo.GetComponent<MeshRenderer>().material = Utils.getWallTwoMaterial();

        InvokeRepeating("OutputTime", 2f, 1f);  //1s delay, repeat every 1s

    }


    public void FixedUpdate()
    {
        // Check if the walls need to be swapped
        if (WallOne.transform.position.z < -65)
        {
            // Reset the wall
            transformOne.Translate(new Vector3(0, 0, 2*wallDistanceOffset), Space.World);
            resetWallOneFade();

            // Swap out the walls since wallTwo is now closer and wallOne will be reset
            GameObject gameObjectTemp = WallOne; 
            Transform transformTemp = transformOne;
            WallOne = WallTwo;
            WallTwo = gameObjectTemp;

            transformOne = transformTwo; 
            transformTwo = transformTemp;
        }

        // Check if the wallOne needs to become transparent
        if (WallOne.transform.position.z - 10 < playerObject.transform.position.z) {
            fadeWallOne();
        }

        // Move the walls
        transformOne.Translate(wallSpeed * Time.deltaTime, Space.World);
        transformTwo.Translate(wallSpeed * Time.deltaTime, Space.World);        
    }

    public void fadeWallOne() {
        Color objColor = WallOne.GetComponent<Renderer>().material.color;
        float fadeAmount = objColor.a - (fadeSpeed * Time.deltaTime);

        if (objColor.a > 0)
        {
            Debug.Log("here");
            objColor = new Color(objColor.r, objColor.g, objColor.b, fadeAmount);
            WallOne.GetComponent<Renderer>().material.color = objColor;
        }
    }

    public void resetWallOneFade() {
        Color objColor = WallOne.GetComponent<Renderer>().material.color;
        objColor = new Color(objColor.r, objColor.g, objColor.b, 1);
        WallOne.GetComponent<Renderer>().material.color = objColor;
    }

    void OutputTime()
    {
        // Debug.Log(Time.time);
        wallSpeed *= 1.05f;
    }
  
}
