using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class SeeModle : MonoBehaviour, IPointerDownHandler, IDragHandler
{

    public Transform modleTransform;

    private float startrotate;
    private Vector2 deltaValue;
    public void OnDrag(PointerEventData eventData)
    {
        float rotateValue = -(eventData.position.x - deltaValue.x);
        SetModleRotate(rotateValue*0.5f);

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SetModleStart();
    }

    public void SetModleStart()
    {
        startrotate = modleTransform.localEulerAngles.y;
    }
    public void SetModleRotate(float temp)
    {
        
        modleTransform.localEulerAngles=new Vector3(0,startrotate+temp,0);
    }
}
