using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_SkillTree : MonoBehaviour
{
    [Header("Souls Info")]
    [SerializeField] private TextMeshProUGUI currentSouls;

    private void Update()
    {
        if (currentSouls.text == (PlayerManager.instance.GetCurrency()).ToString())
            return;
        else
            UpdateSoulsUI();
    }


    private void UpdateSoulsUI()
    { 
        currentSouls.text = (PlayerManager.instance.GetCurrency()).ToString();
    }
}
