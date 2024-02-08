using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_NightBorn : Enemy
{
    [Header("Night Born Spesifics")]
    public float battleStateMoveSpeed;

    #region States

    public NightBornIdleState idleState { get; private set; }
    public NightBornMoveState moveState { get; private set; }
    public NightBornBattleState battleState { get; private set; }
    public NightBornAttackState attackState { get; private set; }
    public NightBornDeadState deadState { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake();

        idleState = new NightBornIdleState(this, stateMachine, "Idle", this);
        moveState = new NightBornMoveState(this, stateMachine, "Move", this);
        battleState = new NightBornBattleState(this, stateMachine, "Move", this);
        attackState = new NightBornAttackState(this, stateMachine, "Attack", this);
        deadState = new NightBornDeadState(this, stateMachine, "Dead", this);
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

    public override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deadState);
    }
}
