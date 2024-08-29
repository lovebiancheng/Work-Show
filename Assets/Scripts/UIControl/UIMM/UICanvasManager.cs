using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICanvasManager : MonoBehaviour
{
    public static GameObject[] gameObjects;
    public static int indexBefore=0;


    private void Start()
    {
        gameObjects = GameObject.FindGameObjectsWithTag("Panel");
        //Debug.Log(gameObjects.Length);
        OpenPanel("MainCanvas");
        //OpenPanel("PlayerBag");
    }
    

    public static void OpenPanel(string panel)
    {
        //Debug.Log("zzzzz");
        if (gameObjects == null)
        {
            return;
        }
        CloseAllPanels();
        for(int i = 0; i < gameObjects.Length; i++)
        {
            if (gameObjects[i].name == panel)
            {
                gameObjects[i].SetActive(true);
                indexBefore = i;
                Debug.LogFormat("chenggongdakai:{0}",gameObjects[i].name);
                break;
            }
        }

    }
    //·µ»Ø·½·¨
    public void ReturnMainPanel()
    {
        if (gameObjects == null)
        {
            return;
        }
        CloseAllPanels();
        OpenPanel("MainCanvas");
    }


    public static void CloseAllPanels()
    {
        for (int i = 0;i< gameObjects.Length;i++)
        {
            gameObjects[i].SetActive(false);
        }
    }

    //public IEnumerator DisablePanelDeleyde(UICanvasInstance uICanvasInstance)
    //{
    //    bool closed=false;
    //    bool wantToClose = true;
        
    //}
}
