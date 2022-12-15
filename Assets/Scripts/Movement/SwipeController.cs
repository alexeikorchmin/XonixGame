using UnityEngine;

public class SwipeController : MonoBehaviour
{
    [SerializeField] private PlayerMover playerMover;

    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector2 currentSwipe;

    private Swipes swipeDirection;
    private Swipes tempSwipeHorizontal;
    private Swipes tempSwipeVertical;

    private void Update()
    {
        CheckSwipeInput();
    }

    private void CheckSwipeInput()
    {
        if (Input.touchCount > 0)
        {       
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    firstPressPos = new Vector2(touch.position.x, touch.position.y);
                    break;

                case TouchPhase.Ended:
                    {
                        secondPressPos = new Vector2(touch.position.x, touch.position.y);

                        if (firstPressPos.x < secondPressPos.x)
                        {
                            currentSwipe.x = secondPressPos.x - firstPressPos.x;
                            tempSwipeHorizontal = Swipes.Right;
                        }
                        if (firstPressPos.x > secondPressPos.x)
                        {
                            currentSwipe.x = firstPressPos.x - secondPressPos.x;
                            tempSwipeHorizontal = Swipes.Left;
                        }
                        if (firstPressPos.y < secondPressPos.y)
                        {
                            currentSwipe.y = secondPressPos.y - firstPressPos.y;
                            tempSwipeVertical = Swipes.Up;

                        }
                        if (firstPressPos.y > secondPressPos.y)
                        {
                            currentSwipe.y = firstPressPos.y - secondPressPos.y;
                            tempSwipeVertical = Swipes.Down;
                        }

                        if (currentSwipe.x > currentSwipe.y)
                        {
                            swipeDirection = tempSwipeHorizontal;
                        }
                        else
                        {
                            swipeDirection = tempSwipeVertical;
                        }

                        playerMover.PlayerSwipeMover(swipeDirection);

                        firstPressPos = Vector2.zero;
                        secondPressPos = Vector2.zero;
                        currentSwipe = Vector2.zero;
                        tempSwipeVertical = Swipes.None;
                        tempSwipeHorizontal = Swipes.None;
                        swipeDirection = Swipes.None;

                        break;
                    }
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            playerMover.PlayerSwipeMover(Swipes.Up);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            playerMover.PlayerSwipeMover(Swipes.Down);
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            playerMover.PlayerSwipeMover(Swipes.Left);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            playerMover.PlayerSwipeMover(Swipes.Right);
        }
    }
}