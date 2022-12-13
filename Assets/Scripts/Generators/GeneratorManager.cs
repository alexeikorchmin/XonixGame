using UnityEngine;

public class GeneratorManager : MonoBehaviour
{
    [SerializeField] private TilesGenerator tilesGenerator;
    [SerializeField] private PlayerGenerator playerGenerator;
    [SerializeField] private EnemiesGenerator enemiesGenerator;
    [SerializeField] private GameField gameField;
    [SerializeField] private PlayerMover playerMover;

    public void Init(int lives)
    {
        gameField.Init(lives);
        tilesGenerator.SetGameFieldSize();
        tilesGenerator.GenerateGround();
        tilesGenerator.GenerateWater();
        playerGenerator.GeneratePlayer();
        enemiesGenerator.GenerateEnemy();

        playerMover.SetPlayerMoveState(true);
    }

    private void Start()
    {
        Init(0);
    }
}