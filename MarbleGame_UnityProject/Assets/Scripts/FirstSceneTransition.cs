using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstSceneTransition : MonoBehaviour
{
    [SerializeField] public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator.SetTrigger("TransitionEnter");
    }
}
