using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] private Animator animator;

    private void Start()
    {
        AttackAnim();
    }

    public void AttackAnim()
    {
        animator.SetTrigger("Attack");
    }

    public void HappyAnim()
    {
        animator.SetTrigger("Happy");
    }

    public void GoingToSleepAnim()
    {
        animator.SetTrigger("GoingToSleep");
    }
}
