using UnityEngine;

public class PauseGame : MonoBehaviour
{
    public GameObject pausePanel;
    //public GameObject settingsPanel;

    void Start()
    {
        pausePanel.SetActive(false);
        //settingsPanel.SetActive(false);
    }

    public void ShowPauseMenu()
    {
        pausePanel.SetActive(true);
        pauseGame();
    }

    public void ClosePauseMenu()
    {
        pausePanel.SetActive(false);
        resumeGame();
    }

    public void pauseGame()
    {
        Time.timeScale = 0.0f;
    }

    public void resumeGame()
    {
        Time.timeScale = 1.0f;
    }

    //public void openSettings()
    //{
    //    pausePanel.SetActive(false);
    //    settingsPanel.SetActive(true);
    //}

    //public void closeSettings()
    //{
    //    pausePanel.SetActive(true);
    //    settingsPanel.SetActive(false);
    //}
}
