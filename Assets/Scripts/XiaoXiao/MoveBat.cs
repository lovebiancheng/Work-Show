using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class MoveBat : MonoBehaviour
{
    private GameBat bat;
    private IEnumerator moveCoroutine;
    private IEnumerator moveSinglePosition;

    public AnimationCurve curve;
    public float duration;


    private Vector3 startPos;
    private Vector3 endPos;
    
    private void Awake()
    {
        bat = GetComponent<GameBat>();
    }
    //单个交换的
    public IEnumerator MoveSingleDownPosition(GameBat gameBat)
    {

        float t = 0;
        Vector3 startPosA = gameBat.transform.localPosition;
        Vector3 endPosA = GameManager.Instance.CorrectPosition(gameBat.X, gameBat.Y + 1);
        
        while (t<duration)
        {
            t+= Time.deltaTime;
            float animationTime=curve.Evaluate(t/duration);
           
            gameBat.transform.localPosition = Vector3.Lerp(startPosA, endPosA, animationTime);
            yield return null;
        }
        gameBat.transform.localPosition = endPosA;
    }
    public IEnumerator MoveSingleDown(GameBat gameBat)
    {
        if (moveSinglePosition != null)
        {
            StopCoroutine(moveSinglePosition);
        }
        moveSinglePosition = MoveSingleDownPosition(gameBat);
        StartCoroutine(moveSinglePosition);
        yield return null;
    }
    public IEnumerator MoveSingleUpPosition(GameBat gameBat)
    {

        float t = 0;
        Vector3 startPosA = gameBat.transform.localPosition;
        Vector3 endPosA = GameManager.Instance.CorrectPosition(gameBat.X, gameBat.Y - 1);

        while (t < duration)
        {
            t += Time.deltaTime;
            float animationTime = curve.Evaluate(t / duration);

            gameBat.transform.localPosition = Vector3.Lerp(startPosA, endPosA, animationTime);
            yield return null;
        }
        gameBat.transform.localPosition = endPosA;
    }
    public IEnumerator MoveSingleUp(GameBat gameBat)
    {
        if (moveSinglePosition != null)
        {
            StopCoroutine(moveSinglePosition);
        }
        moveSinglePosition = MoveSingleUpPosition(gameBat);
        StartCoroutine(moveSinglePosition);
        yield return null;
    }
    //两个交换的
    public IEnumerator MovePosition(GameBat gameBat1,GameBat gameBat2)
    {
        
        startPos=gameBat1.transform.localPosition; 
        endPos=gameBat2.transform.localPosition;
        float t = 0f;
        while (t<duration)
        {

            t += Time.deltaTime;
            float animationTime = curve.Evaluate(t / duration);
            gameBat1.transform.localPosition = Vector3.Lerp(startPos, endPos, animationTime);
            gameBat2.transform.localPosition = Vector3.Lerp(endPos, startPos, animationTime);
            //Debug.Log("移动了一点");
            yield return null;
        }
        gameBat1.transform.localPosition = endPos;
        gameBat2.transform.localPosition = startPos;
        //yield return null;
    }

    public IEnumerator MoveAB(GameBat gameBat3,GameBat gameBat4)
    {
        
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
            //Debug.Log("关闭协程");
        }
        moveCoroutine = MovePosition(gameBat3, gameBat4);
        StartCoroutine(moveCoroutine);
        //Debug.Log("开启协程");
        yield return null;

    }
    //public Vector3 startPos;
    //public Vector3 endPos;
    //public float duration;
    //public float t = curve.Evaluate();
    //public Vector3 pos=Vector3.Lerp(startPos, endPos, duration);
}
