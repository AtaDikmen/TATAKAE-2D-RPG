using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dash_Skill : Skill
{
    [Header("Dash")]
    [SerializeField] private UI_SkillTreeSlot dashUnlockButton;
    public bool dashUnlocked { get; private set; }

    [Header("Clone on dash")]
    [SerializeField] private UI_SkillTreeSlot cloneOnDashUnlockButton;
    public bool cloneOnDashUnlocked { get; private set; }

    [Header("Clone on arrival")]
    [SerializeField] private UI_SkillTreeSlot cloneOnArrivalUnlockButton;
    public bool cloneOnArrivalUnlocked { get; private set; }

    [Header("Reset Button")]
    [SerializeField] private UI_SkillReset skillResetButton;


    public override void UseSkill()
    {
        base.UseSkill();
    }

    protected override void Start()
    {
        base.Start();

        dashUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockDash);
        cloneOnDashUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCloneOnDash);
        cloneOnArrivalUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockCloneOnArrival);

        skillResetButton.GetComponent<Button>().onClick.AddListener(CheckUnlock);

    }

    #region Unlock Region

    protected override void CheckUnlock()
    {
        UnlockDash();
        UnlockCloneOnDash();
        UnlockCloneOnArrival();
    }

    private void UnlockDash()
    {
        if (dashUnlockButton.unlocked)
            dashUnlocked = true;
        else
            dashUnlocked = false;
    }

    private void UnlockCloneOnDash()
    {
        if (cloneOnDashUnlockButton.unlocked)
            cloneOnDashUnlocked = true;
        else
            cloneOnDashUnlocked = false;
    }

    private void UnlockCloneOnArrival()
    {
        if(cloneOnArrivalUnlockButton.unlocked)
            cloneOnArrivalUnlocked = true;
        else
            cloneOnArrivalUnlocked = false;
    }

    #endregion

    public void CloneOnDash()
    {
        if (cloneOnDashUnlocked)
            SkillManager.instance.clone.CreateClone(player.transform, Vector3.zero);
    }

    public void CloneOnArrival()
    {
        if (cloneOnArrivalUnlocked)
            SkillManager.instance.clone.CreateClone(player.transform, Vector3.zero);
    }
}
