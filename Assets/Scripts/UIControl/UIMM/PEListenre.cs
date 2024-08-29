using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PEListenre : MonoBehaviour,IPointerDownHandler,IDragHandler
{
    public Action<PointerEventData> onClickDown;
    public Action<PointerEventData> onDrag;



    public void OnDrag(PointerEventData eventData)
    {
        if (onDrag != null)
        {
            onDrag(eventData);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(onClickDown != null)
        {
            onClickDown(eventData);
        }
    }

 
}
