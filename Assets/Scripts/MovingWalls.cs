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

    GameObject WallOne, WallTwo;
    Transform transformOne, transformTwo;
    public Vector3 wallSpeed;


    void Start()
    {
        // Get the prefab objects and places them inside the "prefabWalls" List<>
        prefabWalls = Utils.loadPreFabWalls();

        // Create the transform object for wallOne
        GameObject temp = new GameObject();
        transformOne = temp.transform;
        transformOne.Translate(new Vector3(0, 4.4F, 0), Space.World);
        // Create the wallOne object
        WallOne = Instantiate(prefabWalls[0], transformOne.position, transformOne.rotation) as GameObject;
        WallOne.transform.parent = transformOne;
        WallOne.AddComponent(typeof(MeshCollider));


        // Create the transform object for wallTwo
        temp = new GameObject();
        transformTwo = temp.transform;
        transformTwo.Translate(new Vector3(0, 4.4F, -10), Space.World);
        // Create the wallTwo object
        WallTwo = Instantiate(prefabWalls[1], transformOne.position, transformOne.rotation) as GameObject;
        WallTwo.transform.parent = transformTwo;
        WallTwo.AddComponent(typeof(MeshCollider));
    }


    public void FixedUpdate()
    {
        // transformOne.Translate(wallSpeed * Time.deltaTime, Space.World);
        if (WallOne.transform.position.z < -46)
        {
            transformOne.Translate(new Vector3(0, 0, 50), Space.World);
        }
        else
        {
            transformOne.Translate(new Vector3(0, 0, -5)* Time.deltaTime, Space.World);
        }
    }

  
}
