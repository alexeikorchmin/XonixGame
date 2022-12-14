using UnityEngine;

public class GeneratorManager : MonoBehaviour
{
    [SerializeField] private TilesGenerator tilesGenerator;
    [SerializeField] private PlayerGenerator playerGenerator;
    [SerializeField] private EnemiesGenerator enemiesGenerator;
    [SerializeField] private GameField gameField;
    [SerializeField] private PlayerMover playerMover;

    public void Init(int addLives, int addEnemies)
    {
        gameField.Init(addLives);
        tilesGenerator.SetGameFieldSize();
        tilesGenerator.GenerateGround();
        tilesGenerator.GenerateWater();
        playerGenerator.GeneratePlayer();
        enemiesGenerator.GenerateEnemy(addEnemies);

        playerMover.SetPlayerMoveState(true);
    }

    private void Start()
    {
        Init(0, 0);
    }
}