using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [field:SerializeField] public PlayerSo Data {  get; private set; }

    [field:SerializeField] public LayerData LayerData { get; private set; }

    public Rigidbody Rigidbody { get; private set; }
    //public Animator Animator { get; private set; }
    public PlayerInput InputActions { get; private set; }
    public Transform MmainCameraTransform { get; private set; }
    public Collider Collider { get; private set; }

    private PlayerStateMachine stateMachine;

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        //Animator= GetComponent<Animator>();
        InputActions = GetComponent<PlayerInput>();

        stateMachine=new PlayerStateMachine(this);

        MmainCameraTransform = Camera.main.transform;
        EliminationGameManager.Instance.changeRemoveNumberEvent.AddListener(ChangeNewRemoveNumber);
    }

    public void ChangeNewRemoveNumber()
    {
        stateMachine.ResuableDataR.RemoveNumber = EliminationGameManager.Instance.destoryNum;
        Debug.Log("这是玩家获取到的"+stateMachine.ResuableDataR.RemoveNumber);
    }

    private void OnValidate()
    {
        
    }
    private void Start()
    {
        stateMachine.ChangeState(stateMachine.NormalIdleN);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    Debug.Log(collision.collider.gameObject.name);
    //}
    //private void OnCollisionExit(Collision collision)
    //{

    //}
    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.collider.gameObject.name);
        stateMachine.OnTriggerEnter(collision.collider);
    }
    private void OnTriggerExit(Collider collider)
    {
        stateMachine.OnTriggerExit(collider);
    }
    private void Update()
    {
        stateMachine.HandleInput();
        stateMachine.Update();
    }
    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }
    
}
