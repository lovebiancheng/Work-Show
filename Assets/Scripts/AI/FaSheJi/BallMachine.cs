using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMachine : MonoBehaviour
{
    private static BallMachine _instance;
    public static BallMachine Instance
    {
        get 
        { 
            if (_instance == null)
                _instance = new BallMachine();
            return _instance; 
        }
    }
    public string ballName = "PingPongBall";
    public GameObject ballGameObject;


    private void Awake()
    {
        ballGameObject = Resources.Load<GameObject>("Prefabs/Ball");
    }
    public void CreatBall()
    {
        GameObject tempBall = Cache.Instance.CreatObject(ballName, ballGameObject, transform.position, transform.rotation);
        tempBall.AddComponent<Ball>();
        tempBall.GetComponent<Ball>().Interlize(new Vector3(0, 0, -4), new Vector3(1, 0, 0), 30f);
        tempBall.GetComponent<Ball>().Fire();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            CreatBall();
        }
    }
}
