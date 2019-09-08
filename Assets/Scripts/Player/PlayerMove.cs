using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour,IMove
{
    [Header("移动速度")]
    public float speed = 6f;
    [Header("动画")]
    public Animator animator;
    [Header("脚步声")]
    public AudioSource au;

    Rigidbody PlayerRigidbody;
    bool Walking;
    int FloorMask;

    private void Awake()
    {
        FloorMask = LayerMask.GetMask("Floor");
        PlayerRigidbody = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Turning();
        WalkAnim();
        IsWalking();
        PlayStepSound();
    }

    public void Move(Vector3 vector)
    {
        vector = vector.normalized * speed * Time.deltaTime;
        PlayerRigidbody.MovePosition(transform.position + vector);
    }

    void IsWalking()
    {
        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            Walking = true;
        }
        else
        {
            Walking = false;
        }
    }

    //由摄像机向鼠标发射的射线检测地面达到转身效果
    void Turning()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1000f, FloorMask))
        {
            Vector3 PlayertoMouse = hit.point - transform.position;
            PlayertoMouse.y = 0f;

            Quaternion newRotation = Quaternion.LookRotation(PlayertoMouse);
            PlayerRigidbody.MoveRotation(newRotation);
        }
    }

    public void PlayStepSound()
    {
        if (Walking)
        {
            if (!au.isPlaying)
                au.Play();
        }
        else
        {
            if (au.isPlaying)
                au.Pause();
        }
    }

    public void WalkAnim()
    {
        if (Walking)
        {
            animator.SetFloat("Run", 0.2f);
        }
        else
        {
            animator.SetFloat("Run", 0f);
        }
    }
    public void Move() {}
}
