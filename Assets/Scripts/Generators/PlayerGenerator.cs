using UnityEngine;

public class PlayerGenerator : MonoBehaviour
{
    [SerializeField] private GameField gameField;
    [SerializeField] private BoundConverter boundConverter;
    [SerializeField] private int yOffset;

    public void GeneratePlayer()
    {
        Vector3Int topLeftCorner = boundConverter.GetTopLeftCornerPosition();
        Vector3Int bottomRightCorner = boundConverter.GetBottomRightCornerPosition();
        
        int x = Mathf.Abs(topLeftCorner.x) + Mathf.Abs(bottomRightCorner.x);
        int y = Mathf.Abs(topLeftCorner.y) + Mathf.Abs(bottomRightCorner.y);
        
        gameField.SetPlayerPosition(new Position(x / 2, y - yOffset));
    }
}