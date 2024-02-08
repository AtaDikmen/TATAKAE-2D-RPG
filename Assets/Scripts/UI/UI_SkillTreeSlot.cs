using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SkillTreeSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISaveManager
{
    private UI ui;
    public Image skillImage;

    public int skillCost;
    [SerializeField] private string skillName;
    [TextArea]
    [SerializeField] private string skillDescription;
    public Color lockedSkillColor;

    public bool unlocked;

    [SerializeField] private UI_SkillTreeSlot[] shouldBeUnlocked;
    [SerializeField] private UI_SkillTreeSlot[] shouldBeLocked;


    private void OnValidate()
    {
        gameObject.name = "SkillTreeSlot_UI - " + skillName;
    }

    private void Awake()
    {
        GetComponent<Button>().onClick.AddListener(() => UnlockSkillSlot());
    }

    private void Start()
    {
        skillImage = GetComponent<Image>();
        ui = GetComponentInParent<UI>();

        skillImage.color = lockedSkillColor;

        if(unlocked)
            skillImage.color = Color.white;
    }

    public void UnlockSkillSlot()
    {
        if (unlocked)
            return;

        for (int i = 0; i < shouldBeUnlocked.Length; i++)
        {
            if (shouldBeUnlocked[i].unlocked == false)
            {
                Debug.Log("Can not unlock skill!");
                AudioManager.instance.PlaySFX(42, null);
                return;
            }
        }

        for (int i = 0;i < shouldBeLocked.Length; i++)
        {
            if (shouldBeLocked[i].unlocked == true)
            {
                Debug.Log("Can not unlock skill!");
                AudioManager.instance.PlaySFX(42, null);
                return;
            }
        }

        if (PlayerManager.instance.HaveEnoughMoney(skillCost) == false)
            return;

        AudioManager.instance.PlaySFX(26, null);
        unlocked = true;
        skillImage.color = Color.white;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.skillTooltip.ShowToolTip(skillDescription, skillName, skillCost);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.skillTooltip.HideToolTip();
    }

    public void LoadData(GameData _data)
    {
        if (_data.skillTree.TryGetValue(skillName, out bool value))
        {
            unlocked = value;
        }
    }

    public void SaveData(ref GameData _data)
    {
        if (_data.skillTree.TryGetValue(skillName, out bool value))
        {
            _data.skillTree.Remove(skillName);
            _data.skillTree.Add(skillName, unlocked);
        }
        else
            _data.skillTree.Add(skillName, unlocked);
    }
}
