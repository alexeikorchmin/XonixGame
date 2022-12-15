using UnityEngine;

public class GeneratorManager : MonoBehaviour
{
    [SerializeField] private TilesGenerator tilesGenerator;
    [SerializeField] private PlayerGenerator playerGenerator;
    [SerializeField] private EnemiesGenerator enemiesGenerator;
    [SerializeField] private GameField gameField;
    [SerializeField] private Grid grid;

    private bool canSetGridSize;

    public void Init(int addLives, int addEnemies)
    {
        gameField.Init(addLives);
        tilesGenerator.SetGameFieldSize();
        tilesGenerator.GenerateGround();
        tilesGenerator.GenerateWater();
        playerGenerator.GeneratePlayer();
        enemiesGenerator.GenerateEnemy(addEnemies);
        SetGridSize();
    }

    private void Start()
    {
        Init(0, 0);
        canSetGridSize = true;
    }

    private void SetGridSize()
    {
        if (!canSetGridSize)
            return;

        grid.transform.localScale = new Vector3(0.9f, 0.9f, 1);
        canSetGridSize = false;
    }
}