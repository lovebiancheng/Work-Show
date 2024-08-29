using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hjojo : MonoBehaviour
{
    private float timeCount;

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.time;
        if(timeCount > 2000)
        {
            timeCount = 0;
            Cache.Instance.CollectObject(gameObject, 0);
        }
    }
    //public string name;
    //private T CreatObject<T>() where T : class
    //{
    //    Type type = Type.GetType(name);
    //    return Activator.CreateInstance(type)as T;
    //}
}
