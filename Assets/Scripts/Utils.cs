using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Utils
{
    public static List<GameObject> loadPlayerModels()
    {
        List<GameObject> models = new List<GameObject>();
        string sAssetFolderPath = "Assets/Resources/PlayerModels/";
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

                models.Add(temp);
            }
        }
        // Debug.Log("End Paths: [" + aFilePaths.ToString() + "] paths");
        // Debug.Log("Added [" + models.Count + "] Objects");
        return models;
    }


    public static List<GameObject> loadPreFabWalls()
    {
        List<GameObject> prefabWalls = new List<GameObject>();
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

        return prefabWalls;
    }

    public static Material getWallOneMaterial() {
        return Resources.Load("Materials/WallOneMat", typeof(Material)) as Material;
    }

    public static Material getWallTwoMaterial()
    {
        return Resources.Load("Materials/WallTwoMat", typeof(Material)) as Material;
    }


}
