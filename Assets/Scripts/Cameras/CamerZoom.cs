using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamerZoom : MonoBehaviour
{
    [SerializeField][Range(0f, 10f)] private float defaultDistance = 6f;//�����ʼ��Ĭ�Ͼ���
    [SerializeField][Range(0f, 10f)] private float minimumDistance = 1f;//�������Сֵ
    [SerializeField][Range(0f, 10f)] private float maximumDistance = 6f;//��������ֵ

    [SerializeField][Range(0f, 10f)] private float smoothing = 4f;//ƽ������Ĳ�ֵ
    [SerializeField][Range(0f, 10f)] private float zoomSensitivity = 1f;//������Z�����

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
    private void Zoom()//���ŷ���
    {
        float zoomValue = inputProvider.GetAxisValue(2) * zoomSensitivity;//2��Z�������

        currentTargetDistance = Mathf.Clamp(currentTargetDistance + zoomValue, minimumDistance, maximumDistance);//Ŀ�����

        float currentDistance = framingTransposer.m_CameraDistance;

        if (currentDistance == currentTargetDistance)
        {
            return;
        }
        float lerpedZoomValue = Mathf.Lerp(currentDistance, currentTargetDistance, smoothing * Time.deltaTime);
        framingTransposer.m_CameraDistance = lerpedZoomValue;
    }



}
