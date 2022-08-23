using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private List<AnimationInformation> _idleHeroAnimations;
    [SerializeField] private List<AnimationInformation> _walkHeroAnimations;    
    [SerializeField] private List<AnimationInformation> _idleHandsAnimations;
    [SerializeField] private List<AnimationInformation> _walkHandsAnimations;
    [SerializeField] private Animator _heroAnimator;
    [SerializeField] private Animator _handsAnimator;

    private (Vector2 vector, Direction direction)[] _directionCircle =
    {
        (new Vector2(0, 1), Direction.Up),
        (new Vector2(0, -1), Direction.Down),
        (new Vector2(-1, 0), Direction.Left),
        (new Vector2(1, 0), Direction.Right)
    };

    private (IState idle, IState walk) _heroStates;
    private (IState idle, IState walk) _handsStates;

    private StateMachine _heroAnimationController;
    private StateMachine _handsAnimationController;
    private Player _player;

    private Vector2 _direction;

    private const int PositionDevitation = 15;

    private void Start()
    {
        _player = GetComponent<Player>();
        _heroAnimationController = new StateMachine();
        _handsAnimationController = new StateMachine();
        _heroStates.idle = new State(_heroAnimator);
        _heroStates.walk = new State(_heroAnimator);        
        _handsStates.idle = new State(_handsAnimator);
        _handsStates.walk = new State(_handsAnimator);
        _direction = ((Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition) - GetComponent<Rigidbody2D>().position).normalized;

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
        Vector2 movement = _player.Movement;
        TryToGetNewDirection();
        Direction viewDirection = FindNearestDirection(_direction);
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
        Vector2 newDirection = _player.AnimationDirection;
        //Debug.Log(Vector3.Angle(newDirection, _direction));
        if (Vector3.Angle(newDirection, _direction) > PositionDevitation)
            _direction = newDirection;
    }

    private bool IsOppositeDirections(Direction a, Direction b)
    {
        return (a == Direction.Left && b == Direction.Right) ||
               (a == Direction.Right && b == Direction.Left) ||
               (a == Direction.Up && b == Direction.Down) ||
               (a == Direction.Down && b == Direction.Up);
    }
}