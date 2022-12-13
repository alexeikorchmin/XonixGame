using System;
using UnityEngine;

[Serializable]
public class Enemy : MonoBehaviour
{
    public Position Position;
    public int DirectionIndex;

    public Enemy(Position position, int directionIndex)
    {
        Position = position;
        DirectionIndex = directionIndex;
    }
}