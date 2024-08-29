using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class Itemdetail
{
    public int id;
    public string itemName;
    public string itemQuality;
    public Sprite itemSprite;
    public string itemDescription;
    public int itemPrice;
    public int itemSpeed;
    public int itemRotation;
    public int itemArc;
    public int itemHardness;
    public int moveSpeed;
}

[CreateAssetMenu(menuName ="ScriptableObject/Item",order = 0)]
public class ItemSc : ScriptableObject
{
    
    public List<Itemdetail> items = new List<Itemdetail>();
}
/*1001球拍
  2001胶皮
  3001鞋子
  4001短袖
  

  物品品质
  "<color=#FFFFFF>普通</color>"
  "<color=#1C86EE>稀少</color>"
  "<color=#B23AEE>史诗</color>"
  "<color=#EEEE00>传说</color>"
 
 */