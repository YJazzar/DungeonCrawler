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

    // Start is called before the first frame update
    void Start()
    {
        loadPlayerModels();
    }

    // Update is called once per frame
    void Update()
    {
        
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
            if (Path.GetExtension(sFilePath) == ".fbx" || Path.GetExtension(sFilePath) == ".prefab")
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
        // Debug.Log("Added [" + models.Count + "] Objects");
    }

}
