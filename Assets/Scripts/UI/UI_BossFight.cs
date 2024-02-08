using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_BossFight : MonoBehaviour
{
    [SerializeField] private GameObject bossFightUI;
    [SerializeField] private Enemy_DeathBringer deathBringer;
    [SerializeField] private EnemyStats bossStat;
    [SerializeField] private Slider slider;

    void Start()
    {
        bossStat.onHealthChanged += UpdateHealthUI;
    }
    private void Update()
    {
        CheckForBossFightUI();
    }

    private void CheckForBossFightUI()
    {
        if (!deathBringer.bossFightBegun)
        {
            if(bossFightUI.activeSelf)
                bossFightUI.SetActive(false);

            return;
        }
        else
            bossFightUI.SetActive(true);
    }

    private void UpdateHealthUI()
    {
        slider.maxValue = bossStat.GetMaxHealthValute();
        slider.value = bossStat.currentHealth;
    }

}
