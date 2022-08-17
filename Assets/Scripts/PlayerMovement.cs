using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool isNormalized = true;
    private Rigidbody2D rb;
    [SerializeField] private Animator _animator, _animatorHands;
    public Vector2 movement;
    public Vector2 dir;
    [SerializeField] private float _timer;
    public Transform obj;
    private float currentTime = 0f;

    private void Start()
    { 
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector2.zero;
        float moveY = 0;
        float moveX = 0;
        if (Input.GetKey(KeyCode.W))
        {
            moveY = 1f;
        }        
        if (Input.GetKey(KeyCode.S))
        {
            moveY = -1f;
        }        
        if (Input.GetKey(KeyCode.A))
        {
            moveX = -1f;
        }        
        if (Input.GetKey(KeyCode.D))
        {
            moveX = 1f;
        }
        Vector2 moveDir = new Vector2(moveX, moveY).normalized;
        rb.velocity = moveDir;
    }


    private void MoveCharacter(Vector2 movement)
    { 
        if (isNormalized)
            rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
        else
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        rb.velocity = Vector3.zero;

    }

}