using UnityEngine;

public class EnemiesGenerator : MonoBehaviour
{
    [SerializeField] private GameField gameField;
    [SerializeField] private BoundConverter boundConverter;

    private int enemiesCount = 1;

    private int border;
    private int x;
    private int y;

    public void GenerateEnemy(int addEnemies)
    {
        enemiesCount += addEnemies;

        Vector3Int topLeftCorner = boundConverter.GetTopLeftCornerPosition();
        Vector3Int bottomRightCorner = boundConverter.GetBottomRightCornerPosition();
        
        x = Mathf.Abs(topLeftCorner.x) + Mathf.Abs(bottomRightCorner.x);
        y = Mathf.Abs(topLeftCorner.y) + Mathf.Abs(bottomRightCorner.y);
        
        for (var i = 0; i < enemiesCount; i++)
        {
            gameField.SetEnemyPosition(new Position(Random.Range(border + 5, x - border - 5), Random.Range(border + 5, y - border - 5)));
        }
    }

    private void Start()
    {
        border = gameField.GetBorderWidth();
    }
}