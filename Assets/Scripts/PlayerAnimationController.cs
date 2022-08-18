using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private List<AnimationInformation> _idleHeroAnimations;
    [SerializeField] private List<AnimationInformation> _walkHeroAnimations;    
    [SerializeField] private List<AnimationInformation> _idleHandsAnimations;
    [SerializeField] private List<AnimationInformation> _walkHandsAnimations;
    [SerializeField] private Animator _heroAnimator;
    [SerializeField] private Animator _handsAnimator;
    private Rigidbody2D _rigidbody;
    private (Vector2 vector, Direction direction)[] _directionCircle =
    {
        (new Vector2(0, 1), Direction.Up),
        (new Vector2(0, -1), Direction.Down),
        (new Vector2(-1, 0), Direction.Left),
        (new Vector2(1, 0), Direction.Right)
    };
        
    private StateMachine _heroAnimationController;
    private StateMachine _handsAnimationController;
    private (IState idle, IState walk) _heroStates;
    private (IState idle, IState walk) _handsStates;
    private Vector2 direction;
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _heroAnimationController = new StateMachine();
        _handsAnimationController = new StateMachine();
        _heroStates.idle = new Idle(_heroAnimator);
        _heroStates.walk = new Walk(_heroAnimator);        
        _handsStates.idle = new Idle(_handsAnimator);
        _handsStates.walk = new Walk(_handsAnimator);
        direction = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - _rigidbody.position).normalized;
        foreach (var e in _idleHeroAnimations)
        {
            _heroStates.idle = _heroStates.idle.AddDirection(e.direction, e.clip);
        }

        foreach (var e in _walkHeroAnimations)
        {
            _heroStates.walk = _heroStates.walk.AddDirection(e.direction, e.clip);
        }

        foreach (var e in _idleHandsAnimations)
        {
            _handsStates.idle = _handsStates.idle.AddDirection(e.direction, e.clip);
        }

        foreach (var e in _walkHandsAnimations)
        {
            _handsStates.walk = _handsStates.walk.AddDirection(e.direction, e.clip);
        }
    }


    private Direction FindNearestDirection(Vector2 vec)
    {
        (Vector2 vector, Direction direction) ans = _directionCircle[0];
        foreach ((Vector2 vector, Direction direction) e in _directionCircle)
        {
            if (Vector2.Dot(vec, e.vector) > Vector2.Dot(vec, ans.vector))
                ans = e;
        }
        return ans.direction;
    }

    private void Update()
    {
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        TryToGetNewDirection();
        Direction viewDirection = FindNearestDirection(direction);
        Direction moveDirection = FindNearestDirection(movement);
        if (movement != Vector2.zero)
        {
            if (IsOppositeDirections(viewDirection, moveDirection))
            {
                _heroStates.walk.SetReverseDirection();
                _handsStates.walk.SetReverseDirection();
            }
            else
            {
                _heroStates.walk.SetNormalDirection();
                _handsStates.walk.SetNormalDirection();
            }
            _heroAnimationController.Initialize(_heroStates.walk, viewDirection);
            _heroAnimationController.Initialize(_handsStates.walk, viewDirection);
        }
        else
        {
            _heroAnimationController.Initialize(_heroStates.idle, viewDirection);
            _handsAnimationController.Initialize(_handsStates.idle, viewDirection);
        }
    }

    private void TryToGetNewDirection()
    {
        Vector2 newDirection = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        if (Vector3.Angle(newDirection, direction) > 15)
            direction = newDirection;
    }

    private bool IsOppositeDirections(Direction a, Direction b)
    {
        return (a == Direction.Left && b == Direction.Right) ||
               (a == Direction.Right && b == Direction.Left) ||
               (a == Direction.Up && b == Direction.Down) ||
               (a == Direction.Down && b == Direction.Up);
    }
}