using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

public class ResourceManager 
{
    public static Dictionary<string, string> configMap;
    //加载文件
    public static string GetConfigFile()
    {
        string url;
#if UNITY_EDITOR||UNITY_STANDALONE
        url= "file://" + Application.persistentDataPath + "/Config.text";
#elif UNITY_ANDROID
        url="jar:file:///"+Application.petsistenDataPath+"/!/assets/Config.text";
#elif UNITY_IOS
        url="file://"+Application.petsistenDataPath+"/Raw"/Config.text";
#endif
        UnityWebRequest request = UnityWebRequest.Get(url);
        while(true)
        {
            if(request.isDone)
            {
                Debug.Log("这是文本" + request.downloadHandler.text);
                return request.downloadHandler.text;
            }
        }
    }
    //解析文件
    public static void BuidlMap(string fileContent)
    {
        configMap= new Dictionary<string, string>();
        using(StringReader reader= new StringReader(fileContent))
        {
            string line=reader.ReadLine();
            while(line!=null)
            {
                string[] keyValue=line.Split('=');
                configMap.Add(keyValue[0], keyValue[1]);
                line=reader.ReadLine();
            }
        }
    }
   public static T Load<T>(string prefabName) where T : Object
    {
        return Resources.Load<T>(prefabName);
    }
}
