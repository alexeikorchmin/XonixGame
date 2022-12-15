using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ManagerUI : MonoBehaviour
{
    [SerializeField] private GeneratorManager generatorManager;
    [SerializeField] private SwipeController swipeController;
    [SerializeField] private GameField gameField;

    [SerializeField] private TMP_Text lifeText;
    [SerializeField] private TMP_Text fieldPercentsText;
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject winPanel;
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button playAgainButton;
    [SerializeField] private Button nextLevelButton;
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeButton;

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
        startGameButton.onClick.AddListener(delegate { StartGame(3, 0); });
        playAgainButton.onClick.AddListener(delegate { StartGame(3, 0); });
        nextLevelButton.onClick.AddListener(delegate { StartGame(0, 1); });
        pauseButton.onClick.AddListener(delegate { PauseResumeGame(true, false); });
        resumeButton.onClick.AddListener(delegate { PauseResumeGame(false, true); });
    }

    private void StartGame(int addLives, int addEnemies)
    {
        generatorManager.Init(addLives, addEnemies);
        menuPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        winPanel.SetActive(false);
        pauseButton.gameObject.SetActive(true);
        swipeController.SetCanTouchValue(true);
        gameField.SetUnitsMoveState(true);
    }

    private void PauseResumeGame(bool pauseGame, bool resumeGame)
    {
        resumeButton.gameObject.SetActive(pauseGame);
        startGameButton.gameObject.SetActive(resumeGame);
        menuPanel.SetActive(pauseGame);
        swipeController.SetCanTouchValue(resumeGame);
        gameField.SetUnitsMoveState(resumeGame);
    }
}