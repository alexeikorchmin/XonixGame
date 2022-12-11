using UnityEngine;

public class EnemiesGenerator : MonoBehaviour
{
    [SerializeField] private GameField gameField;
    [SerializeField] private BoundConverter boundConverter;
    
    public void GenerateEnemy()
    {
        var topLeftCorner = boundConverter.GetTopLeftCornerPosition();
        var bottomRightCorner = boundConverter.GetBottomRightCornerPosition();
        
        var x = Mathf.Abs(topLeftCorner.x) + Mathf.Abs(bottomRightCorner.x);
        var y = Mathf.Abs(topLeftCorner.y) + Mathf.Abs(bottomRightCorner.y);
        
        gameField.SetEnemyPosition(new Vector3Int(x / 2, y / 2, 0));
    }
}