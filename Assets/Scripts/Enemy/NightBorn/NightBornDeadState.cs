using UnityEngine;


public class NightBornDeadState : EnemyState
{
    private Enemy_NightBorn enemy;

    public NightBornDeadState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_NightBorn _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.anim.SetBool("Dead", true);


        stateTimer = .15f;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer > 0)
        {
            rb.velocity = new Vector2(2, 10);
            AudioManager.instance.PlaySFX(41, enemy.transform);
        }
    }
}
