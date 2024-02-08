using UnityEngine;


public class NightBornMoveState : NightBornGroundedState
{
    public NightBornMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_NightBorn _enemy) : base(_enemyBase, _stateMachine, _animBoolName, _enemy)
    {
    }
    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, rb.velocity.y);

        if (enemy.IsWallDetected() || !enemy.IsGroundDetected() || enemy.IsInvisibleWallDetected())
        {
            enemy.Flip();
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
