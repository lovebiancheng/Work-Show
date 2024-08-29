using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;



public delegate void MyDelegate(string message);


public class ShopMain : MonoBehaviour
{
    public enum ItemType
    {
        ALL,
        BATS,
        CLOTHES,
        SHOOES,
        TAOJIAO,
        CONSUMABLES
    }
    private ItemType itemType = ItemType.ALL;
    public List<PlayerData> partList = new List<PlayerData>();


    protected GameObject content;
    protected ItemSc itemsc;

    public Text UIitemPrice;
    public Text UImoney;
    public static int _money = Tools.money;

    public void CreatItem(int id)
    {

        foreach (Itemdetail item in itemsc.items)
        {
            if (item.id == id)
            {
                Itemdetail tempItemdetail = new Itemdetail();
                tempItemdetail = item;
                //创建预制体
                GameObject tempPrefab = GameObject.Instantiate(Resources.Load("Prefabs/ShopItem") as GameObject);
                
                //tempPrefab.transform.parent = content;
                tempPrefab.transform.SetParent(content.transform, false);
                Image tempPrefabSprite = tempPrefab.GetComponent<Image>();
                tempPrefabSprite.sprite = tempItemdetail.itemSprite;

            }
        }
    }
    public void CreatListItem(List<PlayerData> list)
    {
        if (list.Count <= 0)
        {
            return;
        }
        for (int i = 0; i < list.Count; i++)
        {
            CreatItem(list[i].itemid);
        }
    }
    public void ClickExectue(ItemType tempType)
    {

        switch (tempType)
        {
            case ItemType.ALL:
                partList = Tools.afterData;
                //Debug.Log(partList.Count);
                RemoveAllChildren(content);
                CreatListItem(partList);
                break;
            case ItemType.TAOJIAO:
                partList = SelectItems(Tools.afterData, 2);
                //Debug.Log(partList.Count);
                RemoveAllChildren(content);
                CreatListItem(partList);
                break;
            case ItemType.BATS:
                partList = SelectItems(Tools.afterData, 1);
                //Debug.Log(partList.Count);
                RemoveAllChildren(content);
                CreatListItem(partList);
                break;
            case ItemType.CLOTHES:
                partList = SelectItems(Tools.afterData, 4);
                Debug.Log(partList.Count);
                RemoveAllChildren(content);
                CreatListItem(partList);
                break;
            case ItemType.SHOOES:
                partList = SelectItems(Tools.afterData, 3);
                //Debug.Log(partList.Count);
                RemoveAllChildren(content);
                CreatListItem(partList);
                break;
            case ItemType.CONSUMABLES:
                partList = SelectItems(Tools.afterData, 5);
                //Debug.Log(partList.Count);
                RemoveAllChildren(content);
                CreatListItem(partList);
                break;
        }
    }
    public List<PlayerData> SelectItems(List<PlayerData> allData, int num)
    {
        List<PlayerData> partList = new List<PlayerData>();
        for (int i = 0; i < allData.Count; i++)
        {
            if (allData[i].itemid / 1000 == num)
            {
                partList.Add(allData[i]);
            }

        }
        return partList;
    }
    public void RemoveAllChildren(GameObject parent)
    {
        Transform transform;
        for (int i = 0; i < parent.transform.childCount; i++)
        {
            transform = parent.transform.GetChild(i);
            GameObject.Destroy(transform.gameObject);
        }
    }


    
    public static MyDelegate mydelegate;
    



    public void SortItem()
    {
        //默认按价格排序
        //按ID排序
        //实现玩家拖拽物品移动位置

    }

    //保存数据
    public void SaveBag()
    {

    }
    //在商店时卖出物品
    public void Sale()
    {

    }

    void Start()
    {
        itemsc = AssetDatabase.LoadAssetAtPath<ItemSc>("Assets/Scripts/UIControl/PlayerBag/ItemSc.asset");
        //Debug.Log(itemsc.name);
        content = GameObject.Find("ShopCanvas/ShopMainBg/Scroll View/Viewport/Content");
        UIitemPrice = GameObject.Find("ShopCanvas/ShopDetailBg/Coin/UIItemPrice").GetComponent<Text>();
        UImoney = GameObject.Find("ShopCanvas/CoinsImage/Money").GetComponent<Text>();
        UImoney.text=Tools.money.ToString();
    }

    // Update is called once per frame
    
    public void BUttonAllClick()
    {
        itemType = ItemType.ALL;
        ClickExectue(itemType);
    }
    public void ButtonBatClick()
    {
        itemType = ItemType.BATS;
        ClickExectue(itemType);
    }
    public void ButtonClothesClick()
    {
        itemType = ItemType.CLOTHES;
        ClickExectue(itemType);
    }
    public void ButtonShooesClick()
    {
        itemType = ItemType.SHOOES;
        ClickExectue(itemType);
    }
    public void ButtonTaoJiaoClick()
    {
        itemType = ItemType.TAOJIAO;
        ClickExectue(itemType);
    }
    public void ButtonConsumableClik()
    {
        itemType = ItemType.CONSUMABLES;
        ClickExectue(itemType);
    }
    public void ButtonBuyClick()
    {
        int temp=int.Parse(UIitemPrice.text);
        UImoney.text=ReduceMoney(temp).ToString();
        mydelegate(_money.ToString());
    }
    
    public int ReduceMoney(int i)
    {
        if ((_money-i)>=0)
        {
            _money = _money - i;
            AddItemInBag(ShopItemClick.id);
            return _money;
        }
        else
        {
            return _money;
        }
    }
    public void AddItemInBag(int id)
    {
        
        foreach(var temp in itemsc.items)
        {
            if (temp.id == id)
            {
                PlayerData tempdata = new PlayerData();
                tempdata.itemid = temp.id;
                tempdata.itemName = temp.itemName;
                Tools.dataitems.Add(tempdata);
            }
        }
    }
}
