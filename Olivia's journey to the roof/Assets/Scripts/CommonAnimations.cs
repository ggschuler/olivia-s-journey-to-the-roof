using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonAnimations : MonoBehaviour
{
    private float idleTimer = 5f;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            idleTimer += Time.time;
            animator.SetBool("timeToSit", false);
        }
        if (idleTimer < Time.time)
        {
            animator.SetBool("timeToSit",true);
            // need to do some kind of reset!
        }
    }
}
