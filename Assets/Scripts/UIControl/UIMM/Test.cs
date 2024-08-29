using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    public RawImage imgChar;
    public Transform player;


    private Vector2 startPos;
    private Transform charCamTrsns;

    private void Awake()
    {
        gameObject.SetActive(false);
        if(charCamTrsns != null )
        {
            charCamTrsns.gameObject.SetActive(false);
        }
        RegTouchEvts();
    }

    private void Start()
    {
        ClickOpenBtn();
    }

    private float startRotate = 0;
    private void SetStartRotate()
    {
        startRotate=player.transform.localEulerAngles.y;

    }
    private void SetPlayerRotate(float rotate)
    {
        player.transform.localEulerAngles=new Vector3(0,startRotate+rotate,0);
    }


    private void RegTouchEvts()
    {
        OnClickdown(imgChar.gameObject, (PointerEventData evt) =>
        {
            startPos = evt.position;
            SetStartRotate();
        });
        OnDrag(imgChar.gameObject, (PointerEventData evt) =>
        {
            float rotate = -(evt.position.x - startPos.x) * 0.4f;
            SetPlayerRotate(rotate);
        });
    }
    public void ClickOpenBtn()
    {
        if (charCamTrsns==null)
        {
            charCamTrsns = GameObject.FindGameObjectWithTag("CharShowCam").transform;
        }
        charCamTrsns.localPosition = player.transform.localPosition+player.transform.forward*3.8f+new Vector3(0,1.2f,0);
        charCamTrsns.localEulerAngles=new Vector3(0,180+player.transform.localEulerAngles.y,0);
        charCamTrsns.localScale = Vector3.one;
        charCamTrsns.gameObject.SetActive(true);
        gameObject.SetActive(true);

    }




    private T GetOrAddComponent<T>(GameObject go) where T : Component
    {
        T t=go.GetComponent<T>();
        if (t==null)
        {
            t = go.AddComponent<T>();
        }
        return t;
    }
    
    private void OnClickdown(GameObject go,Action<PointerEventData> cb)
    {
        PEListenre listener = GetOrAddComponent<PEListenre>(go);
        listener.onClickDown = cb;
    }
    private void OnDrag(GameObject go,Action<PointerEventData> cb)
    {
        PEListenre listener= GetOrAddComponent<PEListenre>(go);
        listener.onDrag = cb;
    }
    
}
