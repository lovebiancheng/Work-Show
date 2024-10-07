using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UIMainBag :MonoBehaviour
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
    private ItemType itemType=ItemType.ALL;
    public List<PlayerData> partList = new List<PlayerData>();



    protected GameObject content;
    protected ItemSc itemsc;
    public Text nameText;
    public Text qualityText;
    public Text descriptionText;
    //读取玩家背包数据
    public void ReadBagData()
    {
        for(int i = 0; i < Tools.afterData.Count; i++)
        {
            Debug.Log(Tools.afterData[i]);
        }
    }

    public void CreatItem(int id)
    {
       
        foreach (Itemdetail item in itemsc.items)
        {
            if (item.id == id)
            {
                Itemdetail tempItemdetail = new Itemdetail();
                tempItemdetail= item;
                //创建预制体
                GameObject tempPrefab = GameObject.Instantiate(Resources.Load("Prefabs/ItemImage") as GameObject);
                
                //tempPrefab.transform.parent = content;
                tempPrefab.transform.SetParent(content.transform, false);
                Image  tempPrefabSprite = tempPrefab.GetComponent<Image>();
                Debug.Log(tempItemdetail.itemSprite.name);
                tempPrefabSprite.sprite = tempItemdetail.itemSprite;

            }
        }
    }
    public void CreatListItem(List<PlayerData> list)
    {
        if(list.Count<= 0)
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
                Debug.Log(partList.Count);
                RemoveAllChildren(content);
                CreatListItem(partList);
                break;
            case ItemType.TAOJIAO:
                partList = SelectItems(Tools.afterData,2);
                Debug.Log(partList.Count);
                RemoveAllChildren(content);
                CreatListItem(partList);
                break;
            case ItemType.BATS:
                partList = SelectItems(Tools.afterData, 1);
                Debug.Log(partList.Count);
                RemoveAllChildren(content);
                CreatListItem(partList);
                break;
            case ItemType.CLOTHES:
                partList=SelectItems(Tools.afterData, 4);
                Debug.Log(partList.Count);
                RemoveAllChildren(content);
                CreatListItem(partList);
                break;
            case ItemType.SHOOES:
                partList = SelectItems(Tools.afterData, 3);
                Debug.Log(partList.Count);
                RemoveAllChildren(content);
                CreatListItem(partList);
                break;
            case ItemType.CONSUMABLES:
                partList = SelectItems(Tools.afterData, 5);
                Debug.Log(partList.Count);
                RemoveAllChildren(content);
                CreatListItem(partList);
                break;
        }
    }
    public List<PlayerData> SelectItems(List<PlayerData> allData,int num)
    {
        List<PlayerData> partList = new List<PlayerData>();
        for (int i = 0; i < allData.Count; i++)
        {
            if (allData[i].itemid/1000==num)
            {
                partList.Add(allData[i]);
            }
            
        }
        return partList;
    }
    public void RemoveAllChildren(GameObject parent)
    {
        Transform transform;
        for(int i = 0;i < parent.transform.childCount;i++)
        {
            transform=parent.transform.GetChild(i);
            GameObject.Destroy(transform.gameObject);
        }
    }






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

        //Init();

        itemsc = AssetDatabase.LoadAssetAtPath<ItemSc>("Assets/Scripts/UIControl/PlayerBag/ItemSc.asset");
        Debug.Log(itemsc.name);
        content = GameObject.Find("MainBg/Scroll View/Viewport/Content");
    }

    // Update is called once per frame
    void Update()
    {
        //if (EventSystem.current.IsPointerOverGameObject())
        //{
        //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hitInfo;
        //    if (Physics.Raycast(ray, out hitInfo))
        //    {
        //        GameObject rayGameobject = hitInfo.collider.gameObject;
        //        Debug.Log(rayGameobject.name);
        //    }
        //    Debug.Log("dangqianchuyumougeuishang");
        //}


        //if (Input.GetKeyDown(KeyCode.K))
        //{

        //    for (int i = 0; i < Tools.afterData.Count; i++)
        //    {
        //        CreatItem(Tools.afterData[i].itemid);
        //    }
        //}
    }
    public void AllClick()
    {
        itemType = ItemType.ALL;
        ClickExectue(itemType);
    }
    public void BatClick()
    {
        itemType = ItemType.BATS;
        ClickExectue(itemType);
    }
    public void ClothesClick()
    {
        itemType = ItemType.CLOTHES;
        ClickExectue(itemType);
    }
    public void ShooesClick()
    {
        itemType = ItemType.SHOOES;
        ClickExectue(itemType);
    }
    public void TaoJiaoClick()
    {
        itemType = ItemType.TAOJIAO;
        ClickExectue(itemType);
    }
    public void ConsumableClik()
    {
        itemType = ItemType.CONSUMABLES;
        ClickExectue(itemType);
    }
}
