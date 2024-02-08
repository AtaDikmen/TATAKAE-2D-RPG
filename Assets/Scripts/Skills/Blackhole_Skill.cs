using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Blackhole_Skill : Skill
{
    [SerializeField] private UI_SkillTreeSlot blackHoleUnlockButton;
    public bool blackholeUnlocked { get; private set; }
    [SerializeField] private int amountOfAttacks;
    [SerializeField] private float cloneCooldown;
    [SerializeField] private float blackholeDuration;
    [Space]
    [SerializeField] private GameObject blackholePrefab;
    [SerializeField] private float maxSize;
    [SerializeField] private float growSpeed;
    [SerializeField] private float shrinkSpeed;

    
    Blackhole_Skill_Controller currentBlackhole;

    [Header("Skill Reset Button")]
    [SerializeField] private UI_SkillReset skillResetButton;


    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void UseSkill()
    {
        base.UseSkill();

        player.StartCoroutine("BusyFor", 3.5f);

        GameObject newBlackHole = Instantiate(blackholePrefab, player.transform.position, Quaternion.identity);

        currentBlackhole = newBlackHole.GetComponent<Blackhole_Skill_Controller>();

        currentBlackhole.SetupBlackhole(maxSize,growSpeed,shrinkSpeed,amountOfAttacks,cloneCooldown, blackholeDuration);

        AudioManager.instance.PlaySFX(3, player.transform);
        AudioManager.instance.PlaySFX(6, player.transform);
    }

    protected override void Start()
    {
        base.Start();

        blackHoleUnlockButton.GetComponent<Button>().onClick.AddListener(UnlockBlackHole);
        skillResetButton.GetComponent<Button>().onClick.AddListener(CheckUnlock);
    }

    protected override void Update()
    {
        base.Update();
    }

    public bool SkillCompleted()
    {
        if (!currentBlackhole)
            return false;

        if (currentBlackhole.playerCanExitState)
        {
            currentBlackhole = null;
            return true;
        }

        return false;
    }

    public float GetBlackholeRadius()
    {
        return maxSize / 2;
    }

    protected override void CheckUnlock()
    {
        UnlockBlackHole();
    }

    private void UnlockBlackHole()
    {
        if(blackHoleUnlockButton.unlocked)
            blackholeUnlocked = true;
        else
            blackholeUnlocked = false;
    }
}
