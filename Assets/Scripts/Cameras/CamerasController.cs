using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class CamerasController : MonoBehaviour
{
    public float rotationSpeed = 2.0f;
    public float fovSpeed = 10.0f;
    public float distance = 10f;
    public float heightDistance = 1f;
    public Vector3 tempPlayerPosition;
    public Vector3 target;
    //public Transform firstCamera;
    public Transform player;
    private void Start()
    {
        player = GameObject.Find("lele").transform;
    }
    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        //float mouseY = Input.GetAxis("Mouse Y");
        float scrollWhell = Input.GetAxis("Mouse ScrollWheel");
        Camera.main.transform.position = player.position - transform.forward * distance;
        Camera.main.transform.position += transform.up *heightDistance;
        tempPlayerPosition = player.position + heightDistance * Vector3.up;
        Camera.main.transform.LookAt(tempPlayerPosition);
        Camera.main.transform.Rotate(tempPlayerPosition.y*Vector3.up,rotationSpeed*mouseX,Space.Self);
        //Camera.main.transform.Rotate(player.position.x*Vector3.right, rotationSpeed * mouseY, Space.Self);
        
    }
}
