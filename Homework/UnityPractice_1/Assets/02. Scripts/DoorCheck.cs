using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorCheck : MonoBehaviour
{
    public bool isDoorOpen;
    public float doorOpenTime = 10f;

    private float doorTimer;
    private Animator animator;

    private void Awake()
    {
        animator = this.gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        if (GameManager.Instance.isPlaying)
        {
            doorTimer += Time.deltaTime;
            if (doorTimer > doorOpenTime)
            {
                doorTimer = 0;
                animator.SetBool("Open", true);
            }

            if (isDoorOpen)
            {
                if (!GameManager.Instance.isTee && GameManager.Instance.isPlaying)
                {
                    print("들킴!");
                    animator.SetTrigger("Surprise");
                    GameManager.Instance.GameFail();
                }
            }
        }
    }

    public void DoorOpen()
    {
        print("문을 완전히 염!");
        isDoorOpen = true;
    }

    public void DoorClose()
    {
        print("문을 완전히 닫음!");
        isDoorOpen = false;
        animator.SetBool("Open", false);
    }
}
