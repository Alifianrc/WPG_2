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
    [SerializeField] private GameObject loadPanel;
    [SerializeField] private Slider loadingSlider;

    private void Start()
    {
        levelPanel.SetActive(false);
        loadPanel.SetActive(false);
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
    }
    public void CloseSettingPanel()
    {
        settingPanel.SetActive(false);
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void LoadLevel(int a)
    {
        // Set item kind
        GameManager.SelectedItem = a;
        // Open loading panel
        loadPanel.SetActive(true);
        // Load game
        StartCoroutine(LoadAsynchoronously(1));
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
