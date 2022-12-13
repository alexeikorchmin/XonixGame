using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManagerUI : MonoBehaviour
{
    [SerializeField] private GeneratorManager generatorManager;
    [SerializeField] private PlayerMover playerMover;

    [SerializeField] private TMP_Text lifeText;
    [SerializeField] private TMP_Text fieldPercentsText;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button nextLevelButton;

    public void UpdateLifeText(int lives)
    {
        lifeText.text = "Lives: " + lives;
    }

    public void UpdatePercentText(float percent)
    {
        fieldPercentsText.text = Math.Round(percent, 2) + "%";
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void ShowWinPanel()
    {
        winPanel.SetActive(true);
    }

    private void Awake()
    {
        playAgainButton.onClick.AddListener(delegate { StartGame(3); });
        nextLevelButton.onClick.AddListener(delegate { StartGame(0); });
    }

    private void StartGame(int lives)
    {
        generatorManager.Init(lives);
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
        playerMover.SetPlayerMoveState(true);
    }
}