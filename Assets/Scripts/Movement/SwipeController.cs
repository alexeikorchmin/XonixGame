using System.Collections;
using UnityEngine;

public class SwipeController : MonoBehaviour
{
    [SerializeField] private PlayerMover playerMover;
    [SerializeField] private float swipeLength = 10f;

    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector2 currentSwipe;

    private Swipes swipeDirection;
    private Swipes tempSwipeHorizontal;
    private Swipes tempSwipeVertical;

    private Coroutine routine;
    private bool canTouch;

    public void SetCanTouchValue(bool newCanTouch)
    {
        if (routine != null)
            StopCoroutine(routine);

        if (newCanTouch == false)
            canTouch = newCanTouch;
        else
            routine = StartCoroutine(BlockInputRoutine());
    }

    private IEnumerator BlockInputRoutine()
    {
        yield return new WaitForSeconds(0.5f);
        canTouch = true;
    }

    private void Update()
    {
        if (!canTouch)
            return;

        CheckSwipeInput();
    }

    private void CheckSwipeInput()
    {
        if (Input.touches.Length > 0)
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

                        float length;

                        if (currentSwipe.x > currentSwipe.y)
                        {
                            length = currentSwipe.x;
                            swipeDirection = tempSwipeHorizontal;
                        }
                        else
                        {
                            length = currentSwipe.y;
                            swipeDirection = tempSwipeVertical;
                        }

                        if (swipeDirection == tempSwipeHorizontal && length < Screen.width / swipeLength)
                        {
                            swipeDirection = Swipes.None;
                        }

                        if (swipeDirection == tempSwipeVertical && length < Screen.height / swipeLength)
                        {
                            swipeDirection = Swipes.None;
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