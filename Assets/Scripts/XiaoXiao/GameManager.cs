using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;
using UnityEngine.SubsystemsImplementation;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{

    //单例模式
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                //GameObject temp= new GameObject(nameof(GameManager));
                //instance=temp.AddComponent<GameManager>();
                instance=GameObject.FindWithTag("GameManagerTag").GetComponent<GameManager>();

            }
            return instance;
        }
    }

    public enum ItemType
    {
        Empty,
        Normal,
        Special
    }
    [Serializable]
    public struct Cell
    {
        public ItemType itemType;
        public GameObject cell;
    }
    public Cell[] itemPrefabs;
    public GameObject backGround;
    public int xNum;
    public int yNum;
    
     
    private Dictionary<ItemType, GameObject> itemPrefabDict;

    private GameBat startBat;
    private GameBat endBat;

    private GameBat[,] bats;
    private List<GameBat> sameFirstBatList;
    private HashSet<Vector2Int> sameFirstBatHashSet;
    private List<GameBat> sameSecondBatList;
    private HashSet<Vector2Int> sameSecondBatHashSet;

    private Vector2Int firstVector;
    private Vector2Int secondVector;

    private FilingSystem _filing;

    private bool isDelete = true;
    // Start is called before the first frame update
    private void Awake()
    {
        bats = new GameBat[xNum, yNum];
        sameFirstBatList = new List<GameBat>();
        sameFirstBatHashSet = new HashSet<Vector2Int>();
        sameSecondBatList = new List<GameBat>();
        sameSecondBatHashSet = new HashSet<Vector2Int>();
        firstVector = new Vector2Int();
        secondVector = new Vector2Int();
        //实例化字典，并将不同状态添加进字典
        itemPrefabDict = new Dictionary<ItemType, GameObject>();
        _filing=GetComponent<FilingSystem>();
    }
    void Start()
    {
        //bats = new GameBat[xNum, yNum];
        //sameFirstBatList = new List<GameBat>();
        //sameFirstBatHashSet = new HashSet<Vector2Int>();
        //sameSecondBatList = new List<GameBat>();
        //sameSecondBatHashSet = new HashSet<Vector2Int>();
        //firstVector = new Vector2Int();
        //secondVector = new Vector2Int();
        ////实例化字典，并将不同状态添加进字典
        //itemPrefabDict = new Dictionary<ItemType, GameObject>();
        for(int i=0;i<itemPrefabs.Length; i++)
        {
            if (!itemPrefabDict.ContainsKey(itemPrefabs[i].itemType))
            {
                itemPrefabDict.Add(itemPrefabs[i].itemType, itemPrefabs[i].cell);
            }
        }
        CreatGrid(xNum, yNum);
        //这里后面要实现一个重要逻辑，即先读取表格，如果是空，进行创建
        CreatBat(xNum, yNum);

        //Bianliqiupai();
        
        
    }
    public void Bianliqiupai()
    {
        for (int i = 0; i < xNum; i++)
        {
            for (int j = 0; j < yNum; j++)
            {
                Debug.Log(bats[i, j].ColorBatComponent.ReadColorName());
            }
        }
        Debug.Log("+++++++++++++++++++++++++++++++++++++++");
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            //右键按下会进行一次删除
            if (sameFirstBatHashSet.Count>0&&sameFirstBatList.Count>0&&isDelete==false)
            {
                DestroyBat();
                DropBat();
                UpBat();
            }
              
        }
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    Debug.Log("w键按下");
            
        //    startBat.MoveBatComponent.MoveSinglePosition(bats[0,0]);
        //}
    }

    //生成网格
    public void CreatGrid(int m,int n)
    {
        for(int i=0;i<m;i++)
        {
            for(int j = 0; j < n; j++)
            {
                GameObject temp = Instantiate(itemPrefabDict[ItemType.Empty],new Vector3(5,5,0),Quaternion.identity,backGround.transform);
                temp.transform.localScale = new Vector3((float)0.1, (float)0.1, 0);
                temp.transform.localPosition=CorrectPosition(i,j);

            }
        }
    }
    //第一次生成球拍
    public void CreatBat(int m,int n)
    {
        for(int i=0;i<m; i++)
        {
            for(int j = 0; j < n; j++)
            {
                GameObject temp = Instantiate(itemPrefabDict[ItemType.Normal],new Vector3(5,5,0), Quaternion.identity,backGround.transform);
                temp.transform.localScale=new Vector3(7.8f, 7.8f, 0);
                temp.transform.localPosition =CorrectPosition(i,j);
                bats[i,j]=temp.GetComponent<GameBat>();
                bats[i, j].Deliver(i,j,ItemType.Normal);
                bats[i, j].ColorBatComponent.SetBatColor((ColorBat.ColorType)UnityEngine.Random.Range (0, bats[i, j].ColorBatComponent.ColorBats.Length));
                
            }
        }
        
    }
    //纠正位置
    public Vector3 CorrectPosition(int m,int n) 
    { 
        Vector3 startPosition= new Vector3();
        startPosition.x = -2.57f;
        startPosition.y = 4.87f;
        float aDistance = 1.09f;
        float bDistance = 0.74f;
        Vector3 resualt = new Vector3(startPosition.x+m*bDistance,startPosition.y-n*aDistance,0);
        return resualt;
        
    }
    
    
    //查找相同元素
    public void SearchSameFirstBat(Vector2Int currentVector)
    {
        if (currentVector.x >= 0 && currentVector.y >= 0&&currentVector.x<xNum&&currentVector.y<yNum)
        {
            sameFirstBatList.Add(bats[currentVector.x, currentVector.y]);
            sameFirstBatHashSet.Add(currentVector);
            List<Vector2Int> tempList = new List<Vector2Int>();
            tempList = CheckFour(currentVector);
            if(tempList.Count > 1)
            {
                for (int i = 0; i < tempList.Count; i++)
                {
                    if (tempList[i].x >= 0 && tempList[i].y >= 0 && tempList[i].x < xNum && tempList[i].y<yNum&& !sameFirstBatHashSet.Contains(tempList[i]) && bats[tempList[i].x, tempList[i].y]!=null)
                    {
                        if (bats[tempList[i].x, tempList[i].y].ColorBatComponent.ReadColorName() == bats[currentVector.x, currentVector.y].ColorBatComponent.ReadColorName())
                        {
                            SearchSameFirstBat(tempList[i]);
                        }
                    }
                    
                }
            }
            
        }
       
    }
    public void SearchSameSecondBat(Vector2Int currentVector)
    {
        if (currentVector.x >= 0 && currentVector.y >= 0 && currentVector.x < xNum && currentVector.y < yNum)
        {
            sameSecondBatList.Add(bats[currentVector.x, currentVector.y]);
            sameSecondBatHashSet.Add(currentVector);
            List<Vector2Int> tempList = new List<Vector2Int>();
            tempList = CheckFour(currentVector);
            if (tempList.Count > 1)
            {
                for (int i = 0; i < tempList.Count; i++)
                {
                    if (tempList[i].x >= 0 && tempList[i].y >= 0 && tempList[i].x < xNum && tempList[i].y < yNum && !sameSecondBatHashSet.Contains(tempList[i]) && bats[tempList[i].x, tempList[i].y] != null)
                    {
                        if (bats[tempList[i].x, tempList[i].y].ColorBatComponent.ReadColorName() == bats[currentVector.x, currentVector.y].ColorBatComponent.ReadColorName())
                        {
                            SearchSameSecondBat(tempList[i]);
                        }
                    }

                }
            }

        }

    }
    //查找基准位置周围的4个元素
    public List<Vector2Int> CheckFour(Vector2Int currentVector)
    {
        List<Vector2Int> fourList= new List<Vector2Int>();
        fourList.Add(currentVector+Vector2Int.right);
        fourList.Add(currentVector+Vector2Int.left);
        fourList.Add(currentVector-Vector2Int.up);
        fourList.Add(currentVector - Vector2Int.down);
        return fourList;
    }

    //判断两个物体是否相邻
    public bool IsNear(GameBat gameBat1,GameBat gameBat2)
    {
       
        if((gameBat1.X==gameBat2.X&& Mathf.Abs(gameBat1.Y-gameBat2.Y)==1)||(gameBat1.Y == gameBat2.Y && Mathf.Abs(gameBat1.X - gameBat2.X) == 1))
        {
            return true;
        }
        else
        {
            return false;
        }
        

    }
    //交换两个球拍位置
    public void  ExchangeBats(GameBat gameBat1, GameBat gameBat2)
    {
        //GameObject temp1=new GameObject(nameof(GameBat));
        //GameBat firstBat=temp1.AddComponent<GameBat>();
        GameBat firstBat = gameBat1;
        GameBat secondBat = gameBat2;
        int x1=firstBat.X;
        int y1=firstBat.Y;
        int x2=secondBat.X;
        int y2=secondBat.Y;

        bats[firstBat.X, firstBat.Y]= gameBat2;
        bats[secondBat.X,secondBat.Y] = gameBat1;
        //gameBat1.MoveBatComponent.Move(firstBat,secondBat);
        IEnumerator tempCoroutine= gameBat1.MoveBatComponent.MoveAB(firstBat, secondBat);
        StartCoroutine(tempCoroutine);
        //gameBat1.Deliver(secondBat.X,secondBat.Y);
        //gameBat2.Deliver(firstBat.X,firstBat.Y);
        gameBat1.Deliver(x2, y2);
        gameBat2.Deliver(x1,y1);
        //FindSame(gameBat1);
        

        firstVector=new Vector2Int();
        secondVector=new Vector2Int();
        firstVector.x = gameBat1.X;
        firstVector.y = gameBat1.Y;
        secondVector.x = gameBat2.X;
        secondVector.y = gameBat2.Y;
        
        //FindSameBat(firstVector);
        

    }
    //销毁
    public void DestroyBat()
    {
        if (sameFirstBatList.Count >= 2)
        {
            for (int i = 0; i < sameFirstBatList.Count; i++)
            {

                bats[sameFirstBatList[i].X, sameFirstBatList[i].Y] = null;
                Destroy(sameFirstBatList[i].gameObject);
            }
        }
        
        sameFirstBatHashSet.Clear();
        sameFirstBatList.Clear();

        if (sameSecondBatList.Count >= 2)
        {
            for (int i = 0; i < sameSecondBatList.Count; i++)
            {

                bats[sameSecondBatList[i].X, sameSecondBatList[i].Y] = null;
                Destroy(sameSecondBatList[i].gameObject);
            }
        }

        sameSecondBatHashSet.Clear();
        sameSecondBatList.Clear();


        isDelete = true;
    }
    
    public void DropBat()
    {
        
        int part1Num = 0;
       
        //这是检查上半部分
        while (part1Num<96)
        {
            for (int i = yNum/2 - 2; i >=0; i--)
            {
                Queue<GameBat> queue = new Queue<GameBat>();
                int count = 0;
                //找到每行不为空的 入队
                for (int j = 0; j < xNum; j++)
                {
                    if (bats[j, i] != null)
                    {
                        queue.Enqueue(bats[j, i]);
                        count++;

                    }
                }
                Debug.Log("&&&&&&&&&&&&&&&&&&" + count);
                

                //出队，并检查不对应下面的物品，如果为空，则向下移动
                for (int k = 0; k < count; k++)
                {
                    GameBat gameBat = queue.Dequeue();
                    if (bats[gameBat.X, gameBat.Y + 1] == null)
                    {
                        bats[gameBat.X, gameBat.Y] = null;
                       IEnumerator a=   gameBat.MoveBatComponent.MoveSingleDown(gameBat);
                        StartCoroutine(a);
                        gameBat.Deliver(gameBat.X, gameBat.Y + 1);
                        bats[gameBat.X, gameBat.Y] = gameBat;
                        //gameBat.MoveBatComponent.Move();
                        //movedPiece = true;

                    }
                }
                //检查顶行
                for (int m = 0; m < xNum; m++)
                {
                    GameBat bat = bats[m, 0];
                    if (bat == null)
                    {
                        GameObject newBat = Instantiate(itemPrefabDict[ItemType.Normal], new Vector3(5, 5, 0), Quaternion.identity, backGround.transform);
                        newBat.transform.localScale = new Vector3(7.8f, 7.8f, 0);
                        newBat.transform.localPosition = CorrectPosition(m, -2);
                        GameBat tempBat = newBat.GetComponent<GameBat>();
                        tempBat.Deliver(m, -1, ItemType.Normal);
                        tempBat.ColorBatComponent.SetBatColor((ColorBat.ColorType)UnityEngine.Random.Range(0, tempBat.ColorBatComponent.ColorBats.Length));

                        IEnumerator b=  tempBat.MoveBatComponent.MoveSingleDown(tempBat);
                        StartCoroutine(b);
                        tempBat.Deliver(tempBat.X, tempBat.Y + 1);
                        bats[m, 0] = tempBat;

                    }
                }
                part1Num += count;

            }
        }
        

    }
    public void UpBat()
    {
        int part2Num = 0;
        //这是检查下半部分
        while (part2Num < 96)
        {
            for (int i = yNum / 2 + 1; i < yNum; i++)
            {
                Queue<GameBat> queue = new Queue<GameBat>();
                int count = 0;
                //找到每行不为空的 入队
                for (int j = 0; j < xNum; j++)
                {
                    if (bats[j, i] != null)
                    {
                        queue.Enqueue(bats[j, i]);
                        count++;

                    }
                }
                Debug.Log("&&&&&&&&&&&&&&&&&&" + count);


                //出队，并检查不对应上面的物品，如果为空，则向上移动
                for (int k = 0; k < count; k++)
                {
                    GameBat gameBat = queue.Dequeue();
                    if (bats[gameBat.X, gameBat.Y - 1] == null)
                    {
                        bats[gameBat.X, gameBat.Y] = null;
                        IEnumerator a = gameBat.MoveBatComponent.MoveSingleUp(gameBat);
                        StartCoroutine(a);
                        gameBat.Deliver(gameBat.X, gameBat.Y - 1);
                        bats[gameBat.X, gameBat.Y] = gameBat;
                        //gameBat.MoveBatComponent.Move();
                        //movedPiece = true;

                    }
                }
                //检查顶行
                for (int m = 0; m < xNum; m++)
                {
                    GameBat bat = bats[m, 9];
                    if (bat == null)
                    {
                        GameObject newBat = Instantiate(itemPrefabDict[ItemType.Normal], new Vector3(5, 5, 0), Quaternion.identity, backGround.transform);
                        newBat.transform.localScale = new Vector3(7.8f, 7.8f, 0);
                        newBat.transform.localPosition = CorrectPosition(m, 11);
                        GameBat tempBat = newBat.GetComponent<GameBat>();
                        tempBat.Deliver(m, 10, ItemType.Normal);
                        tempBat.ColorBatComponent.SetBatColor((ColorBat.ColorType)UnityEngine.Random.Range(0, tempBat.ColorBatComponent.ColorBats.Length));

                        IEnumerator b = tempBat.MoveBatComponent.MoveSingleUp(tempBat);
                        StartCoroutine(b);
                        tempBat.Deliver(tempBat.X, tempBat.Y - 1);
                        bats[m, 9] = tempBat;

                    }
                }
                part2Num += count;

            }
        }
    }


    //鼠标的三种状态传递
    public void SecondBat(GameBat temp)
    {
        endBat = temp;
        //Debug.Log("第2个的坐标" + endBat.Y);
            

    }
    public void FirstBat(GameBat temp)
    {
        if (isDelete == true)
        {
            startBat = temp;
            //可以播放选中动画
            Debug.Log("第一个的坐标" + startBat.Y);
        }  
    }
    
    public void ReleaseBat()
    {
        
        //如果两个物体相邻，即可进行交换，否则不行
        if (IsNear(startBat, endBat)&&isDelete==true)
        {
            ExchangeBats(startBat, endBat);
            
            
        }
        if (isDelete==true)
        {
            SearchSameFirstBat(firstVector);
            SearchSameSecondBat(secondVector);
            isDelete = false;
        }
        
       


    }
    public void EndGame()
    {
        Application.Quit();
    }
    public void ReturnBegainingGame()
    {
        SceneManager.LoadScene(0);
    }
}
