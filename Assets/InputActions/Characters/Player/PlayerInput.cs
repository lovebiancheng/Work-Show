using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    public PlayerInputActions InputActions { get; private set; }
    public PlayerInputActions.PlayerActions playerActions { get; private set; }

    private void Awake()
    {
        InputActions = new PlayerInputActions();//实例化
        playerActions = InputActions.Player;//player是PlayerActions结构类型的变量名称
    }
    //先启用操作方式
    private void OnEnable()
    {
        InputActions.Enable();
    }
    private void OnDisable()
    {
        InputActions.Disable();
    }

    //禁用输入操作几秒
    public void DisableActionFor(InputAction action, float seconds)
    {
        StartCoroutine(DisableAction(action, seconds));
    }
    private IEnumerator DisableAction(InputAction action, float seconds)
    {
        action.Disable();
        yield return new WaitForSeconds(seconds);
        action.Enable();
    }
}
