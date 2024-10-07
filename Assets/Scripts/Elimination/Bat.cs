using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum BatColor
{
    BlueBat,
    CyanBat,
    GreenBat,
    OrangeBat,
    PurpleBat,
    RedBat,
    YellowBat
}
public enum BatType
{
    Empty,
    Normal
}
[Serializable]
public class Bat :MonoBehaviour,IPointerEnterHandler,IPointerDownHandler,IPointerUpHandler,IPointerExitHandler
{
    
    public int X { get; set; }
    public int Y { get; set; }
    public BatColor batColor;
    public BatType batType;
    public Vector3 batPosition;
    public int batIndex;
    private void Active()
    {
        EliminationGameManager.Instance.gridList[batIndex].transform.Find("SelectImage").gameObject.SetActive(true);
    }
    private void Disactive()
    {
        EliminationGameManager.Instance.gridList[batIndex].transform.Find("SelectImage").gameObject.SetActive(false);
    }
   
    public void OnPointerEnter(PointerEventData eventData)
    {
        
        Active();
        
        //Debug.Log("batIndex:" +batIndex);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!EliminationGameManager.Instance.IsSecond)
        {
            EliminationGameManager.Instance.FirstBat(this.gameObject);
        }
        else
        {
            if(EliminationGameManager.Instance.IsNear(this.gameObject))
            {
                EliminationGameManager.Instance.SecondBat(this.gameObject);
                EliminationGameManager.Instance.Exchange();
            }
            
        }
        EliminationGameManager.Instance.IsSecond = !EliminationGameManager.Instance.IsSecond;
        //Debug.Log("µã»÷ÁË");
       
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Disactive();
    }
}
