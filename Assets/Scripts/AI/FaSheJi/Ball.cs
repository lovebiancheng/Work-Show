using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody rb;
    private bool hadFire = false;
    private bool hadGround=false;

    private LineRenderer lineRenderer;
    public Vector3 lastPos;


    public Vector3 force;
    public Vector3 rotationVector;
    public float rotationSpeed;


    //public Vector3 preVelocity;


    #region Main Methods
    public void Interlize(Vector3 force,Vector3 rotationVector,float rotationSpeed)
    {
        this.force = force;
        this.rotationVector=rotationVector;
        this.rotationSpeed = rotationSpeed;
    }



    public void Fire()
    {
        rb.AddForce(transform.forward*force.magnitude, ForceMode.VelocityChange);
        hadFire = true;
        //Debug.Log("速度" + rb.velocity.magnitude+"      力"+force.magnitude);
        //Debug.Log("发射");
    }
    public void RotateSelf(Vector3 rotationVector,float speed)
    {
        transform.Rotate(rotationVector,speed,Space.Self);
        //Debug.Log("旋转");
    }

    public void CreatLineRender()
    {
        lineRenderer=new GameObject().AddComponent<LineRenderer>();
        lineRenderer.positionCount = 0;
        lineRenderer.startColor = Color.green;
        lineRenderer.endColor = Color.red;
        lineRenderer.startWidth = 0.01f;
        lineRenderer.endWidth = 0.01f;
        lastPos = transform.position;

    }
    public void RealTimeDrawline(LineRenderer lineRender,Vector3 newPoint)
    {
        lineRenderer.positionCount++;
        lineRenderer.SetPosition(lineRender.positionCount-1,newPoint);
    }






    public void OnCollisionEnter(Collision collision)
    {

        Debug.Log(collision.gameObject.name);
        
        if (collision.gameObject.name == "TableMian")
        {
            //Vector3 preVelocity = rb.velocity; 
            //ContactPoint contactPoint = collision.contacts[0];
            //Vector3 newDir = Vector3.Reflect(preVelocity, contactPoint.normal);//第一个平面向量，第二个法线
            //Debug.Log("normal"+contactPoint.normal+"    pre"+preVelocity+"           new"+newDir);
            //Quaternion rotation = Quaternion.FromToRotation(preVelocity, newDir);
            //transform.rotation = rotation;
            //rb.velocity = new Vector3(newDir.x, newDir.y*10, newDir.z);//这里的10这个值很关键，是检验一个球是否能弹到一定高度的重要条件
            
        }
        if(collision.gameObject.name == "Ground")
        {
            hadGround = true;
        }
    }



    #endregion


    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = true;
        
    }
    private void Start()
    {
        CreatLineRender();
    }
    private void Update()
    {
        if (hadFire)
        {
            RotateSelf(rotationVector, rotationSpeed);
            if (Vector3.Distance(lastPos, transform.position) > 0.1f&&!hadGround)
            {
                RealTimeDrawline(lineRenderer,transform.position);
                lastPos = transform.position;
            }
        }
    }
}
