using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class ziyuanpeizhi : MonoBehaviour
{
    [MenuItem("Tools/CreatTable")]
    public static void CreatTable()
    {
        string[] shuzu = AssetDatabase.FindAssets("t:prefab", new string[] { "Assets/Resources" });
        for(int i=0;i<shuzu.Length; i++)
        {
            shuzu[i] = AssetDatabase.GUIDToAssetPath(shuzu[i]);
            string fileName = Path.GetFileName(shuzu[i]);
            string filePath = shuzu[i].Replace("Asset/Resources/",string.Empty);
            shuzu[i] = fileName + "=" + filePath;
        }
        File.WriteAllLines("StreamingAssets.txt", shuzu);
    }
}
