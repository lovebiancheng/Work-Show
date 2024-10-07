using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatDangBan : MonoBehaviour
{
    public int horizontalNum = 23;
    public int verticalNum = 6;
    public Vector3 originalVector = new Vector3(-22f, 0.41f, 25f);
    public float horiaontalDistance = 2f;
    public float verticalDistance = 10f;

    public GameObject dangBan;
    

    private void Awake()
    {
        dangBan = Resources.Load<GameObject>("Prefabs/TablesPrefab/DangBan");
        
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < verticalNum; i++)
        {
            for (int j = 0; j < horizontalNum; j++)
            {
                GameObject temp = GameObject.Instantiate(dangBan, gameObject.transform);
                temp.transform.localPosition = new Vector3(originalVector.x + j * horiaontalDistance, 0.41f, originalVector.z - i * verticalDistance);
            }

        }
    }
}
