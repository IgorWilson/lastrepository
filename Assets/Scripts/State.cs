using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : IState
{

    private Dictionary<Direction, string> _animations = new Dictionary<Direction, string>();
    private Animator _animator;
    public void Enter(Direction direction)
    {
        if (!_animations.ContainsKey(direction))
            throw new Exception("Current state doesn't have animation in this direction");
        _animator.Play(_animations[direction]);
    }

    public void SetNormalDirection()
    {
        _animator.SetFloat("direction", 1);
    }

    public void SetReverseDirection()
    {
        _animator.SetFloat("direction", -1);
    }

    public IState AddDirection(Direction direction, AnimationClip clip)
    {
        if (_animations.ContainsKey(direction))
            throw new Exception("Can't add the same direction");
        _animations[direction] = clip.name;
        return this;
    }

    public IState AddDirection(IEnumerable<AnimationInformation> list)
    {
        foreach (var element in list)
        {
            if (_animations.ContainsKey(element.direction))
                throw new Exception("Can't add the same direction");
            _animations[element.direction] = element.clip.name;
        }

        return this;
    }

    public State(Animator animator)
    {
        _animator = animator;
    }

}
