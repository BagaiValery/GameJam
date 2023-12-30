using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishScript : MonoBehaviour
{
    
    [SerializeField] PauseGame pauseGame;

    [SerializeField] TimerManager timerManager;

    [Header("UI/UX")]
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject successPanel;
    [SerializeField] GameObject failurePanel;

    private void Start()
    {
        successPanel.SetActive(false);
        failurePanel.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Star")
        {
            FinishGame(true);
        }
    }

    public void FinishGame(bool success)
    {
        if (success) successPanel.SetActive(true);
        else failurePanel.SetActive(true);

        pauseGame.pauseGame();
        gameOverPanel.SetActive(true);

        timerManager.ShowFinishCounter();
    }
}
