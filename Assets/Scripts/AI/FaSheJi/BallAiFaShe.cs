using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallAiFaShe : MonoBehaviour
{
    public GameObject ball;
    public Vector3 ballVector;
    public string ballName = "PingPongBall";
    public Rigidbody ballcRigidbody;
    public GameObject ballFirePosition;

    public float speed = 6.0f;

    public float speed2 = 5.0f;
    private void Start()
    {
        ball = Resources.Load<GameObject>("Prefabs/Ball");
        ballFirePosition = GameObject.Find("BallPosition");
        
    }
    public void Fire(float forwardSpeend,float downSpeed)
    {
        //首先创建在对应位置
        GameObject ballc= Cache.Instance.CreatObject(ballName, ball, ballVector, Quaternion.LookRotation(ballFirePosition.transform.up));
        ballcRigidbody = ballc.GetComponent<Rigidbody>();
        ballcRigidbody.velocity = ballc.transform.forward*forwardSpeend+ballc.transform.up*(-downSpeed);
        //Debug.Log("------------" + ballVector.normalized);
        //ballcRigidbody.useGravity = true;
        
    }
    public void RotateSelf(float tempRotate)
    {
        gameObject.transform.Rotate(new Vector3(0,0,tempRotate));
    }
    private void Update()
    {
        ballVector=ballFirePosition.transform.position;
        if(Input.GetMouseButtonDown(1))
        {
            RotateSelf(10f);//这个值只能在30到-30之间
        }
        if(Input.GetMouseButtonDown(0))
        {
            Fire(speed,speed2);
        }
    }
}
