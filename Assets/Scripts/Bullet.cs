using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Bullet : MonoBehaviour
{
    private Vector2 _direction = Vector2.zero;
    private float _speed;
    private bool _isCreated = false;
    private Rigidbody2D _rigidbody;

    public void CreateBullet(Vector2 direction, float speed)
    {
        _direction = direction;
        _speed = speed;
        _isCreated = true;
        _rigidbody = GetComponent<Rigidbody2D>();
        _rigidbody.velocity = direction.normalized * speed;
        Destroy(gameObject, 10f);
    }

}
