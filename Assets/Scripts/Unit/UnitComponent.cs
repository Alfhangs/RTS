using Configuration;
using System;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(Animator))]
public class UnitComponent : MonoBehaviour
{
    public string ID;
    public UnitType type;
    public int level;
    public float levelMultiplier;
    public float health;
    public float attack;
    public float defense;
    public float walkSpeed;
    public float attackSpeed;
    public Color selectedColor;
    public Color originalColor;
    public float attackRange;
    public ActionType Actions;

    private Animator animator;
    private Renderer myRenderer;
    private Vector3 movePosition;
    private bool shouldMove;
    private bool shouldAttack;
    private float attackCooldown;
    private ActionType action;
    private UnitData unitData;
    private float minDistance = 0.5f;

    private void Awake()
    {
        myRenderer = GetComponentInChildren<Renderer>();
        animator = GetComponent<Animator>();
        animator.Play("Idle");
    }

    private void OnEnable()
    {
        MessageQueueManager.Instance.AddListener<ActionCommandMessage>(OnActionCommandReceived);
    }

    private void OnDisable()
    {
        MessageQueueManager.Instance.RemoveListener<ActionCommandMessage>(OnActionCommandReceived);
    }

    private void OnActionCommandReceived(ActionCommandMessage message)
    {
        action = message.Action;
        shouldAttack = false;
    }

    private void Update()
    {
        if (!shouldMove)
        {
            //Debug.Log("A");
            return;
        }
        if (Vector3.Distance(transform.position, movePosition) < 0.5f)
        {
            //Debug.Log("A");
            animator.Play("Idle");
            shouldMove = false;
            return;
        }
        Vector3 pos = (movePosition - transform.position).normalized;
        transform.position += pos * Time.deltaTime * walkSpeed;
    }

    //private void Update()
    //{
    //    switch (action)
    //    {
    //        case ActionType.Attack:
    //            UpdateAttack();
    //            break;
    //        case ActionType.Defense:
    //            UpdateDefense();
    //            break;
    //        case ActionType.Move:
    //            UpdateMovement();
    //            break;
    //        case ActionType.Collect:
    //            UpdateCollect();
    //            break;
    //        case ActionType.Build:
    //        case ActionType.Upgrade:
    //        case ActionType.None:
    //        default:
    //            EnableMovement(false);
    //            break;
    //    }
    //}
    public void CopyData(UnitData unitData)
    {
        ID = Guid.NewGuid().ToString();
        type = unitData.Type;
        level = unitData.Level;
        levelMultiplier = unitData.LevelMultiplier;
        health = unitData.Health;
        attack = unitData.Attack;
        defense = unitData.Defense;
        walkSpeed = unitData.WalkSpeed;
        attackSpeed = unitData.AttackSpeed;
        selectedColor = unitData.SelectedColor;
        originalColor = unitData.OriginalColor;
        attackRange = unitData.AttackRange;
        Actions = unitData.Actions;

        this.unitData = unitData;
        shouldMove = false;
        //EnableMovement(false);
    }
    public void Selected(bool selected)
    {
        if (myRenderer == null)
        {
            Debug.LogError("Renderer component is missing!");
            return;
        }
        Material[] materials = myRenderer.materials;
        foreach (Material material in materials)
        {
            if (selected)
            {
                material.SetColor("_EmissionColor", selectedColor * 0.5f);
            }
            else
            {
                material.SetColor("_EmissionColor", originalColor);
            }
        }
    }

    public void MoveTo(Vector3 position)
    {
        transform.LookAt(position);
        movePosition = position;
        animator.Play("Run");
        shouldMove = true;
        //EnableMovement(true);
    }

    private void EnableMovement(bool enabled)
    {
        if (enabled)
        {
            animator.Play(unitData.GetAnimationState(UnitAnimationState.Move));
        }
        else
        {
            //animator.Play(unitData.GetAnimationState(UnitAnimationState.Idle));
        }
        shouldMove = enabled;
    }

    private void UpdateAttack()
    {
        UnitAnimationState attackState = (UnityEngine.Random.value < 0.5f) ? UnitAnimationState.Attack01 : UnitAnimationState.Attack02;
        UpdatePosition(minDistance + attackRange, attackState);

        if (!shouldAttack ||  attackRange <= 0)
        {
            return;
        }
        attackCooldown -= Time.deltaTime;
        if (attackCooldown < 0)
        {
            MessageQueueManager.Instance.SendMessage(
            new FireballSpawnMessage
            {
                Position = transform.position,
                Rotation = transform.rotation,
                Damage = attack
            }); ;
            attackCooldown = attackSpeed;
        }
    }
    private void UpdateDefense()
    {
        UpdatePosition(minDistance, UnitAnimationState.Defense);
    }
    private void UpdateMovement()
    {
        UpdatePosition(minDistance, UnitAnimationState.Move);
    }
    private void UpdateCollect()
    {
        UpdatePosition(minDistance, UnitAnimationState.Collect);
    }
    private void UpdatePosition(float range, UnitAnimationState state)
    {
        if (!shouldMove)
        {
            return;
        }
        if (Vector3.Distance(transform.position, movePosition) < range)
        {
            animator.Play(unitData.GetAnimationState(state));
            shouldMove = false;
            shouldAttack = true;
            return;
        }
        UpdatePosition();
    }

    protected virtual void UpdatePosition()
    {
        Vector3 direction = (movePosition - transform.position).normalized;
        transform.position += direction * Time.deltaTime * walkSpeed;
    }

    protected Vector3 GetFinalPosition()
    {
        return movePosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"name: {collision.gameObject.name} tag: {collision.gameObject.tag}");
        if (!collision.gameObject.CompareTag("Plane"))
        {
            //animator.Play(unitData.GetAnimationState(UnitAnimationState.Idle));
            shouldMove = false;
        }
    }
}
