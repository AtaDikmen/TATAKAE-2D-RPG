using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField] private string sceneName;
    [SerializeField] private GameObject continueButton;
    [SerializeField] UI_FadeScreen fadeScreen;

    private void Start()
    {
        if(SaveManager.instance.HasSavedData() == false)
            continueButton.SetActive(false);

        fadeScreen.FadeIn();
    }
    public void ContinueGame(string _sceneName)
    {
        sceneName = _sceneName;
        StartCoroutine(LoadSceneWithFadeEffect(1.5f));
    }

    public void NewGame(string _sceneName)
    {
        sceneName = _sceneName;
        SaveManager.instance.DeleteSavedData();
        StartCoroutine(LoadSceneWithFadeEffect(1.5f));
    }

    public void ExitGame()
    {
        Debug.Log("Exit Game");
        Application.Quit();
    }

    IEnumerator LoadSceneWithFadeEffect(float _delay)
    {
        fadeScreen.anim.ResetTrigger("fadeIn");

        fadeScreen.FadeOut();

        yield return new WaitForSeconds(_delay);

        SceneManager.LoadScene(sceneName);
    }

    public void TutorialButton(string _sceneName)
    {
        sceneName = _sceneName;
        SaveManager.instance.DeleteSavedData();
        StartCoroutine(LoadSceneWithFadeEffect(1.5f));
    }
}
