using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBat : MonoBehaviour
{
    public enum ColorType
    {
        BlueBat,
        CyanBat,
        GreenBat,
        OrangeBat,
        PurpleBat,
        RedBat,
        YellowBat
    }
    [Serializable]
    public struct Part
    {
        public ColorType colorType;
        public Sprite sprite;
    }
    public Part[] ColorBats;

    private Dictionary<ColorType, Sprite> colorBatDic;
    
    //这个用来获取预制件中的图片
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer=transform.Find("Bat").GetComponent<SpriteRenderer>();
        colorBatDic = new Dictionary<ColorType, Sprite>();
        for(int i = 0;i<ColorBats.Length;i++)
        {
            if (!colorBatDic.ContainsKey(ColorBats[i].colorType))
            {
                colorBatDic.Add(ColorBats[i].colorType, ColorBats[i].sprite);
            }
        }
    }
    //设置颜色
    public void SetBatColor(ColorType tempColorType)
    {
        if(colorBatDic.ContainsKey(tempColorType))
        {
            spriteRenderer.sprite = colorBatDic[tempColorType];
        }
    }
    //读取图片名称
    public string ReadColorName()
    {
       return  spriteRenderer.sprite.name;
    }
    //读取图片
    public Sprite ReadColorSprite()
    {
        return spriteRenderer.sprite;
    }
}
