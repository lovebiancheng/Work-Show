using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerStates
{
    Idle,
    Wale,
    Run,
    Jump,

}
public enum ControlType
{
    NORMAL,
    MATCH
}


public class PlayerCtroller : MonoBehaviour
{
    public Transform playerTra;
    public Transform cameraTransform;
    public Animator playerAni;

    public float normalSpeed = 1f;
    public float rotateSpeed = 90f;
    public float jumpDistance=1;

    public float matchSpeed = 5f;
    
    public ControlType controlType = ControlType.NORMAL;
    public Text typeName;
    public bool gameType=false;

    public bool isGround = false;
    public Vector3 _forwaredTarget;


    



    public Rigidbody rig;
    void Start()
    {
        playerTra = transform;
        rig = GetComponent<Rigidbody>();
        playerAni= GetComponent<Animator>();
        cameraTransform = GameObject.Find("Main Camera").transform;
        
    }

    // Update is called once per frame
    void Update()
    {
        Ray ray = new Ray(transform.position,-transform.up);
        RaycastHit rayHit;
        if(Physics.Raycast(ray, out rayHit,0.1f) )
        {
           isGround = true;
            Debug.Log("chupengdaodimian");
        }
        
        if(controlType == ControlType.NORMAL)
        {
            PlayerNormalMove();
        }
        if (controlType == ControlType.MATCH)
        {
            PlayerMatchMove();
        }
        
        
    }


    public void ShiftButtonClick()
    {
        gameType =!gameType;
        if(gameType==true)
        {
            typeName.text = "Match";
            playerAni.SetBool("IsMatch", true);
            controlType = ControlType.MATCH;
        }
        if (gameType == false)
        {
            typeName.text = "Normal";
            playerAni.SetBool("IsMatch", false);
            controlType = ControlType.NORMAL;
        }

        
    }

    public void PlayerNormalMove()
    {
        //向前
        if (Input.GetKey(KeyCode.W))
        {
            RotateAngles(0f);
            MoveForwared(normalSpeed);
            playerAni.SetInteger("NormalNumber", 2); 
        }
        
        //向后
        if (Input.GetKey(KeyCode.S))
        {
            RotateAngles(180f);
            MoveForwared(normalSpeed);
            playerAni.SetInteger("NormalNumber", 2);
        }
        //向左
        if (Input.GetKey(KeyCode.A))
        {

            RotateAngles(-90f);
            MoveForwared(normalSpeed);
            playerAni.SetInteger("NormalNumber", 2);
        }
        //向右
        if (Input.GetKey(KeyCode.D))
        {
            RotateAngles(90f);
            MoveForwared(normalSpeed);
            playerAni.SetInteger("NormalNumber", 2);
        }
        //W,A,S,D抬起
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            playerAni.SetInteger("NormalNumber", 0);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAni.SetTrigger("Jump");


        }

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    if(isGround)
        //    {
        //        rig.velocity = new Vector3(0, (float)Math.Sqrt(jumpDistance * 9.8f * 2), 0);
        //        isGround = false;
        //        playerAni.SetInteger("NormalNumber", 0);
        //    }


        //}       
    }

    public void PlayerMatchMove()
    {
        //向前
        if (Input.GetKey(KeyCode.W))
        {
            playerAni.SetBool("IsYiDong",true);
            MoveForwared(matchSpeed);
            playerAni.SetInteger("YiDongNumber", 1);
            Debug.Log("xiangqianxiangqian");
        }

        //向后
        if (Input.GetKey(KeyCode.S))
        {
            playerAni.SetBool("IsYiDong", true);
            MoveForwared(-matchSpeed);
            playerAni.SetInteger("YiDongNumber", 0);
        }
        //向左
        if (Input.GetKey(KeyCode.A))
        {

            playerAni.SetBool("IsYiDong", true);
            MatchMove(-matchSpeed);
            playerAni.SetInteger("YiDongNumber", 6);
            
        }
        //向右
        if (Input.GetKey(KeyCode.D))
        {
            playerAni.SetBool("IsYiDong", true);
            MatchMove(matchSpeed);
            playerAni.SetInteger("YiDongNumber", 5);
        }
        //W,A,S,D抬起
        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            playerAni.SetBool("IsYiDong", false);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerAni.SetBool("IsDian",true) ;
        }
        if(Input.GetKeyUp(KeyCode.Space))
        {
            playerAni.SetBool("IsDian",false) ;
            
        }
        
    }



    public void MatchMove(float tempSpeed)
    {
        var temp=rig.position+rig.transform.right*tempSpeed*Time.deltaTime;
        rig.MovePosition(temp);
    }




    public void MoveForwared(float tempSpeed)
    {
        //rig.position += transform.forward * speed * Time.deltaTime;
        var temp=rig.position+rig.transform.forward*tempSpeed*Time.deltaTime;
        rig.MovePosition(temp);
    }
    public void RotateAngles(float roteNumber)
    {
        _forwaredTarget=Quaternion.AngleAxis(roteNumber, Vector3.up)*cameraTransform.forward;
        rig.transform.forward= _forwaredTarget;
    }
}
