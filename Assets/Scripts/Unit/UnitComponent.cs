using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(Animator))]
public class UnitComponent : MonoBehaviour
{
    public string ID;
    public UnitType Type;
    public int Level;
    public float LevelMultiplier;
    public float Health;
    public float Attack;
    public float Defense;
    public float WalkSpeed;
    public float AttackSpeed;
    public Color selectedColor;

    private Animator animator;
    private Renderer _renderer;

    private Vector3 _movePosition;
    private bool shouldMove;
    private void Awake()
    {
        _renderer = GetComponentInChildren<Renderer>();
        animator = GetComponent<Animator>();
        animator.Play("Idle");
    }
    private void Update()
    {
        if (!shouldMove)
        {
            Debug.Log("A");
            return;
        }
        if (Vector3.Distance(transform.position, _movePosition) < 0.5f)
        {
            Debug.Log("A");
            animator.Play("Idle");
            shouldMove = false;
            return;
        }
        Vector3 pos = (_movePosition - transform.position).normalized;
        transform.position += pos * Time.deltaTime * WalkSpeed;
    }
    public void CopyData(UnitData unitData)
    {
        ID = Guid.NewGuid().ToString();
        Type = unitData.Type;
        Level = unitData.Level;
        LevelMultiplier = unitData.LevelMultiplier;
        Health = unitData.Health;
        Attack = unitData.Attack;
        Defense = unitData.Defense;
        WalkSpeed = unitData.WalkSpeed;
        AttackSpeed = unitData.AttackSpeed;
        selectedColor = unitData.SelectedColor;
    }
    public void Selected(bool selected)
    {
        if (_renderer == null)
        {
            Debug.LogError("Renderer component is missing!");
            return;
        }
        Material[] materials = _renderer.materials;
        foreach (Material material in materials)
        {
            if (selected)
            {
                material.EnableKeyword("_EMISSION");
                material.SetColor("_EmissionColor", selectedColor * 0.5f);
            }
            else
            {
                material.EnableKeyword("_EMISSION");
            }
        }
    }
    public void MoveTo(Vector3 position)
    {
        transform.LookAt(position);
        _movePosition = position;
        animator.Play("Run");
        shouldMove = true;
    }
}
