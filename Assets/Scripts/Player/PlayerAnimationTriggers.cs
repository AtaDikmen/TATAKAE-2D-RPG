using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    private void AttackTrigger()
    {
        AudioManager.instance.PlaySFX(2, null); // attack sound effect

        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                EnemyStats _target = hit.GetComponent<EnemyStats>();

                if (_target != null)
                {
                    player.stats.DoDamage(_target);

                    player.fx.ScreenShake(new Vector3(0,.5f,0));

                    if (player.stats.fireDamage.GetValue() >= 1 || player.stats.iceDamage.GetValue() >= 1 || player.stats.lightingDamage.GetValue() >= 1)
                        player.stats.DoMagicalDamage(_target);
                }

                //Inventory.instance.GetEquipment(EquipmentType.Weapon).Effect(_target.transform);

                ItemData_Equipment weaponData = Inventory.instance.GetEquipment(EquipmentType.Weapon);

                if (weaponData != null)
                    weaponData.Effect(_target.transform);
            }
        }
    }

    private void ThrowSword()
    {
        SkillManager.instance.sword.CreateSword();
    }
}
