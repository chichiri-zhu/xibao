using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SoloState : StateMachineBehaviour
{
    public string Name;
    public bool Continuous;
    public bool Active;
    public bool KeepAction;
    public Func<bool> Continue;

    private float _enterTime;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        _enterTime = Time.time;
        animator.SetBool("Walk", false);
        animator.SetBool("Action", true);
        Active = true;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.normalizedTime >= 1 && !Continuous)
        {
            Exit(animator, stateInfo);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Exit(animator, stateInfo);
    }

    private void Exit(Animator animator, AnimatorStateInfo stateInfo)
    {
        if (!Active || Time.time - _enterTime < stateInfo.length) return;
        animator.SetBool("Action", false);
        Active = false;
        if (Continue == null)
        {
            animator.SetBool("Walk", true);
        }
        else if (Continue != null)
        {
            if (!Continue())
            {
                animator.SetBool("Walk", true);
            }

            Continue = null;
        }
    }
}
