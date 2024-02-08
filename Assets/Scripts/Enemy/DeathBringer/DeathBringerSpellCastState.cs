using UnityEngine;


public class DeathBringerSpellCastState : EnemyState
{
    private Enemy_DeathBringer enemy;

    private int amountOfSpells;
    private float spellTimer;

    public DeathBringerSpellCastState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_DeathBringer _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySFX(12, enemy.transform);

        amountOfSpells = enemy.amountOfSpells;
        spellTimer = .5f;

        enemy.stats.MakeInvincible(true);
    }

    public override void Update()
    {
        base.Update();

        spellTimer -= Time.deltaTime;

        if (CanCast())
            enemy.CastSpell();


        if(amountOfSpells <= 0)
            stateMachine.ChangeState(enemy.teleportState);
    }

    public override void Exit()
    {
        base.Exit();

        enemy.lastTimeCast = Time.time;

        enemy.stats.MakeInvincible(false);
    }

    private bool CanCast()
    {
        if (amountOfSpells > 0 && spellTimer < 0)
        {
            amountOfSpells--;
            float randomCd = Random.Range(1.5f, 2.1f);
            enemy.spellCooldown = randomCd;
            spellTimer = enemy.spellCooldown;
            return true;
        }

        return false;
    }
}
