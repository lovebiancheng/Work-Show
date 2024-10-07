using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatTables : MonoBehaviour
{

    public int horizontalNum=7;
    public int verticalNum=5;
    public Vector3 originalVector=new Vector3(-18f,0.769f,20f);
    public float horiaontalDistance = 6;
    public float verticalDistance = 10;

    public GameObject rioTableObject;
    public GameObject londonTableObject;
    public GameObject tokyoTableObject;
    
    private void Awake()
    {
        rioTableObject = Resources.Load<GameObject>("Prefabs/TablesPrefab/RioTable");
        londonTableObject= Resources.Load<GameObject>("Prefabs/TablesPrefab/LondonTable");
        tokyoTableObject= Resources.Load<GameObject>("Prefabs/TablesPrefab/TokyoTable");
    }
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < verticalNum; i++)
        {
            if(i<2)
            {
                for (int j = 0; j < horizontalNum; j++)
                {
                    GameObject temp = GameObject.Instantiate(rioTableObject, gameObject.transform);
                    temp.transform.localPosition = new Vector3(originalVector.x + j * horiaontalDistance, 0.769f, originalVector.z - i * verticalDistance);
                }
            }
            if(i==2)
            {
                for (int j = 0; j < horizontalNum; j++)
                {
                    GameObject temp = GameObject.Instantiate(tokyoTableObject, gameObject.transform);
                    temp.transform.localPosition = new Vector3(originalVector.x + j * horiaontalDistance, 0.769f, originalVector.z - i * verticalDistance);
                }
            }
            if (i > 2)
            {
                for (int j = 0; j < horizontalNum; j++)
                {
                    GameObject temp = GameObject.Instantiate(londonTableObject, gameObject.transform);
                    temp.transform.localPosition = new Vector3(originalVector.x + j * horiaontalDistance, 0.769f, originalVector.z - i * verticalDistance);
                }
            }
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
