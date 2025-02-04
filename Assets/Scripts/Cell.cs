
using UnityEngine;
public struct Cell
{
    public enum Type
    {
        Invalid,
        Empty,
        Mine,
        Number
    }

    public Type type;
    public Vector2Int position;
    public int number;

    public bool isRevealed;
    public bool isFlagged;
    public bool exploded;

}
