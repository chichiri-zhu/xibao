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
        //myInput = new MyInput();
        //myInput.Enable();
        //myInput.Player.Esc.performed += ctx =>
        //{
        //    HandleEsc();
        //};

        OnAwake();
    }

    public virtual void OnAwake()
    {

    }

    public void Start()
    {
        OnStart();
        Hide();
    }

    public virtual void OnStart()
    {

    }

    public virtual void Show()
    {
        //myInput.Enable();
        isShow = true;
        //transform.localScale = new Vector3(1, 1, 1);
        gameObject.SetActive(true);
        if (OnShow != null)
        {
            OnShow();
        }
    }

    public virtual void Hide()
    {
        //myInput.Disable();
        isShow = false;
        //transform.localScale = new Vector3(0, 0, 0);
        gameObject.SetActive(false);
        if (OnClose != null)
        {
            OnClose();
        }
    }

    public virtual void HandleEsc()
    {
        if (isShow)
        {
            Hide();
        }
    }

    public bool IsShow()
    {
        return isShow;
    }
}
