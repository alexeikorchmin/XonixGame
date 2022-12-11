using UnityEngine;

public class PlayerGenerator : MonoBehaviour
{
    [SerializeField] private GameField gameField;
    [SerializeField] private BoundConverter boundConverter;
    [SerializeField] private int yOffset;

    public void GeneratePlayer()
    {
        var topLeftCorner = boundConverter.GetTopLeftCornerPosition();
        var bottomRightCorner = boundConverter.GetBottomRightCornerPosition();
        
        var x = Mathf.Abs(topLeftCorner.x) + Mathf.Abs(bottomRightCorner.x);
        var y = Mathf.Abs(topLeftCorner.y) + Mathf.Abs(bottomRightCorner.y);
        
        gameField.SetPlayerPosition( new Vector3Int(x / 2, y - yOffset, 0));
    }
}