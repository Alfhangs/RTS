using Configuration;
using UnityEngine;

[RequireComponent(typeof(BoxCollider), typeof(Animator))]
public class UnitComponent : MonoBehaviour
{
    //public string ID;
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

    private Animator _animator;
    private Renderer _myRenderer;
    private Vector3 _movePosition;
    private bool _shouldMove;
    private bool _shouldAttack;
    private float _attackCooldown;
    private ActionType _action;
    [SerializeField] private UnitData _unitData;
    private float _minDistance = 0.5f;

    private void Awake()
    {
        _myRenderer = GetComponentInChildren<Renderer>();
        _animator = GetComponent<Animator>();
        _animator.Play("Idle");
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
        _action = message.Action;
        _shouldAttack = false;
    }

    //private void Update()
    //{
    //    if (!shouldMove)
    //    {
    //        //Debug.Log("A");
    //        return;
    //    }
    //    if (Vector3.Distance(transform.position, movePosition) < 0.5f)
    //    {
    //        //Debug.Log("A");
    //        animator.Play("Idle");
    //        shouldMove = false;
    //        return;
    //    }
    //    Vector3 pos = (movePosition - transform.position).normalized;
    //    transform.position += pos * Time.deltaTime * walkSpeed;
    //}

    private void Update()
    {
        switch (_action)
        {
            case ActionType.Attack:
                UpdateAttack();
                break;
            case ActionType.Defense:
                UpdateDefense();
                break;
            case ActionType.Move:
                UpdateMovement();
                break;
            case ActionType.Collect:
                UpdateCollect();
                break;
            case ActionType.Build:
            case ActionType.Upgrade:
            case ActionType.None:
            default:
                EnableMovement(false);
                break;
        }
    }
    public void CopyData(UnitData unitData)
    {
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

        this._unitData = unitData;
        _shouldMove = false;
        EnableMovement(false);
    }
    public void Selected(bool selected)
    {
        if (_myRenderer == null)
        {
            Debug.LogError("Renderer component is missing!");
            return;
        }
        Material[] materials = _myRenderer.materials;
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
        _movePosition = position;

        EnableMovement(true);
    }

    private void EnableMovement(bool enabled)
    {
        if (enabled)
        {
            PlayAnimation(UnitAnimationState.Move);
        }
        else
        {
            PlayAnimation(UnitAnimationState.Idle);
        }
        _shouldMove = enabled;
    }
    private void PlayAnimation(UnitAnimationState animationState)
    {
        // Debug.Log(unitData.GetAnimationState(animationState));
        //UnitData = null.... Why?
        _animator.Play(_unitData.GetAnimationState(animationState));
    }
    private void UpdateAttack()
    {
        UnitAnimationState attackState = (UnityEngine.Random.value < 0.5f) ? UnitAnimationState.Attack01 : UnitAnimationState.Attack02;
        UpdatePosition(_minDistance + attackRange, attackState);

        if (!_shouldAttack || attackRange <= 0)
        {
            return;
        }
        _attackCooldown -= Time.deltaTime;
        if (_attackCooldown < 0)
        {
            MessageQueueManager.Instance.SendMessage(
            new FireballSpawnMessage
            {
                Position = transform.position,
                Rotation = transform.rotation,
                Damage = attack
            });
            _attackCooldown = attackSpeed;
        }
    }
    private void UpdateDefense()
    {
        UpdatePosition(_minDistance, UnitAnimationState.Defense);
    }
    private void UpdateMovement()
    {
        UpdatePosition(_minDistance, UnitAnimationState.Move);
    }
    private void UpdateCollect()
    {
        UpdatePosition(_minDistance, UnitAnimationState.Collect);
    }
    private void UpdatePosition(float range, UnitAnimationState state)
    {
        if (!_shouldMove)
        {
            return;
        }
        if (Vector3.Distance(transform.position, _movePosition) < range)
        {
            _animator.Play(_unitData.GetAnimationState(state));
            _shouldMove = false;
            _shouldAttack = true;
            return;
        }
        UpdatePosition();
    }

    protected virtual void UpdatePosition()
    {
        Vector3 direction = (_movePosition - transform.position).normalized;
        transform.position += direction * Time.deltaTime * walkSpeed;
    }

    protected Vector3 GetFinalPosition()
    {
        return _movePosition;
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Plane"))
        {
            Debug.Log(_unitData.name);
            _animator.Play(_unitData.GetAnimationState(UnitAnimationState.Idle));
            _shouldMove = false;
        }
    }
}
