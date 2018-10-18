using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour {
    private Animator animator;


    private void Start() {
        animator = GetComponent<Animator>();

        animator.SetTrigger("ShowCommands");
    }
}