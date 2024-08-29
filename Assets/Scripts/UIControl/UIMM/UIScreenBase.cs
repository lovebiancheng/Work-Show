using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIScreenBase : MonoBehaviour
{
    public enum UIStates
    {
        None,
        OpenState,
        CloseState,
        HidenState,
    }
    protected bool mInitialized=false;
    protected UIStates mState=UIStates.None;
    public UIStates State
    {
        get { return mState; }
    }
    //public delegate void OnScreenHandlerEventHandler();//委托   方法的引用  通过new关键字   +实现多播
    //public event OnScreenHandlerEventHandler onCloseScreen;
    protected virtual void Init()
    {
        mInitialized = true;
    }
    public virtual void Open() { }
    public virtual void Close() { }
    public virtual void RefreshUI() { }
    
}
