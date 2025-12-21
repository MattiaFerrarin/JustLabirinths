using System.Collections.Generic;
using UnityEngine;

public class MazeNode
{
    public int X;
    public int Y;

    public bool Visited;

    // Normal tree for DFS and others and oriented tree for Origin Shift
    public MazeNode Parent;
    public Direction? DirectionFromParent;
    public List<MazeNode> Children = new();

    // Walls
    public bool WallLeft = true;
    public bool WallRight = true;
    public bool WallFront = true;
    public bool WallBack = true;

    public MazeNode(int x, int y)
    {
        X = x;
        Y = y;
    }
}

public enum Direction
{
    Left,
    Down,
    Right,
    Up
}