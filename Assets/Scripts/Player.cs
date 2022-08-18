using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    private Rigidbody2D _rigidbody;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _placeForShooting;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 dir = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - _placeForShooting.position).normalized;
            Bullet bullet = Instantiate(_bulletPrefab, _placeForShooting.position, Quaternion.identity);
            bullet.CreateBullet(dir, 25);
        }
    }
    private void FixedUpdate()
    {
        _rigidbody.velocity = Vector3.zero;
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        if (movement != Vector2.zero)
        {
            MoveCharacter(movement);
        }
    }


    private void MoveCharacter(Vector2 movement)
    {
        _rigidbody.MovePosition(_rigidbody.position + movement.normalized * _movementSpeed * Time.fixedDeltaTime);
    }

}