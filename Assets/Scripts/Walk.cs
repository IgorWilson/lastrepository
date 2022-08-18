using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : IState
{
    private Dictionary<Direction, string> _animations = new Dictionary<Direction, string>();
    private Animator _animator;
    public void Enter(Direction direction)
    {
        _animator.Play(_animations[direction]);
    }

    public Walk(Animator animator)
    {
        _animator = animator;
    }
    public IState AddDirection(Direction direction, AnimationClip clip)
    {
        _animations[direction] = clip.name;
        return this;
    }

    public void SetNormalDirection()
    {
        _animator.SetFloat("direction", 1);
    }

    public void SetReverseDirection()
    {
        _animator.SetFloat("direction", -1);
    }
}
