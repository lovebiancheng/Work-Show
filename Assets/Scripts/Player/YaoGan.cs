using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YaoGan : MonoBehaviour
{
    public Transform _player;
    // Start is called before the first frame update
    private Transform _joyBg;
    private Transform _joyCenter;
    private Transform tempjoyCenter;
    //private Transform _player;
    private Vector3 _forwardTarget;


    private float radius;
    private Vector2 moveCenter;
    private Vector2 mouseToCenter;
    private float mouseToCenterDistance;
    private float _hor;
    private float _ver;
    private float _rotAngle;
    

    void Start()
    {
        _joyBg = GameObject.Find("Canvas").transform.Find("joyBg");
        _joyCenter = GameObject.Find("Canvas").transform.Find("joyBg/joyM");
        //tempjoyCenter = _joyCenter;
        //Debug.Log(tempjoyCenter.localPosition);
        _player = GameObject.Find("lele").transform;
        radius = 50;
    }

    // Update is called once per frame
    void Update()
    {
        if(System.Math.Abs(_hor)>0||System.Math.Abs(_ver)>0)
        {
            _player.position += _player.TransformDirection(new Vector3(0, 0, 0.1f * Mathf.Clamp(mouseToCenterDistance / 100, 0, 1)));
            _player.forward = _forwardTarget;
        }
    }

    //开始拖动
    public void OnDrageBegain()
    {
        moveCenter=Input.mousePosition;
        _joyBg.gameObject.SetActive(true);
        _joyBg.position= moveCenter;
        _joyCenter.position = moveCenter;
        
    }




    //正在拖动
    public void OnDragMove()
    {
        mouseToCenter=(Vector2)Input.mousePosition-moveCenter;
        mouseToCenterDistance=Mathf.Clamp(mouseToCenter.magnitude, 0, 100);
        if(mouseToCenterDistance < radius)
        {
            _joyCenter.position=mouseToCenter.normalized*mouseToCenterDistance+moveCenter;
        }
        else
        {
            _joyCenter.position = mouseToCenter.normalized * radius + moveCenter;
        }
        _hor=(_joyCenter.position.x-moveCenter.x)/100;
        _ver=(_joyCenter.position.y-moveCenter.y)/100;

        _rotAngle=Vector3.Angle(mouseToCenter,Vector3.up);

        if (_hor < 0)
        {
            _rotAngle = 360 - _rotAngle;
        }
        _forwardTarget = Quaternion.AngleAxis(_rotAngle, Vector3.up) * Vector3.forward;
        
    }



    //拖动结束
    public void OnDragEnd()
    {
        _hor=0;
        _ver=0;
        _joyBg.gameObject.SetActive(false);
    }


}
