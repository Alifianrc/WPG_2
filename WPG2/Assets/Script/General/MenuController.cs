using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;

    private bool menuIsActive;

    private bool canOpenMenu;

    private void Start()
    {
        // Close Menu Panel
        menuPanel.SetActive(false);
        menuIsActive = false;
        canOpenMenu = true;
    }

    public void FinishNextButton()
    {
        SceneManager.LoadScene(2);
    }

    public void OpenMenu()
    {
        if (canOpenMenu)
        {
            menuPanel.SetActive(true);
            menuIsActive = true;

            // Check Ui Button
            FindObjectOfType<AudioManager>().CheckUI();

            // Stop time
            Time.timeScale = 0;
        }
    }
    public void CloseMenu()
    {
        menuPanel.SetActive(false);
        menuIsActive = false;

        // Start time
        Time.timeScale = 1;
    }

    public void ReturnToMenu()
    {
        // Start time
        Time.timeScale = 1;
        // Load main menu
        SceneManager.LoadScene(0);
    }

    public void RestartGame()
    {
        // Start time
        Time.timeScale = 1;
        // Reload scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Set
    public void SetCanOpenMenu(bool can)
    {
        canOpenMenu = can;
    }
    public void SetMenuIsActive(bool isActive)
    {
        menuIsActive = isActive;
    }
    public bool GetMenuIsActive()
    {
        return menuIsActive;
    }
}
