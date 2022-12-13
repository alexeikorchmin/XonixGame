using UnityEngine;

public class EnemiesGenerator : MonoBehaviour
{
    [SerializeField] private GameField gameField;
    [SerializeField] private BoundConverter boundConverter;
    [SerializeField] private int enemiesCount;

    private int border;
    private int x;
    private int y;

    public void ResetEnemiesCount()
    {
        enemiesCount = 0;
    }

    public void GenerateEnemy()
    {
        border = gameField.GetBorderWidth();
            
        Vector3Int topLeftCorner = boundConverter.GetTopLeftCornerPosition();
        Vector3Int bottomRightCorner = boundConverter.GetBottomRightCornerPosition();
        
        x = Mathf.Abs(topLeftCorner.x) + Mathf.Abs(bottomRightCorner.x);
        y = Mathf.Abs(topLeftCorner.y) + Mathf.Abs(bottomRightCorner.y);
        
        for (var i = 0; i < enemiesCount; i++)
        {
            gameField.SetEnemyPosition(new Position(Random.Range(border + 1, x - border - 1), Random.Range(border + 1, y - border - 1)));
        }

        enemiesCount++;
    }
}