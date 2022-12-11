using UnityEngine;

public class GeneratorManager : MonoBehaviour
{
    [SerializeField] private TilesGenerator tilesGenerator;
    [SerializeField] private PlayerGenerator playerGenerator;
    [SerializeField] private EnemiesGenerator enemiesGenerator;

    private void Start()
    {
        tilesGenerator.SetGameFieldSize();
        tilesGenerator.GenerateGround();
        tilesGenerator.GenerateWater();
        playerGenerator.GeneratePlayer();
        enemiesGenerator.GenerateEnemy();
    }
}