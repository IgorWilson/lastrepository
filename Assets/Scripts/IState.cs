using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    void SetNormalDirection();
    void SetReverseDirection();
    IState AddDirection(Direction direction, AnimationClip clip);
    void Enter(Direction direction);
}
