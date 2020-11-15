using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using System.IO;
using UnityEditor;
using UnityEngine;


public class ObstacleWalls : MonoBehaviour
{

    public List<GameObject> prefabWalls;

    GameObject WallOne, WallTwo;

    Transform transformOne, transformTwo;

    public Vector3 wallSpeed;

    // Start is called before the first frame update
    void Start()
    {
        // Get the prefab objects and places them inside the "prefabWalls" List<>
        loadPlayerModels();

        // Create the transform objects and move them as needed:
        GameObject temp = new GameObject();
        transformOne = temp.transform;
        // transformTwo = new GameObject().transform;
        transformOne.Translate(new Vector3(0, 4.4F, 0), Space.World);
        // transformTwo.Translate(new Vector3(0, 0, 50), Space.World);
        
        
        WallOne = Instantiate(prefabWalls[0], transformOne.position, transformOne.rotation) as GameObject;
        WallOne.transform.parent = transformOne;
        WallOne.AddComponent(typeof(BoxCollider));

        
        // WallTwo = Instantiate(prefabWalls[1], transformTwo.position, transformTwo.rotation) as GameObject;
        // WallTwo.transform.parent = transformTwo;
    }

    // Update is called once per frame
    void Update()
    {
        // If it is passed -46 Z, de load and change the model
        if (WallOne.transform.position.z < -46) {
            // Debug.Log("Need to swap");
            transformOne.Translate(new Vector3(0, 0, 50), Space.World);
        }

    }

    public void FixedUpdate()
    {
        // Move the walls: 
        transformOne.Translate(wallSpeed * Time.deltaTime, Space.World);
        // transformTwo.Translate(wallSpeed * Time.deltaTime, Space.World);


    }

    public void loadPlayerModels()
    {
        string sAssetFolderPath = "Assets/Resources/WallModels/";
        string[] aux = sAssetFolderPath.Split(new char[] { '/' });
        string onlyFolderPath = aux[0] + "/" + aux[1] + "/";

        string[] aFilePaths = Directory.GetFiles(sAssetFolderPath);

        GameObject temp;

        foreach (string sFilePath in aFilePaths)
        {
            // Debug.Log("Path: [" + sFilePath + "] paths");
            if (Path.GetExtension(sFilePath) == ".prefab")
            {
                string[] path = sFilePath.Split(new char[] { '/' });
                int n = path.Length;

                string finalResPath = path[n - 2] + "/" + Path.GetFileNameWithoutExtension(path[n - 1]);

                // temp = AssetDatabase.LoadAssetAtPath(sFilePath, typeof(Object)) as GameObject;
                temp = Resources.Load(finalResPath, typeof(GameObject)) as GameObject;
                // Debug.Log("Adding the model: " +  finalResPath + " is null?: " + (temp == null? "yes" : "no"));

                prefabWalls.Add(temp);
            }
        }
        // Debug.Log("End Paths: [" + aFilePaths.ToString() + "] paths");
        // Debug.Log("Added [" + prefabWalls.Count + "] Objects");
    }
}
