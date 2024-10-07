using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerZoom : MonoBehaviour
{
    [SerializeField][Range(0f, 10f)] private float defaultDistance = 6f;//相机开始的默认距离
    [SerializeField][Range(0f, 10f)] private float minimumDistance = 1f;//距离的最小值
    [SerializeField][Range(0f, 10f)] private float maximumDistance = 6f;//距离的最大值

    [SerializeField][Range(0f, 10f)] private float smoothing = 4f;//平滑距离的插值
    [SerializeField][Range(0f, 10f)] private float zoomSensitivity = 1f;//用于与Z轴相乘

    private CinemachineFramingTransposer framingTransposer;
    private CinemachineInputProvider inputProvider;

    private float currentTargetDistance;
    private void Awake()
    {
        framingTransposer = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachineFramingTransposer>();
        inputProvider = GetComponent<CinemachineInputProvider>();
        currentTargetDistance = defaultDistance;
    }
    private void Update()
    {
        Zoom();
    }
    private void Zoom()//缩放方法
    {
        float zoomValue = inputProvider.GetAxisValue(2) * zoomSensitivity;//2，Z轴的索引

        currentTargetDistance = Mathf.Clamp(currentTargetDistance + zoomValue, minimumDistance, maximumDistance);//目标距离

        float currentDistance = framingTransposer.m_CameraDistance;

        if (currentDistance == currentTargetDistance)
        {
            return;
        }
        float lerpedZoomValue = Mathf.Lerp(currentDistance, currentTargetDistance, smoothing * Time.deltaTime);
        framingTransposer.m_CameraDistance = lerpedZoomValue;
    }



}
