using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class EliminationGameManager : MonoBehaviour
{
    public int XNum;
    public int YNum;
    public float XDistance;
    public float YDistance;
    public List<GameObject> batList;
    public Vector2 bornPoint;
    public bool IsSecond;
    public List<GameObject> gridList;
    public int allNum;
    public int destoryNum;
    public UnityEvent changeRemoveNumberEvent;

    private GameObject batPrefab;
    private GameObject gridPrefab;
    private BatScriptTable batScriptTable;
    private List<GameObject> sameFirstList;
    private HashSet<GameObject> sameFirstHashSet;
    private List<GameObject> sameSecondList;
    private HashSet<GameObject> sameSecondHashSet;
    private GameObject startGameObject;
    private GameObject secondGameObject;

    private static EliminationGameManager _instance;
    public static EliminationGameManager Instance
    {
        get 
        { 
            if (_instance == null)
            {
                _instance =GameObject.FindWithTag("EliminationGameManager").GetComponent<EliminationGameManager>();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        batList = new List<GameObject>();
        gridList = new List<GameObject>();
        sameFirstList = new List<GameObject>();
        sameFirstHashSet = new HashSet<GameObject>();
        sameSecondList = new List<GameObject>();
        sameSecondHashSet=new HashSet<GameObject>();
        batPrefab = Resources.Load<GameObject>("Prefabs/EliminationPrefabs/BatPrefab");
        gridPrefab = Resources.Load<GameObject>("Prefabs/EliminationPrefabs/GridPrefab");
        batScriptTable = Resources.Load<BatScriptTable>("AllDatas/EliminationData/BaseBatsList");
        allNum = XNum * YNum;
    }

    private void Start()
    {
        CreatGrids();
        CreatBatList();
    }

    public void ChangeRemoveNumber()
    {
        changeRemoveNumberEvent?.Invoke();
    }
    public void CreatGrids()
    {
        for (int i = 0; i < YNum; i++)
        {
            for (int j = 0; j < XNum; j++)
            {
                float X = bornPoint.x + j * XDistance;
                float Y = bornPoint.y - i * YDistance;
                GameObject tempBat = GameObject.Instantiate(gridPrefab, gameObject.transform);
                tempBat.transform.localPosition = new Vector3(X, Y, 0f);
                gridList.Add(tempBat);
            }
        }
    }
    public void CreatBatList()
    {
        for (int i = 0; i <YNum; i++)
        {
            for(int j = 0; j <XNum; j++)
            {
                float X = bornPoint.x + j * XDistance;
                float Y = bornPoint.y - i * YDistance;
                GameObject tempBat=GameObject.Instantiate(batPrefab,gameObject.transform);
                Vector3 position=new Vector3(X, Y, 0f);
                tempBat.transform.localPosition = position;
                ColorBat(tempBat);
                SetBat(tempBat,j,i,position);
                //Debug.Log(tempBat.transform.position + "||||||||" + tempBat.transform.localPosition);
            }
        }
    }
    public void ColorBat(GameObject tempGameObject)
    {
        Image tempSprite=tempGameObject.GetComponent<Image>();
        Bat bat = tempGameObject.GetComponent<Bat>();
        int index = UnityEngine.Random.Range(0,batScriptTable.baseBatsList.Count);
        tempSprite.sprite = batScriptTable.baseBatsList[index].sprite;
        bat.batColor = batScriptTable.baseBatsList[index].batColor;
        bat.batType = batScriptTable.baseBatsList[index].batType;
    }
    public void SetBat(GameObject tempGameObject,int XIndex,int YIndex,Vector3 position)
    {
        Bat bat = tempGameObject.GetComponent<Bat>();//Vector3(-240,463,0)
        bat.X = XIndex + 1;
        bat.Y = YIndex + 1;
        bat.batPosition = position;
        bat.batIndex=batList.Count;
        batList.Add(tempGameObject);
    }

    public void SearchSameFirstBat(GameObject tempGameObject)
    {
        
        sameFirstList.Add(tempGameObject);
        sameFirstHashSet.Add(tempGameObject);
        //int indexPosition = FindBatInList(tempGameObject);
        Bat bat = tempGameObject.GetComponent<Bat>();
        if ( bat.batIndex>= 0 && bat.batIndex < batList.Count)
        {
            List<int> tempList = new();
            tempList = CheckFour(bat.batIndex,tempList);
            for(int i = 0; i < tempList.Count; i++)
            {
                if (!sameFirstHashSet.Contains(batList[tempList[i]]) && bat.batColor == batList[tempList[i]].GetComponent<Bat>().batColor)
                {
                    SearchSameFirstBat(batList[tempList[i]]);
                }
            }

        }
    }
    public void SearchSameSecondeBat(GameObject tempGameObject)
    {

        sameSecondList.Add(tempGameObject);
        sameSecondHashSet.Add(tempGameObject);
        //int indexPosition = FindBatInList(tempGameObject);
        Bat bat = tempGameObject.GetComponent<Bat>();
        if (bat.batIndex >= 0 && bat.batIndex < batList.Count)
        {
            List<int> tempList = new();
            tempList = CheckFour(bat.batIndex, tempList);
            for (int i = 0; i < tempList.Count; i++)
            {
                if (!sameSecondHashSet.Contains(batList[tempList[i]]) && bat.batColor == batList[tempList[i]].GetComponent<Bat>().batColor)
                {
                    SearchSameSecondeBat(batList[tempList[i]]);
                }
            }

        }
    }
    public List<int> CheckFour(int index,List<int> list)
    {

        if(index%XNum == 0)//左边
        {

        }
        else
        {
            int  left = index - 1;
            list.Add(left);
        }
        if(index%XNum==XNum-1)//右边
        {

        }
        else
        {
            int right = index + 1;
            list.Add(right);
        }
        if(index>=0&&index<XNum)//上边
        {

        }
        else
        {
            int up = index - XNum;
            list.Add(up);
        }
        if (index >batList.Count - XNum-1 && index < batList.Count)//下边
        {

        }
        else
        {
            int down = index+XNum;
            list.Add(down);
        }
        return list;
    }
    public int FindBatInList(GameObject tempGameobject)
    {
        int index = -1;
        for(int i=0; i<batList.Count; i++)
        {
            if (batList[i] == tempGameobject)
            {
                index = i;
                return index;
                
            }
        }
        return -1;
    }

    public void ExchangeBats(GameObject object1,GameObject object2)
    {
        Bat bat1 = object1.GetComponent<Bat>();
        Bat bat2 = object2.GetComponent<Bat>();
        Vector3 vector1 = bat1.batPosition;
        Vector3 vector2= bat2.batPosition;
        int batIndex1 = bat1.batIndex;
        int batIndex2 = bat2.batIndex;
        StartCoroutine(MoveCouplePosition(object1.transform.localPosition, object2.transform.localPosition));
        object1.transform.localPosition = vector2;
        object2.transform.localPosition = vector1;
        ChangeBaseInformation(bat1 ,bat2);
        var temp = batList[batIndex1];
        batList[batIndex1] = batList[batIndex2];
        batList[batIndex2] = temp;

        
    }
    public void ChangeBaseInformation(Bat bat1,Bat bat2)
    {
        int x1 = bat1.X;
        int  y1=bat1.Y;
        int index1 = bat1.batIndex;
        Vector2 vector1= bat1.batPosition;

        bat1.X=bat2.X; 
        bat1.Y=bat2.Y;
        bat1.batIndex = bat2.batIndex;
        bat1.batPosition = bat2.batPosition; 


        bat2.X=x1;
        bat2.Y=y1;
        bat2.batIndex=index1;
        bat2.batPosition=vector1;

    }
    public void ChangeSingleDownBaseInformation(Bat bat,Vector3 newVector,int index)
    {
        bat.batPosition = newVector;
        bat.Y += 1;
        bat.batIndex = index;
    }
    public void ChangeSingleUpBaseInformation(Bat bat,Vector3 newVector,int index)
    {
        bat.batPosition = newVector;
        bat.Y -= 1;
        bat.batIndex = index;
    }

    public void MoveSingle(GameObject startObject,Vector3 endPosition)
    {
        StartCoroutine(MoveSingleDownPosition(startObject.transform.localPosition,endPosition));
        startObject.transform.localPosition = endPosition;
    }


    public IEnumerator MoveSingleDownPosition(Vector3 startPosition, Vector3 endPosition)
    {
        float distance=Vector3.Distance(startPosition,endPosition)/1000f;
        while (Vector3.Distance(startPosition, endPosition) > 0.01f)
        {
            if (startPosition.y > endPosition.y)
            {
                startPosition.y -= distance;
            }
            if(startPosition.y < endPosition.y)
            {
                startPosition.y += distance;
            }
            yield return new WaitForSeconds(1f);
        }
        
    }
    public IEnumerator MoveCouplePosition(Vector3 startPosition, Vector3 endPosition)
    {
        Vector3 start=startPosition;
        Vector3 end=endPosition;
        float distance = Vector3.Distance(start, end) / 1000f;
        while (Vector3.Distance(startPosition, end) > 0.01f && Vector3.Distance(endPosition, start) > 0.01f)
        {
            if (start.x == end.x)
            {
                if(start.y>end.y)
                {
                    startPosition.y-=distance;
                    endPosition.y+=distance;
                }
                if (start.y<end.y)
                {
                    startPosition.y += distance;
                    endPosition.y+=distance;
                }
                
            }
            if (start.y == end.y)
            {
                if (start.x > end.x)
                {
                    startPosition.x -= distance;
                    endPosition.x += distance;
                }
                if (start.x < end.x)
                {
                    startPosition.x += distance;
                    endPosition.x += distance;
                }
                
            }

            yield return new WaitForSeconds(1f);
        }
    }
    public void FirstBat(GameObject tempGameObject)
    {
        startGameObject = tempGameObject;
        //Debug.Log("第一个"+startGameObject.GetComponent<Bat>().batPosition);
        //SearchSameFirstBat(startGameObject);
        //Debug.Log(sameFirstList.Count);
        
        
    }
    public void SecondBat(GameObject tempGameObject)
    {
        secondGameObject = tempGameObject;
        //Debug.Log("第二个" + secondGameObject.GetComponent<Bat>().X + "))))" + secondGameObject.GetComponent<Bat>().Y);
        //SearchSameSecondeList(secondGameObject);
        //Debug.Log(sameSecondList.Count);
       
    }

    public bool IsNear(GameObject tempGameObject1)
    {
        Bat bat1 = tempGameObject1.GetComponent<Bat>();
        Bat startBat=startGameObject.GetComponent<Bat>();
        if(Mathf.Abs(bat1.X-startBat.X)==1 ||Mathf.Abs(bat1.Y-startBat.Y)==1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //查找两个位置处和它们颜色相同的
    public void SerachAll(int startIndex, int secondIndex)
    {
        if (startGameObject.GetComponent<Bat>().batColor == secondGameObject.GetComponent<Bat>().batColor)
        {
            SearchSameFirstBat(batList[secondIndex]);
        }
        else
        {
            SearchSameFirstBat(batList[secondIndex]);
            SearchSameSecondeBat(batList[startIndex]);
        }

    }  
    public bool IsOnly()
    {

        if (sameFirstList.Count == 1 && sameSecondList.Count == 1)
        {
            return true;
        }
        return false;
    }
    public int DestoryGameObject()
    {
        int tempDestoryNum = 0;
            if (sameFirstList.Count > 1)
            {
                for (int i = 0; i < sameFirstList.Count; i++)
                {
                    int index = sameFirstList[i].GetComponent<Bat>().batIndex;
                    GameObject.Destroy(batList[index]);
                    tempDestoryNum++;
                    batList[index] = null;
                }
            }
            if (sameSecondList.Count > 1)
            {
                for (int i = 0; i < sameSecondList.Count; i++)
                {
                    int index = sameSecondList[i].GetComponent<Bat>().batIndex;
                    GameObject.Destroy(batList[index]);
                    tempDestoryNum++;
                    batList[index] = null;
                    //GameObject.Destroy(sameSecondList[i]);
                }
            }
            sameFirstHashSet.Clear();
            sameSecondHashSet.Clear();
            sameFirstList.Clear();
            sameSecondList.Clear();
        //int num = 0;
        //for (int i = 0; i < batList.Count; i++)
        //{
        //    Debug.Log(i + "---->" + batList[i]);
        //    if (batList[i] == null)
        //    {
        //        num++;
        //        Debug.Log("空的位置" + "---------" + i);
        //    }
        //}
        //Debug.Log("空的数量" + num);
        return tempDestoryNum;
    }
    
    public void DropBat()
    {
        
        int allNum = XNum * YNum;
        int startNum = allNum / 2;
        for(int i=startNum-1-XNum;i>=0;i-=XNum)
        {
            
            Queue<GameObject> queue= new Queue<GameObject>();
            int count = 0;
            for(int j = i; j > i - XNum; j--)
            {
               
                if (batList[j] != null)
                {
                    queue.Enqueue(batList[j]);
                    count++;
                }
            }
            
            //Debug.Log("------------");
            for(int k = 0; k <count; k++)
            {
                GameObject tempgameObject = queue.Dequeue();
                int index = tempgameObject.GetComponent<Bat>().batIndex;
                int newIndex = index + XNum;
                if (newIndex < allNum && newIndex >= 0 && batList[newIndex]==null)
                {
                    Vector3 newVector = gridList[newIndex].transform.localPosition;
                    MoveSingle(batList[index], newVector);
                    
                    ChangeSingleDownBaseInformation(batList[index].GetComponent<Bat>(),newVector, newIndex);
                    batList[newIndex] = batList[index];
                    batList[index] = null;
                }
            }
            
        }
    }

   
    public void UpBat()
    {

        int allNum = XNum * YNum;
        int startNum = allNum / 2;
        for (int i = startNum + XNum; i <allNum; i += XNum)
        {

            Queue<GameObject> queue = new Queue<GameObject>();
            int count = 0;
            for (int j = i; j < i + XNum; j++)
            {

                if (batList[j] != null)
                {
                    queue.Enqueue(batList[j]);
                    count++;
                }
            }

            
            for (int k = 0; k < count; k++)
            {
                GameObject tempgameObject = queue.Dequeue();
                int index = tempgameObject.GetComponent<Bat>().batIndex;
                int newIndex = index - XNum;
                if (newIndex < allNum && newIndex >= 0 && batList[newIndex] == null)
                {
                    Vector3 newVector = gridList[newIndex].transform.localPosition;
                    MoveSingle(batList[index], newVector);
                    ChangeSingleUpBaseInformation(batList[index].GetComponent<Bat>(), newVector,newIndex);
                    batList[newIndex] = batList[index];
                    batList[index] = null;
                }
            }
            
        }
    }
    public void MoveBat()
    {
        for (int i = 0; i < 5; i++)
        {
            UpBat();
            DropBat();
            CheckUpAndDown();
        }
    }
    public void CheckUpAndDown()
    {
        for(int i = 0;i < XNum;i++)
        {
            if (batList[i] == null)
            {
                GameObject tempBat = GameObject.Instantiate(batPrefab, gameObject.transform);
                ColorBat(tempBat);
                tempBat.transform.localPosition = gridList[i].transform.localPosition;
                Bat bat= tempBat.GetComponent<Bat>();
                bat.batIndex = i;
                bat.batPosition = gridList[i].transform.localPosition;
                batList[i] = tempBat;
            }
        }
        for(int j=allNum-XNum-1; j<allNum;j++)
        {
            if (batList[j] == null)
            {
                GameObject tempBat = GameObject.Instantiate(batPrefab, gameObject.transform);
                ColorBat(tempBat);
                tempBat.transform.localPosition = gridList[j].transform.localPosition;
                Bat bat = tempBat.GetComponent<Bat>();
                bat.batIndex = j;
                bat.batPosition = gridList[j].transform.localPosition;
                batList[j] = tempBat;
            }
        }
    }



    public void Exchange()
    {
        int startIndex= startGameObject.GetComponent<Bat>().batIndex;
        int secondIndex=secondGameObject.GetComponent<Bat>().batIndex;
        ExchangeBats(startGameObject, secondGameObject);
        SerachAll(startIndex, secondIndex);
        if (IsOnly())
        {
            ExchangeBats(startGameObject, secondGameObject);
        }
        destoryNum=DestoryGameObject();
        Debug.Log("消除数量"+destoryNum);
        ChangeRemoveNumber();
        MoveBat();
    }
}
