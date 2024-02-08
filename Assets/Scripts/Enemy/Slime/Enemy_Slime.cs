

using Unity.VisualScripting;
using UnityEngine;

public enum SlimeType { big, medium, small }
public class Enemy_Slime : Enemy
{
    [Header("Slime Spesific")]
    [SerializeField] private SlimeType slimeType;
    [SerializeField] private int slimesToCreate;
    [SerializeField] private GameObject slimePrefab;
    [SerializeField] private Vector2 minCreationVelocity;
    [SerializeField] private Vector2 maxCreationVelocity;

    #region States

    public SlimeIdle_State idleState { get; private set; }
    public SlimeMove_State moveState { get; private set; }
    public SlimeBattle_State battleState { get; private set; }
    public SlimeAttack_State attackState { get; private set; }
    public SlimeStunned_State stunnedState { get; private set; }
    public SlimeDead_State deadState { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake();

        SetupDefaultFacingDir(-1);

        idleState = new SlimeIdle_State(this, stateMachine, "Idle", this);
        moveState = new SlimeMove_State(this, stateMachine, "Move", this);
        battleState = new SlimeBattle_State(this, stateMachine, "Move", this);
        attackState = new SlimeAttack_State(this, stateMachine, "Attack", this);
        stunnedState = new SlimeStunned_State(this, stateMachine, "Stunned", this);
        deadState = new SlimeDead_State(this, stateMachine, "Idle", this);
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

        
    }

    public override bool CanBeStunned()
    {
        if (base.CanBeStunned())
        {
            stateMachine.ChangeState(stunnedState);
            return true;
        }

        return false;
    }

    public override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deadState);

        if (slimeType == SlimeType.small)
            return;

        CreateSlimes(slimesToCreate, slimePrefab);
    }

    private void CreateSlimes(int _amountOfSlimes, GameObject _slimePrefab)
    {
        for (int i = 0; i < _amountOfSlimes; i++)
        {
            GameObject newSlime = Instantiate(_slimePrefab, transform.position, Quaternion.identity);

            newSlime.GetComponent<Enemy_Slime>().SetupSlime(facingDir);
        }
    }

    public void SetupSlime(int _facingDir)
    {
        if (_facingDir != facingDir)
            Flip();

        float xVelocity = Random.Range(minCreationVelocity.x, maxCreationVelocity.y);
        float yVelocity = Random.Range(minCreationVelocity.y, maxCreationVelocity.y);

        isKnocked = true;

        GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity * -facingDir, yVelocity);

        Invoke("CancelKnockback", 1.5f);
    }

    private void CancelKnockback() => isKnocked = false;
}
