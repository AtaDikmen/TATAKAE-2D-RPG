using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dodge_Skill : Skill
{
    [Header("Dodge")]
    [SerializeField] private UI_SkillTreeSlot unlockDodgeButton;
    [SerializeField] private int evasionAmount;
    public bool dodgeUnlocked;

    [Header("Mirage Dodge")]
    [SerializeField] private UI_SkillTreeSlot unlockMirageDodgeButton;
    public bool dodgeMirageUnlocked;

    [Header("Reset Button")]
    [SerializeField] private UI_SkillReset skillResetButton;

    protected override void Start()
    {
        base.Start();

        unlockDodgeButton.GetComponent<Button>().onClick.AddListener(UnlockDodge);
        unlockMirageDodgeButton.GetComponent<Button>().onClick.AddListener(UnlockMirageDodge);

        skillResetButton.GetComponent<Button>().onClick.AddListener(CheckUnlock);
    }

    #region Unlock Region

    protected override void CheckUnlock()
    {
        UnlockDodge();
        UnlockMirageDodge();
    }

    private void UnlockDodge()
    {
        if (unlockDodgeButton.unlocked && !dodgeUnlocked)
        {
            player.stats.evasion.AddModifier(evasionAmount);
            Inventory.instance.UpdateStatsUI();
            dodgeUnlocked = true;
        }
        if(!unlockDodgeButton.unlocked && dodgeUnlocked)
        {
            if(player.stats.evasion.modifiers.Count > 0)
                player.stats.evasion.RemoveModifier(evasionAmount);
                Inventory.instance.UpdateStatsUI();
            dodgeUnlocked = false;
        }
    }

    private void UnlockMirageDodge()
    {
        if (unlockMirageDodgeButton.unlocked)
            dodgeMirageUnlocked = true;
        else
            dodgeMirageUnlocked = false;
    }

    #endregion

    public void CreateMirageOnDodge()
    {
        if (dodgeMirageUnlocked)
            SkillManager.instance.clone.CreateClone(player.transform, new Vector3(2 * player.facingDir,0));
    }
}
