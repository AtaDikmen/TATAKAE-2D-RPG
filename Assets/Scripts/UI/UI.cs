using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour, ISaveManager
{
    [Header("End Screen")]
    [SerializeField] private UI_FadeScreen fadeScreen;
    [SerializeField] private GameObject endText;
    [SerializeField] private GameObject restartButton;
    [SerializeField] private GameObject mainMenuButton;
    [Space]

    [Header("Canvas List")]
    [SerializeField] private GameObject characterUI;
    [SerializeField] private GameObject skillTreeUI;
    [SerializeField] private GameObject craftUI;
    [SerializeField] private GameObject optionsUI;
    [SerializeField] private GameObject inGameUI;

    [Header("Tool Tips")]
    public UI_SkillTooltip skillTooltip;
    public UI_ItemTooltip itemToolTip;
    public UI_StatTooltip statToolTip;

    [Space]
    public UI_CraftWindow craftWindow;
    [SerializeField] private UI_VolumeSlider[] volumeSettings;

    private void Awake()
    {
        SwitchTo(skillTreeUI); // we need this to assign events on skill tree slots before we assign events on skill scripts
        fadeScreen.gameObject.SetActive(true);
    }


    void Start()
    {
        SwitchTo(inGameUI);

        itemToolTip.gameObject.SetActive(false);
        statToolTip.gameObject.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
            SwitchWithKey(inGameUI);

        if (Input.GetKeyDown(KeyCode.C))
            SwitchWithKey(characterUI);

        if (Input.GetKeyDown(KeyCode.B))
            SwitchWithKey(craftUI);

        if (Input.GetKeyDown(KeyCode.K))
            SwitchWithKey(skillTreeUI);

        if (Input.GetKeyDown(KeyCode.O))
            SwitchWithKey(optionsUI);
    }

    public void SwitchTo(GameObject _menu)
    {

        for (int i = 0; i < transform.childCount; i++)
        {
            bool fadeScreen = transform.GetChild(i).GetComponent<UI_FadeScreen>() != null; // we need this to keep fade screen game object active

            if(!fadeScreen)
                transform.GetChild(i).gameObject.SetActive(false);
        }

        if (_menu != null)
        {
            if(AudioManager.instance != null)
                AudioManager.instance.PlaySFX(7, null);
            _menu.SetActive(true);
        }

        if (GameManager.instance != null)
        {
            if (_menu == inGameUI)
                GameManager.instance.PauseGame(false);
            else
                GameManager.instance.PauseGame(true);
        }
    }

    public void SwitchWithKey(GameObject _menu)
    {
        if (_menu != null && _menu.activeSelf)
        {
            _menu.SetActive(false);
            CheckForInGameUI();
            return;
        }

        SwitchTo(_menu);
    }

    private void CheckForInGameUI()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf && transform.GetChild(i).GetComponent<UI_FadeScreen>() == null)
                return;
        }

        SwitchTo(inGameUI);
    }


    public void SwitchOnEndScreen()
    {
        fadeScreen.FadeOut();
        StartCoroutine(EndScreenCoroutine());
    }

    IEnumerator EndScreenCoroutine()
    {
        yield return new WaitForSeconds(1);
        endText.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        restartButton.SetActive(true);
        mainMenuButton.SetActive(true);
    }

    public void RestartGameButton() => GameManager.instance.RestartScene();
    public void MainMenuButton() => GameManager.instance.MainMenu();

    public void LoadData(GameData _data)
    {
        foreach (KeyValuePair<string, float> pair in _data.volumeSettings)
        {
            foreach (UI_VolumeSlider item in volumeSettings)
            {
                if(item.parameter == pair.Key)
                    item.LoadSlider(pair.Value);
            }
        }
    }

    public void SaveData(ref GameData _data)
    {
        _data.volumeSettings.Clear();

        foreach (UI_VolumeSlider item in volumeSettings)
        {
            _data.volumeSettings.Add(item.parameter, item.slider.value);
        }
    }
}
