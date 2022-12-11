using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private GameField gameField;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            gameField.MovePlayer(Vector3Int.down);
        }
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            gameField.MovePlayer(Vector3Int.up);
        }
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gameField.MovePlayer(Vector3Int.left);
        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            gameField.MovePlayer(Vector3Int.right);
        }
    }
}