using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ShopItemClick : MonoBehaviour
{
    private Button _button;
    private Image _sprite;
    private ItemSc _sourceList;
    public Text UIitemName;
    public Text UIitemQuality;
    public Text UIitemDescription;
    public Text UIitemPrice;
    public static int id;
    
    private void Start()
    {
        _sprite = GetComponent<Image>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SingleItemButtonClick);
        _sourceList = AssetDatabase.LoadAssetAtPath<ItemSc>("Assets/Scripts/UIControl/PlayerBag/ItemSc.asset");
        UIitemName = GameObject.Find("ShopCanvas/ShopDetailBg/UIItemName").GetComponent<Text>();
        UIitemQuality = GameObject.Find("ShopCanvas/ShopDetailBg/UIItemQuality").GetComponent<Text>();
        UIitemDescription = GameObject.Find("ShopCanvas/ShopDetailBg/UIItemDescription").GetComponent<Text>();
        UIitemPrice = GameObject.Find("ShopCanvas/ShopDetailBg/Coin/UIItemPrice").GetComponent<Text>();
        
        
}
    public void SingleItemButtonClick()
    {
        string spritename = _sprite.sprite.name;

        foreach (Itemdetail temp in _sourceList.items)
        {
            //Debug.Log(temp.id);
            if (spritename == temp.itemSprite.name)
            {
                UIitemDescription.text = temp.itemDescription;

                UIitemName.text = temp.itemName;
                UIitemQuality.text = temp.itemQuality;
                UIitemPrice.text = temp.itemPrice.ToString();
                id=temp.id;
            }


        }

    }
    
}
