using Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class BaseCharacter : MonoBehaviour
{
    protected Animator _animator;
    protected Renderer _renderer;
    protected Color _originalColor;
    protected Color _emissionColor;
    protected ActionType _action;

    public string ID;
    public float Health;
    public float Attack;
    public float Defense;
    public float WalkSpeed;
    public float AttackSpeed;
    public Color SelectedColor;
    public float AttackRange;
    public float ColliderSize;

    public bool IsDead { get; private set; }

    private void Awake()
    {
        _renderer = GetComponentInChildren<Renderer>();
        _animator = GetComponent<Animator>();
        _originalColor = _renderer.material.color;
        _emissionColor = _renderer.material.GetColor("_EmissionColor");
    }

    private void OnMouseEnter()
    {
        _renderer.material.color = SelectedColor;
    }

    private void OnMouseExit()
    {
        _renderer.material.color = _originalColor;
    }

    protected void CopyBaseData(BaseCharacterData data)
    {
        ID = Guid.NewGuid().ToString();
        Health = data.health;
        Attack = data.attack;
        Defense = data.defense;
        WalkSpeed = data.walkSpeed;
        AttackSpeed = data.attackSpeed;
        SelectedColor = data.selectedColor;
        AttackRange = data.attackRange;
        ColliderSize = data.colliderSize;
    }

    protected virtual void UpdateState(ActionType action)
    {
        if (IsDead || _action == action)
        {
            return;
        }
        _action = action;
    }

    protected virtual void PlayAnimation(UnitAnimationState animationState)
    {
        throw new NotImplementedException();
    }

    public virtual float GetAttack()
    {
        throw new NotImplementedException();
    }

    public virtual float GetDefense()
    {
        throw new NotImplementedException();
    }

    public bool TakeDamage(float damage)
    {
        if (IsDead)
        {
            return true;
        }

        Health -= damage;
        Debug.Log($"{gameObject.name}-{ID} took {damage} damage! HP: {Health}");
        if (Health > 0)
        {
            return false;
        }

        gameObject.AddComponent<DeadComponent>();
        PlayAnimation(UnitAnimationState.Death);
        IsDead = true;
        return true;
    }

    public Vector3 GetDamageFeedbackPosition()
    {
        return new Vector3(
            transform.position.x + _renderer.bounds.size.x / 2,
            transform.position.y + _renderer.bounds.size.y / 2,
            transform.position.z);
    }
}
