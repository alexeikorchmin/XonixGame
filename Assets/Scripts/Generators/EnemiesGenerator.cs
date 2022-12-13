using UnityEngine;

public class EnemiesGenerator : MonoBehaviour
{
    [SerializeField] private GameField gameField;
    [SerializeField] private BoundConverter boundConverter;
    
    public void GenerateEnemy()
    {
        Vector3Int topLeftCorner = boundConverter.GetTopLeftCornerPosition();
        Vector3Int bottomRightCorner = boundConverter.GetBottomRightCornerPosition();
        
        int x = Mathf.Abs(topLeftCorner.x) + Mathf.Abs(bottomRightCorner.x);
        int y = Mathf.Abs(topLeftCorner.y) + Mathf.Abs(bottomRightCorner.y);
        
        gameField.SetEnemyPosition(new Position(x / 2, y / 2));
    }
}