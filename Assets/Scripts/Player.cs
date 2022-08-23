using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private Spell _bulletPrefab;
    [SerializeField] private Transform _placeForShooting;
    [SerializeField] private Transform _middle;
    [SerializeField] private float _bulletSpeed = 3f;
    [SerializeField] private float shootingDistance = 20f;

    private Rigidbody2D _rigidbody;
    private Camera _camera;
    public Vector2 AnimationDirection { get; private set; }
    public Vector2 Movement { get; private set; }

    private void Start()
    {
        _camera = Camera.main;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 shootingDirection = _camera.ScreenToWorldPoint(Input.mousePosition) - _placeForShooting.position;
        if (Input.GetMouseButtonDown(0))
        {
            Spell bullet = Instantiate(_bulletPrefab, _placeForShooting.position, Quaternion.identity);
            bullet.CreateBullet(shootingDirection, _bulletSpeed, shootingDistance);
        }

    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = Vector3.zero;
        if (Movement != Vector2.zero)
        {
            MoveCharacter(Movement);
        }
        else
        {
            AnimationDirection = ((Vector2)_camera.ScreenToWorldPoint(Input.mousePosition) - _rigidbody.position).normalized;
        }
    }

    private void MoveCharacter(Vector2 movement)
    {
        _rigidbody.MovePosition(PixelPerfectClamp(_rigidbody.position, 32) + PixelPerfectClamp(movement.normalized * _movementSpeed * Time.fixedDeltaTime, 32));
        AnimationDirection = (_camera.ScreenToWorldPoint(Input.mousePosition) - _middle.position).normalized;
    }

    private Vector2 PixelPerfectClamp(Vector2 moveVector, int pixelsPerUnit)
    {
        Vector2 vectorInPixels = new Vector2(
            Mathf.RoundToInt(moveVector.x * pixelsPerUnit),
            Mathf.RoundToInt(moveVector.y * pixelsPerUnit));
        return vectorInPixels /  pixelsPerUnit;
    }
}