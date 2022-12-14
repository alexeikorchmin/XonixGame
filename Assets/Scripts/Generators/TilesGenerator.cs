using UnityEngine;
using UnityEngine.Tilemaps;

public class TilesGenerator : MonoBehaviour
{
    [SerializeField] private BoundConverter boundConverter;
    [SerializeField] private GameField gameField;
    [SerializeField] private int startGroundSize;
    
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Tile groundTile;
    [SerializeField] private Tile waterTile;
    
    public void SetGameFieldSize()
    {
        Vector3Int topLeftCorner = boundConverter.GetTopLeftCornerPosition();
        Vector3Int bottomRightCorner = boundConverter.GetBottomRightCornerPosition();
        
        int x = Mathf.Abs(topLeftCorner.x) + Mathf.Abs(bottomRightCorner.x);
        int y = Mathf.Abs(topLeftCorner.y) + Mathf.Abs(bottomRightCorner.y);

        gameField.SetGameFieldSize(x, y);
    }
    
    public void GenerateGround()
    {
        var topLeftCorner = boundConverter.GetTopLeftCornerPosition();
        var bottomRightCorner = boundConverter.GetBottomRightCornerPosition();
        
        for (int x = topLeftCorner.x; x < bottomRightCorner.x; x++)
        {
            for (int y = bottomRightCorner.y; y < topLeftCorner.y; y++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), groundTile);
                gameField.SetGameFieldData(Items.Ground, x, y);
            }
        }
    }
    
    public void GenerateWater()
    {
        Vector3Int topLeftCorner = boundConverter.GetTopLeftCornerPosition();
        Vector3Int bottomRightCorner = boundConverter.GetBottomRightCornerPosition();

        topLeftCorner.x += startGroundSize;
        topLeftCorner.y -= startGroundSize;
        bottomRightCorner.x -= startGroundSize;
        bottomRightCorner.y += startGroundSize;
        
        for (int x = topLeftCorner.x; x < bottomRightCorner.x; x++)
        {
            for (int y = bottomRightCorner.y; y < topLeftCorner.y; y++)
            {
                tilemap.SetTile(new Vector3Int(x, y, 0), waterTile);
                gameField.SetGameFieldData(Items.Water, x, y);
            }
        }

        int x2 = Mathf.Abs(topLeftCorner.x) + Mathf.Abs(bottomRightCorner.x);
        int y2 = Mathf.Abs(topLeftCorner.y) + Mathf.Abs(bottomRightCorner.y);

        gameField.SetTotalCells(x2, y2, startGroundSize);
    }
}