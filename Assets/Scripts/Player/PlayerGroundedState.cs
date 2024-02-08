using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerGroundedState : PlayerState
{
    public PlayerGroundedState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (player.skill.blackhole.blackholeUnlocked)
            {
                if (player.skill.blackhole.cooldownTimer > 0)
                {
                    player.fx.CreatePopUpText("Cooldown!");
                    return;
                }

                stateMachine.ChangeState(player.blackHole);
            }
            if (!player.skill.blackhole.blackholeUnlocked)
                player.fx.CreatePopUpText("Skill is locked");
        }

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            if(HasNoSword() && player.skill.sword.swordUnlocked && !player.isBusy)
                stateMachine.ChangeState(player.aimSword);
            if(!player.skill.sword.swordUnlocked)
                player.fx.CreatePopUpText("Skill is locked");
        }
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(player.skill.parry.parryUnlocked)
                stateMachine.ChangeState(player.counterAttack);
            if(!player.skill.parry.parryUnlocked)
                player.fx.CreatePopUpText("Skill is locked");
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
            stateMachine.ChangeState(player.primaryAttack);

        if (!player.IsGroundDetected())
            stateMachine.ChangeState(player.airState);

        if (Input.GetKeyDown(KeyCode.Space) && player.IsGroundDetected())
            stateMachine.ChangeState(player.jumpState);
    }

    private bool HasNoSword()
    {
        if (!player.sword)
            return true;

        player.sword.GetComponent<Sword_Skill_Controller>().ReturnSword();
        return false;
    }
}
