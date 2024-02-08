using UnityEngine;

public class UI_SkillReset : MonoBehaviour
{
    [SerializeField] private UI_SkillTreeSlot[] skillTreeSlots;

    public void ResetSkillTree()
    {
        foreach (var slot in skillTreeSlots)
        {
            if (slot.unlocked)
                PlayerManager.instance.currency += slot.skillCost;

            slot.skillImage.color = slot.lockedSkillColor;
            slot.unlocked = false;
        }
        AudioManager.instance.PlaySFX(26, null);
    }
}
