using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Parry_Skill : Skill
{
    [Header("Parry")]
    [SerializeField] private UI_SkillTreeSlot parryUnlockButton;
    public bool parryUnlocked { get; private set; }

    [Header("Parry Restore")]
    [SerializeField] private UI_SkillTreeSlot restoreUnlockButton;
    [Range(0f, 1f)]
    [SerializeField] private float restoreHealthPercentage;
    public bool restoreUnlocked { get; private set; }

    [Header("Parry with Mirage")]
    [SerializeField] private UI_SkillTreeSlot parryWithMirageUnlockButton;
    public bool parryWithMirageUnlocked { get; private set; }

    [Header("Reset Button")]
    [SerializeField] private UI_SkillReset skillResetButton;


    public override void UseSkill()
    {
        base.UseSkill();

        if (restoreUnlocked)
        {
            int restoreAmount = Mathf.RoundToInt(player.stats.GetMaxHealthValute() * restoreHealthPercentage);
            player.stats.IncreaseHealthBy(restoreAmount);
        }
    }

    protected override void Start()
    {
        base.Start();

        cooldown = .5f;

        parryUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParry);
        restoreUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParryRestore);
        parryWithMirageUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockParryWithMirage);

        skillResetButton.GetComponent<Button>().onClick.AddListener(CheckUnlock);
    }

    #region Unlock Region

    protected override void CheckUnlock()
    {
        UnlockParry();
        UnlockParryRestore();
        UnlockParryWithMirage();
    }

    private void UnlockParry()
    {
        if(parryUnlockButton.unlocked)
            parryUnlocked = true;
        else
            parryUnlocked = false;
    }

    private void UnlockParryRestore()
    {
        if(restoreUnlockButton.unlocked)
            restoreUnlocked = true;
        else
            restoreUnlocked = false;
    }
    private void UnlockParryWithMirage()
    {
        if(parryWithMirageUnlockButton.unlocked)
            parryWithMirageUnlocked = true;
        else
            parryWithMirageUnlocked = false;
    }

    #endregion

    public void MakeMirageOnParry(Transform _respawnTransform)
    {
        if (parryWithMirageUnlocked)
            SkillManager.instance.clone.CreateClonWithDelay(_respawnTransform);
    }
}
