using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;

public class FilingSystem : MonoBehaviour
{
    private string filingName = "eliminateData.json";
    public void SavePlayerData(GameBat temp)
    {
        string json=JsonUtility.ToJson(temp);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, filingName), json);
    }
    public GameBat ReadPlayerData()
    {
        string filepath = Path.Combine(Application.persistentDataPath, filingName);
        if(File.Exists(filepath))
        {
            string json=File.ReadAllText(filepath);
            return JsonUtility.FromJson<GameBat>(json);
        }
        else
        {
            return null;
        }
    }
    
}
