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
/*1001����
  2001��Ƥ
  3001Ь��
  4001����
  

  ��ƷƷ��
  "<color=#FFFFFF>��ͨ</color>"
  "<color=#1C86EE>ϡ��</color>"
  "<color=#B23AEE>ʷʫ</color>"
  "<color=#EEEE00>��˵</color>"
 
 */