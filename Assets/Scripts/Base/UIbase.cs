using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class UIbase : MonoBehaviour, IShowUI
{
    protected bool isShow = false;

    public Action OnClose;
    public Action OnShow;
    public MyInput myInput;

    public void Awake()
    {
        myInput = new MyInput();
        myInput.Enable();
        myInput.Player.Esc.performed += ctx =>
        {
            if (isShow)
            {
                HandleEsc();
            }
        };

        Hide();
        OnAwake();
    }

    public virtual void OnAwake()
    {

    }

    public virtual void Show()
    {
        myInput.Enable();
        isShow = true;
        transform.localScale = new Vector3(1, 1, 1);
        if (OnShow != null)
        {
            OnShow();
        }
    }

    public virtual void Hide()
    {
        myInput.Disable();
        isShow = false;
        transform.localScale = new Vector3(0, 0, 0);
        if(OnClose != null)
        {
            OnClose();
        }
    }

    public virtual void HandleEsc()
    {
        Hide();
    }

    public bool IsShow()
    {
        return isShow;
    }
}
