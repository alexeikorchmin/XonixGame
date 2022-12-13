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
    private int totalCells;
    private int groundCells;

    private int xOffset;
    private int yOffset;

    private Position playerPosition;
    private Position enemyPosition;

    private Items nextItem;
    private bool isCutting;
    private bool needCut;

    private HashSet<Position> tailCellsPositionsList = new HashSet<Position>();
    private List<HashSet<Position>> listAreas = new List<HashSet<Position>>();
    private Coroutine moveRoutine;

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

    public void SetTotalCells(int x, int y, int groundCellsCount)
    {
        totalCells = x * y;
        groundCells = groundCellsCount;
    }

    public void SetPlayerPosition(Position position)
    {
        playerPosition = new Position(position.x, position.y);
        SetGamefieldAndTilesData(position, Items.Player);
    }

    public void SetEnemyPosition(Position position)
    {
        enemyPosition = new Position(position.x, position.y);
        SetGamefieldAndTilesData(position, Items.Enemy);
    }

    public void MovePlayer(Position direction)
    {
        //StopAllCoroutines();
        //StartCoroutine(NewPlayerMove(direction));
        PlayerMovement(direction);
    }

    private Vector3Int GetPosition(Position position)
    {
        return new Vector3Int(position.x - xOffset, position.y - yOffset, 0);
    }

    private void SetGamefieldAndTilesData(Position position, Items item)
    {
        gameField[position.x, position.y] = (int)item;
        tilemap.SetTile(GetPosition(position), tilesDict[item]);
    }

    private void PlayerMovement(Position direction)
    {
        if (CanMove(direction) == false) return;

        if (needCut)
        {
            needCut = false;
            return;
        }

        if (isCutting)
        {
            SetGamefieldAndTilesData(playerPosition, Items.Tail);
            tailCellsPositionsList.Add(playerPosition);
        }
        else
        {
            SetGamefieldAndTilesData(playerPosition, Items.Ground);
        }

        nextItem = (Items)gameField[playerPosition.x + direction.x, playerPosition.y + direction.y];

        if (nextItem == Items.Tail)
        {
            Debug.Log("You Died");
        }

        if (nextItem == Items.Water)
        {
            isCutting = true;
        }

        if (isCutting && nextItem == Items.Ground)
        {
            isCutting = false;
            needCut = true;
            CalculateAreas();
            CutArea();
        }

        Position newPlayerPosition = new Position(playerPosition.x + direction.x, playerPosition.y + direction.y);
        SetGamefieldAndTilesData(newPlayerPosition, Items.Player);

        playerPosition += direction;
    }

    private bool CanMove(Position direction)
    {
        return playerPosition.x + direction.x >= 0 &&
               playerPosition.x + direction.x < width &&
               playerPosition.y + direction.y >= 0 &&
               playerPosition.y + direction.y < height;
    }

    private void CutArea()
    {
        int cellsCount = 0;
        int minCount = totalCells;
        int indexMinHashSet = 0;

        for (int i = 0; i < listAreas.Count; i++)
        {
            if (listAreas[i].Count < minCount)
            {
                minCount = listAreas[i].Count;
                indexMinHashSet = i;
            }
        }

        foreach (var pos in listAreas[indexMinHashSet])
        {
            SetGamefieldAndTilesData(pos, Items.Ground);
            cellsCount++;
        }

        foreach (var pos in tailCellsPositionsList)
        {
            SetGamefieldAndTilesData(pos, Items.Ground);
            cellsCount++;
        }

        listAreas.Clear();
        tailCellsPositionsList.Clear();
    }

    private bool ContainsThePosition(Position pos)
    {
        foreach (var position in listAreas)
        {
            if (position.Contains(pos))
                return true;
        }

        return false;
    }

    private void CalculateAreas()
    {
        listAreas.Clear();

        for (int i = groundCells; i < width - groundCells; i++)
        {
            for (int j = groundCells; j < height - groundCells; j++)
            {
                if (gameField[i, j] != (int)Items.Water)
                    continue;

                if (ContainsThePosition(new Position(i, j)) == false)
                {
                    var newArea = new HashSet<Position>();
                    listAreas.Add(newArea);
                    ExploreArea(newArea, new Position(i, j));
                }
            }
        }
    }

    private void ExploreArea(HashSet<Position> hashSet, Position startPoint)
    {
        if (hashSet.Contains(startPoint) ||
            gameField[startPoint.x, startPoint.y] == (int)Items.Ground ||
            gameField[startPoint.x, startPoint.y] == (int)Items.Player ||
            gameField[startPoint.x, startPoint.y] == (int)Items.Tail)
        {
            return;
        }

        if (gameField[startPoint.x, startPoint.y] == (int)Items.Water ||
            gameField[startPoint.x, startPoint.y] == (int)Items.Enemy)
        {
            hashSet.Add(startPoint);
        }

        ExploreArea(hashSet, startPoint + new Position(0, 1));
        ExploreArea(hashSet, startPoint + new Position(0, -1));
        ExploreArea(hashSet, startPoint + new Position(1, 0));
        ExploreArea(hashSet, startPoint + new Position(-1, 0));
    }
}