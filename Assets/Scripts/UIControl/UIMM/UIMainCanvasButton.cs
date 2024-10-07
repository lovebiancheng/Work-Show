using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIMainCanvasButton : MonoBehaviour 
{

    public Text playerMoney;
    private void Start()
    {
        playerMoney = GameObject.Find("MainCanvas/CanvasBg/PlayerMoney").GetComponent<Text>();
        playerMoney.text = Tools.money.ToString();
        ShopMain.mydelegate += ChangeMoney;
    }
    public void ButtonBagClick()
    {
        string canv = "PlayerBag";
        
        UICanvasManager.OpenPanel(canv);
    }
    public void ButtonStoreClick()
    {
        string canv = "ShopCanvas";
        UICanvasManager.OpenPanel(canv);
    }
    public void ButtonZhanDouClick()
    {
        SceneManager.LoadScene("TrainHall");
    }
    
    

    
    public void ChangeMoney(string temp)
    {
        playerMoney.text = temp;
    }
    
}



