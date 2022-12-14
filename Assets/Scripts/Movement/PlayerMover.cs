using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private GameField gameField;

    public void PlayerSwipeMover(Swipes swipe)
    {
        if (swipe == Swipes.None)
        {
            gameField.MovePlayer(new Position(0, 0));
        }
        else if (swipe == Swipes.Up)
        {
            gameField.MovePlayer(new Position(0, 1));
        }
        else if (swipe == Swipes.Down)
        {
            gameField.MovePlayer(new Position(0, -1));
        }
        else if (swipe == Swipes.Left)
        {
            gameField.MovePlayer(new Position(-1, 0));
        }
        else if (swipe == Swipes.Right)
        {
            gameField.MovePlayer(new Position(1, 0));
        }
    }
}