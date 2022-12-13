using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private GameField gameField;
    [SerializeField] private bool isMoveActive;

    public void SetPlayerMoveState(bool isActive)
    {
        isMoveActive = isActive;
    }

    private void Update()
    {
        if (!isMoveActive)
            return;

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            gameField.MovePlayer(new Position(0, -1));
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            gameField.MovePlayer(new Position(0, 1));
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            gameField.MovePlayer(new Position(-1, 0));
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            gameField.MovePlayer(new Position(1, 0));
        }
    }
}