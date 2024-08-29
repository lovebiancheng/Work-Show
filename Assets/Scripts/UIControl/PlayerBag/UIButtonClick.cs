using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonClick :MonoBehaviour
{
    private Button _button;
    private Image _sprite;
    private ItemSc _sourceList;
    public Text UIitemName;
    public Text UIitemQuality;
    public Text UIitemDescription;
    private void Start()
    {
        _sprite = GetComponent<Image>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SingleItemButtonClick); 
        _sourceList = AssetDatabase.LoadAssetAtPath<ItemSc>("Assets/Scripts/UIControl/PlayerBag/ItemSc.asset");
        UIitemName = GameObject.Find("PlayerBag/DetailBg/UIItemName").GetComponent<Text>();
        UIitemQuality = GameObject.Find("PlayerBag/DetailBg/UIItemQuality").GetComponent<Text>();
        UIitemDescription = GameObject.Find("PlayerBag/DetailBg/UIItemDescription").GetComponent<Text>();
    }
    public void SingleItemButtonClick()
    {
        string spritename = _sprite.sprite.name;

        foreach (Itemdetail temp in _sourceList.items)
        {
            Debug.Log(temp.id);
            if (spritename == temp.itemSprite.name)
            {
                UIitemDescription.text = temp.itemDescription;

                UIitemName.text = temp.itemName;
                UIitemQuality.text = temp.itemQuality;
                //UIitemQuality.color = Color.yellow;
            }


        }

    }
    
}
