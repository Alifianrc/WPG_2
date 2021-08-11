using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;

    private bool menuIsActive;

    private void Start()
    {
        // Close Menu Panel
        menuPanel.SetActive(false);
        menuIsActive = false;
    }

    public void FinishNextButton()
    {
        SceneManager.LoadScene(2);
    }

    public void OpenMenu()
    {
        menuPanel.SetActive(true);
        menuIsActive = true;

        // Check Ui Button
        FindObjectOfType<AudioManager>().CheckUI();
    }
    public void CloseMenu()
    {
        menuPanel.SetActive(false);
        menuIsActive = false;
    }

    public bool GetMenuIsActive()
    {
        return menuIsActive;   
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
