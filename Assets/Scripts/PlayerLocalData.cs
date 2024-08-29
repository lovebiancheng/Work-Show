using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLocalData : MonoBehaviour
{
    private static PlayerLocalData _instance;
    public static PlayerLocalData Instance
    {
        
        get 
        { 
            if (_instance == null)
            {
                _instance=new PlayerLocalData();
            }
            return _instance; 
        }
    }

    
}
