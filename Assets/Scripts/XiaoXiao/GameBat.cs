using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameBat : MonoBehaviour
{
    
    //球拍的基本信息，位置，物品类型
    private int x;
    private int y;
    private GameManager.ItemType type;
    private ColorBat colorBatComponent;
    private MoveBat moveBatComponent;
    private RemoveBat removeBatComponent;
    public int X
    {
        get { return x; } 
        set
        {
            x = value;
        }
    }
    public int Y
    {
        get { return y; }
        set
        {
            y = value;
        }
    }
    public GameManager.ItemType Type
    {
        get { return type; }
    }
    public ColorBat ColorBatComponent
    {
        get
        {
            return colorBatComponent;
        }
    }
    public MoveBat MoveBatComponent
    {
        get
        {
            return moveBatComponent;
        }
    }
    public RemoveBat RemoveBatComponent
    {
        get
        {
            return removeBatComponent;
        }
    }

    public void Deliver(int m,int n,GameManager.ItemType temp)
    {
        x = m;
        y=n;
        type = temp;
    }
    public void Deliver(int m,int n)
    {
        x = m;
        y = n;
    }
    private void Awake()
    {
        colorBatComponent=GetComponent<ColorBat>();
        moveBatComponent = GetComponent<MoveBat>();
        removeBatComponent = GetComponent<RemoveBat>();
    }



    //鼠标
    
    private void OnMouseEnter()
    {
        GameManager.Instance.SecondBat(this);
    }
    private void OnMouseDown()
    {
        GameManager.Instance.FirstBat(this);
    }
    private void OnMouseUp()
    {
        GameManager.Instance.ReleaseBat();
    }
    

}
