using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private bool isNormalized = true;
    private Rigidbody2D rb;
    [SerializeField] private Animator _animator;
    private const string canMove = "canMove";
    private const string moveX = "moveX";
    private const string moveY = "moveY";

    private void Start()
    { 
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Vector2 movement = Vector2.zero;
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        if (movement != Vector2.zero)
        {
            _animator.SetBool(canMove, true);
            MoveCharacter(movement);
        }
        else
            _animator.SetBool(canMove, false);
    }

    private void MoveCharacter(Vector2 movement)
    { 
        _animator.SetFloat(moveX, movement.x);
        _animator.SetFloat(moveY, movement.y);
        if (isNormalized)
            rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
        else
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

    }
}