using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject levelPanel;
    [SerializeField] private GameObject creditPanel;
    [SerializeField] private GameObject settingPanel;
    [SerializeField] private GameObject highScorePanel;
    [SerializeField] private GameObject helpPanel;
    [SerializeField] private GameObject exitPanel;
    [SerializeField] private GameObject loadPanel;
    [SerializeField] private Slider loadingSlider;

    private void Start()
    {
        // Set Panel
        levelPanel.SetActive(false);
        creditPanel.SetActive(false);
        settingPanel.SetActive(false);
        highScorePanel.SetActive(false);
        loadPanel.SetActive(false);

        // Start BGM
        FindObjectOfType<AudioManager>().Play("BGM");

        // Ads
        AdsManager.instance.RequestBannerBottom();
    }

    public void PlayButton()
    {
        levelPanel.SetActive(true);
    }
    public void CloseLevelPanel()
    {
        levelPanel.SetActive(false);
    }

    public void OpenCreditPanel()
    {
        creditPanel.SetActive(true);
    }
    public void CloseCreditPanel()
    {
        creditPanel.SetActive(false);
    }

    public void OpenSettingPanel()
    {
        settingPanel.SetActive(true);
        // Check Ui
        FindObjectOfType<AudioManager>().CheckUI();
    }
    public void CloseSettingPanel()
    {
        // Safe setting data first
        SaveGame.SaveProgress(FindObjectOfType<SettingPanel>().GetSettingData());

        settingPanel.SetActive(false);       
    }

    public void OpenHighScorePanel()
    {
        highScorePanel.SetActive(true);
    }
    public void CloseHighScorePanel()
    {
        highScorePanel.SetActive(false);
    }

    public void OpenHelpPanel()
    {
        helpPanel.SetActive(true);
    }
    public void CloseHelpPanel()
    {
        helpPanel.SetActive(false);
    }

    public void ExitAplication()
    {
        Application.Quit();
    }
    public void OpenExitPanel()
    {
        exitPanel.SetActive(true);
    }
    public void CloseExitPanel()
    {
        exitPanel.SetActive(false);
    }

    public void LoadLevel(int a)
    {
        // Check data
        SaveData theData = SaveGame.LoadData();
        if(theData.GetLevelIsLockedData(a - 1) == false)
        {
            // Set item kind
            GameManager.SelectedItem = a;
            // Open loading panel
            loadPanel.SetActive(true);
            // Load game
            StartCoroutine(LoadAsynchoronously(1));
        }
    }
    IEnumerator LoadAsynchoronously(int sceneIndex)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!operation.isDone)
        {
            // Calculate progress
            float progress = Mathf.Clamp01(operation.progress / 0.9f);
            // Change slider
            loadingSlider.value = progress;
            // Return null
            yield return null;
        }
    }
}
