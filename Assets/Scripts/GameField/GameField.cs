using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Sirenix.OdinInspector;

public class GameField : SerializedMonoBehaviour
{
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private Dictionary<Items, Tile> tilesDict = new Dictionary<Items, Tile>();

    private int[,] gameField;

    private int width;
    private int height;

    private int xOffset;
    private int yOffset;

    private Vector3Int playerPosition;
    private Vector3Int enemyPosition;

    private Items nextItem;
    private bool isCutting;
    private bool needCut;

    private int startCountX;
    private int endCountX;
    private int startCountY;
    private int endCountY;
    private bool canCountCells = true;

    public void SetGameFieldSize(int x, int y)
    {
        gameField = new int[x, y];

        width = x;
        height = y;

        xOffset = Mathf.CeilToInt((float)x / 2);
        yOffset = Mathf.CeilToInt((float)y / 2);
    }

    public void SetGameFieldData(Items item, int x, int y)
    {
        gameField[x + xOffset, y + yOffset] = (int)item;
    }

    public void SetPlayerPosition(Vector3Int position)
    {
        playerPosition = new Vector3Int(position.x, position.y, 0);
        gameField[position.x, position.y] = (int)Items.Player;
        tilemap.SetTile(GetPosition(position), tilesDict[Items.Player]);
    }

    public void SetEnemyPosition(Vector3Int position)
    {
        enemyPosition = new Vector3Int(position.x, position.y, 0);
        gameField[position.x, position.y] = (int)Items.Enemy;
        tilemap.SetTile(GetPosition(position), tilesDict[Items.Enemy]);
    }

    public void MovePlayer(Vector3Int direction)
    {
        StopAllCoroutines();
        StartCoroutine(MoveRoutine(direction));
    }

    private Vector3Int GetPosition(Vector3Int position)
    {
        return new Vector3Int(position.x - xOffset, position.y - yOffset, 0);
    }

    private void SetGamefieldAndTilesData(Vector3Int position, Items item)
    {
        gameField[position.x, position.y] = (int)item;
        tilemap.SetTile(GetPosition(position), tilesDict[item]);
    }

    private IEnumerator MoveRoutine(Vector3Int direction)
    {
        while (CanMove(direction))
        {
            if (needCut)
            {
                Debug.Log("Cut Field");

                Debug.Log($"startCountX = {startCountX}, startCountY = {startCountY}");
                Debug.Log($"endCountX = {endCountX}, endCountY = {endCountY}");

                CutTheField();
                needCut = false;
                break;
            }

            nextItem = (Items)gameField[playerPosition.x + direction.x, playerPosition.y + direction.y];
            if (nextItem == Items.Water)
            {
                isCutting = true;
                SetGamefieldAndTilesData(playerPosition, Items.Tail);

                if (canCountCells)
                {
                    startCountX = playerPosition.x - xOffset;
                    startCountY = playerPosition.y - yOffset;
                    //Debug.Log($"Water: startCountX = {startCountX}, startCountY = {startCountY}");
                    canCountCells = false;
                }
            }

            if (isCutting && nextItem == Items.Ground)
            {
                isCutting = false;
                needCut = true;

                endCountX = playerPosition.x - xOffset;
                endCountY = playerPosition.y - yOffset;
                //Debug.Log($"Ground: endCountX = {endCountX}, endCountY = {endCountY}");
                canCountCells = true;
            }

            if (!isCutting && nextItem == Items.Ground)
            {
                SetGamefieldAndTilesData(playerPosition, Items.Ground);
            }

            if (nextItem == Items.Tail)
            {
                Debug.Log("You Died");
                break;
            }

            Vector3Int newPlayerPosition = new Vector3Int(playerPosition.x + direction.x, playerPosition.y + direction.y);
            SetGamefieldAndTilesData(newPlayerPosition, Items.Player);

            playerPosition += direction;

            yield return new WaitForSeconds(0.015f);
        }

        StopAllCoroutines();
    }

    private bool CanMove(Vector3Int direction)
    {
        return playerPosition.x + direction.x >= 0 &&
               playerPosition.x + direction.x < width &&
               playerPosition.y + direction.y >= 0 &&
               playerPosition.y + direction.y < height;
    }

    private void CutTheField()
    {
        int newStartCountX;
        int newStartCountY;

        if (startCountX == endCountX)
        {
            if (endCountX < width / 2)
            {
                endCountX = gameField.GetLowerBound(1);
            }
            else
            {
                endCountX = gameField.GetUpperBound(1);
            }
        }
        else if (startCountX > endCountX)
        {
            newStartCountX = endCountX;
            endCountX = startCountX;
            startCountX = newStartCountX;
        }

        if (startCountY == endCountY)
        {
            if (endCountY < height / 2)
            {
                endCountY = gameField.GetLowerBound(2) - yOffset;
            }
            else
            {
                endCountY = gameField.GetUpperBound(2) - yOffset;
            }
        }
        else if (startCountY > endCountY)
        {
            newStartCountY = endCountY;
            endCountY = startCountY;
            startCountY = newStartCountY;
        }

        for (int i = startCountX; i <= endCountX; i++)
        {
            for (int j = startCountY; j <= endCountY; j++)
            {
                SetGamefieldAndTilesData(new Vector3Int(i, j), Items.Ground);
            }
        }
    }

    private bool CutArray()
    {
        foreach (var cell in gameField)
        {
            if (cell == (int)Items.Water)
            {
                return false;
            }
        }

        return true;
    }
}