using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.IO;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using static UnityEditor.Progress;


[System.Serializable]
public class Tools : MonoBehaviour
{
    private  static string saveFileName = "BagData.json";
    
    public static List<PlayerData> dataitems=new List<PlayerData>();
    public static List<PlayerData> afterData=new List<PlayerData>();
    public static PlayerData data;
    public static int money=1000;
    [MenuItem("Tools/CreatTestData", false, 1)]
    
    public  static void CreatTestData()
    {
        
        data = new PlayerData();
        data.itemid = 1001;
        data.itemName = "QiuPai1";
       
        dataitems.Add(data);
        PlayerData playerData = new PlayerData();
        playerData.itemid = 2001;
        playerData.itemName = "QiuPai2";
        dataitems.Add(playerData);
        //Debug.Log(data.itemid);
        PlayerData test1= new PlayerData();
        test1.itemid = 3001;
        test1.itemName = "XieZi1";
        dataitems.Add(test1);
        SaveData();
        
    }
    public static void SaveData()
    {
        
        Data data = new Data();
        data.datalist = dataitems;
        string json=JsonUtility.ToJson(data);
        File.WriteAllText(Path.Combine(Application.persistentDataPath, saveFileName), json);


        //这种将列表中的每一项都进行一次序列化然后再把列表序列化的方式方式过于繁琐以及抽象
        //for(int i = 0;i < dataitems.Count; i++)
        //{

        //    //data.datalist.Add(JsonUtility.ToJson(dataitems[i]));
        //    //Debug.Log(data.datalist.Count);

        //}

        //string json = JsonUtility.ToJson(data, true);
        //File.WriteAllText(Path.Combine(Application.persistentDataPath, saveFileName), json);
        //Debug.Log(json);
    }



    [MenuItem("Tools/ReadTestData",false,2)]
    public static void ReadData()
    {
        string filepath=Path.Combine(Application.persistentDataPath,saveFileName);
        if(File.Exists(filepath))
        {
            string json = File.ReadAllText(filepath);
            Debug.Log(json);
            Data temp=new Data();
            temp=JsonUtility.FromJson<Data>(json);
            //foreach(var item in temp.datalist)
            //{
            //    Debug.Log(item.itemid);
            //}
            afterData = temp.datalist;
            //Data temp= new Data();
            //temp = JsonUtility.FromJson<Data>(json);
            //if(temp != null)
            //{
            //    //这里清空dataitems列表里面的数值
            //    List<string> templist = temp.datalist;
            //    for(int i = 0;i < templist.Count; i++)
            //    {

            //    }
            //}
        }
        else
        {
            Debug.Log("kongkongruye");
        }
    }




}
[System.Serializable]
public class PlayerData
{
    public int itemid;
    public string itemName;
}

//[System.Serializable]
public  class Data 
{
    //public List<string> datalist=new List<string>();
      public List<PlayerData> datalist=new List<PlayerData>();
}

