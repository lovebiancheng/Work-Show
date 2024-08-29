using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cache : MonoBehaviour
{
    //����ģʽ
    private static Cache _instance;
    public static Cache Instance
    {
        get 
        { 
            if (_instance == null)
            {
                GameObject go= new GameObject(nameof(Cache));
                _instance = go.AddComponent<Cache>();
            }
            return _instance; 
        }
        
    }
    public Dictionary<string, List<GameObject>> cache;
    private void Awake()
    {
        cache = new Dictionary<string, List<GameObject>>();
    }
    //��������
    public GameObject CreatObject(string key,GameObject prefab,Vector3 pos, Quaternion rot)
    {
       GameObject obj = null;
        if(cache.ContainsKey(key))
        {
            obj = cache[key].Find(g => !g.activeInHierarchy);//������þͰѰ��������
        }
        if(obj == null)
        {
            obj = Instantiate(prefab);
            if(!cache.ContainsKey(key))
            {
                cache.Add(key, new List<GameObject>());
                
            }
            cache[key].Add(obj);
        }
        
        obj.transform.position = pos;
        obj.transform.rotation = rot;
        obj.SetActive(true);
        if (key == "PingPongBall")
        {
            Rigidbody rb = obj.GetComponent<Rigidbody>();
            rb.useGravity = false;
        }
        return obj;
    }
    public void CollectObject(GameObject obj,float delayTime)
    {
        StartCoroutine(CollectObjectCoroutine(obj,delayTime));
    }
    private IEnumerator CollectObjectCoroutine(GameObject obj,float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Debug.Log("������");
        obj.SetActive(false);
    }
    public void Clear(string key)
    {
        for(int i=cache.Count-1;i>=0;i--)
        {
            Destroy(cache[key][i]);
        }
        cache.Remove(key);
    }
    public void ClarAll()
    {
        List<string> keyList = new List<string>(cache.Keys);
        foreach (var key in keyList)
        {
            Clear(key);
        }
    }
}
