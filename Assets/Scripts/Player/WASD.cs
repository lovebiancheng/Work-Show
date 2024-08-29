using System.Collections;
using System.Collections.Generic;
using System.Net;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class WASD : MonoBehaviour
{
    private CharacterController characterController;
    public float speed = 10f;
    public float rotateSpeed = 1f;
    public float gravity = -9.8f;
    private Vector3 velocity = Vector3.zero;

    public Animator playerAnimator;
    //public Transform groundCheck;
    //public float checkRadius = 0.2f;
    //public LayerMask groundMask;

    private bool isGround;

    public float jumpHeight = 1f;

    public float walkToRun = 0f;

    // Start is called before the first frame update
    void Start()
    {
        playerAnimator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePosition();
    }

    public void MovePosition()
    {
        //isGround=Physics.CheckSphere(groundCheck.position, checkRadius,groundMask);

        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit rayHit;
        if (Physics.Raycast(ray, out rayHit, 0.1f))
        {
            Debug.DrawLine(transform.position, rayHit.point, Color.yellow);
            ;
            if (rayHit.collider.gameObject.tag == "Ground")
            {
                isGround = true;
                
            }
            else
            {
                isGround = false;
            }
            
            
        }
        
        if (isGround && velocity.y<0 )
        {
            velocity.y = 0;
            
        }
        if(isGround && Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        

        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        
        
        var move=transform.forward*speed*vertical*Time.deltaTime;
        if(move!=Vector3.zero)
        {
            Walk();
        }
        characterController.Move(move);
        

        //重力
        velocity.y += gravity * Time.deltaTime;
        characterController.Move(velocity);

        
        transform.Rotate(Vector3.up,horizontal*rotateSpeed);
    }
    
    public void Jump()
    {
        //播放动画
        playerAnimator.SetTrigger("Jump");
        velocity.y += Mathf.Sqrt(jumpHeight * -2 * gravity);
    }
    public void Walk()
    {
        playerAnimator.SetTrigger("Walk");
    }
    public void Run()
    {
        playerAnimator.SetBool("IsRun", true);
    }
}
