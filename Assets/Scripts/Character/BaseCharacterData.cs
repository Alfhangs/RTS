using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacterData : ScriptableObject
{
    public float health;
    public float attack;
    public float defense;
    public float walkSpeed;
    public float attackSpeed;
    public Color selectedColor;
    public string animationStateAttack01;
    public string animationStateAttack02;
    public string animationStateDefense;
    public string animationStateMove;
    public string animationStateIdle;
    public string animationStateCollect;
    public string animationStateDeath;
    public float attackRange;
    public float colliderSize;

    public string GetAnimationState(UnitAnimationState animationState)
    {
        switch (animationState)
        {
            case UnitAnimationState.Attack01:
                return animationStateAttack01;
            case UnitAnimationState.Attack02:
                return animationStateAttack02;
            case UnitAnimationState.Defense:
                return animationStateDefense;
            case UnitAnimationState.Move:
                return animationStateMove;
            case UnitAnimationState.Idle:
                return animationStateIdle;
            case UnitAnimationState.Collect:
                return animationStateCollect;
            case UnitAnimationState.Death:
                return animationStateDeath;
            default:
                return animationStateIdle;
        }
    }
}
