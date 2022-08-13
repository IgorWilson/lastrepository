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
    [SerializeField] private SpriteRenderer  _handsSpriteRenderer;
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
            _animatorHands.SetBool(canMove, true);
            MoveCharacter(movement);
        }
        else
        {
            _animator.SetBool(canMove, false);
            _animatorHands.SetBool(canMove, false);
        }
    }

    private void MoveCharacter(Vector2 movement)
    { 
        _animator.SetFloat(moveX, movement.x);
        _animator.SetFloat(moveY, movement.y);
        _animatorHands.SetFloat(moveX, movement.x);
        _animatorHands.SetFloat(moveY, movement.y);
        if(movement.y > 0 && movement.x == 0)
        {
            _handsSpriteRenderer.sortingOrder = 0;
        }
        else
        {
            _handsSpriteRenderer.sortingOrder = 1;
        }
        if (isNormalized)
            rb.MovePosition(rb.position + movement.normalized * speed * Time.fixedDeltaTime);
        else
            rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);

    }
}