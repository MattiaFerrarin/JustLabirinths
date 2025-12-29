using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;
using UnityEngine.UIElements;

public abstract class MazeGenerator
{
    protected int width;
    protected int depth;
    protected MazeNode[,] grid;
    protected System.Random rng = new System.Random();

    public MazeGenerator(int width, int depth)
    {
        this.width = width;
        this.depth = depth;

        grid = new MazeNode[width, depth];
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < depth; y++)
            {
                grid[x, y] = new MazeNode(x, y);
            }
        }
    }
    protected MazeGenerator() { }

    public abstract MazeNode[,] Generate();

    protected IEnumerable<MazeNode> GetShuffledUnvisitedNeighbors(MazeNode node)
    {
        List<MazeNode> neighbors = new List<MazeNode>();
        int x = node.X;
        int y = node.Y;

        if (x + 1 < width && !grid[x + 1, y].Visited)
            neighbors.Add(grid[x + 1, y]);
        if (x - 1 >= 0 && !grid[x - 1, y].Visited)
            neighbors.Add(grid[x - 1, y]);
        if (y + 1 < depth && !grid[x, y + 1].Visited)
            neighbors.Add(grid[x, y + 1]);
        if (y - 1 >= 0 && !grid[x, y - 1].Visited)
            neighbors.Add(grid[x, y - 1]);

        neighbors = neighbors.OrderBy((par) => rng.Next(0, 10)).ToList();

        return neighbors;
    }
    protected List<MazeNode> GetNeighbors(MazeNode node)
    {
        List<MazeNode> neighbors = new();

        int x = node.X;
        int y = node.Y;

        if (x + 1 < width) neighbors.Add(grid[x + 1, y]);
        if (x - 1 >= 0) neighbors.Add(grid[x - 1, y]);
        if (y + 1 < depth) neighbors.Add(grid[x, y + 1]);
        if (y - 1 >= 0) neighbors.Add(grid[x, y - 1]);

        return neighbors;
    }
    protected void ClearWalls(MazeNode a, MazeNode b)
    {
        if (b.X == a.X + 1)
        {
            a.WallRight = false;
            b.WallLeft = false;
        }
        else if (b.X == a.X - 1)
        {
            a.WallLeft = false;
            b.WallRight = false;
        }
        else if (b.Y == a.Y + 1)
        {
            a.WallFront = false;
            b.WallBack = false;
        }
        else if (b.Y == a.Y - 1)
        {
            a.WallBack = false;
            b.WallFront = false;
        }
    }
    protected void SetWalls(MazeNode a, MazeNode b, bool open)
    {
        if (b.X == a.X + 1)
        {
            a.WallRight = !open;
            b.WallLeft = !open;
        }
        else if (b.X == a.X - 1)
        {
            a.WallLeft = !open;
            b.WallRight = !open;
        }
        else if (b.Y == a.Y + 1)
        {
            a.WallFront = !open;
            b.WallBack = !open;
        }
        else if (b.Y == a.Y - 1)
        {
            a.WallBack = !open;
            b.WallFront = !open;
        }
    }
}

public class DFSMazeGenerator : MazeGenerator
{
    public DFSMazeGenerator(int width, int depth) : base(width, depth) { }

    public override MazeNode[,] Generate()
    {
        DFS(null, grid[0, 0]);
        return grid;
    }

    private void DFS(MazeNode parent, MazeNode current)
    {
        current.Visited = true;
        current.Parent = parent;

        if (parent != null)
        {
            parent.Children.Add(current);
            ClearWalls(parent, current);
        }

        foreach (MazeNode neighbor in GetShuffledUnvisitedNeighbors(current))
        {
            if (neighbor.Visited) // Because it evaluates the unvisited neighbours before the first recursion so it then could have already visited neighbours
                continue;
            DFS(current, neighbor);
        }
    }
}

public class OriginShiftMazeGenerator : MazeGenerator
{
    private MazeNode origin;
    public OriginShiftMazeGenerator(int width, int depth) : base()
    {
        this.width = width;
        this.depth = depth;

        grid = new MazeNode[width, depth];

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < depth; y++)
            {
                MazeNode node = new MazeNode(x,y);
                grid[x, y] = node;

                if (x == 0 && y == 0)
                {
                    origin = node; // origin
                    node.Parent = null;
                    continue;
                }

                MazeNode parent;

                if (x > 0)
                    parent = grid[x - 1, y];
                else
                    parent = grid[x, y - 1];

                node.Parent = parent;
                parent.Children.Add(node);

                ClearWalls(parent, node);
            }
        }
    }

    public override MazeNode[,] Generate()
    {
        for (int i=0; i<width*depth*15; i++)  // Decent number for a pretty random maze
        {
            OriginShift();
        }
        return grid;
    }

    public MazeNode[,] Step()
    {
        OriginShift();
        return grid;
    }

    private void OriginShift()
    {
        // 1. Random neighbour of the origin
        List<MazeNode> neighbors = GetNeighbors(origin);
        MazeNode next = neighbors[rng.Next(neighbors.Count)];

        // 2. Re-root tree between origin and rand neighbour
        ReRoot(origin, next);

        // 3. Move origin
        origin = next;
    }

    private void ReRoot(MazeNode oldRoot, MazeNode newRoot)
    {
        MazeNode oldParent = newRoot.Parent;
        if (oldParent != null)
        {
            oldParent.Children.Remove(newRoot);
            SetWalls(oldParent, newRoot, false);
        }

        newRoot.Children.Add(oldRoot);
        newRoot.Parent = null;
        oldRoot.Parent = newRoot;

        // Update walls
        SetWalls(newRoot, oldRoot, true);
    }
}

public static class MazeGeneratorFactory
{
    private static readonly Dictionary<Type, Func<int, int, MazeGenerator>> _registry = new()
    {
        { typeof(DFSMazeGenerator), (int w, int h) => new DFSMazeGenerator(w,h) },
        { typeof(OriginShiftMazeGenerator), (int w, int h) => new OriginShiftMazeGenerator(w,h) }
    };

    public static MazeGenerator CreateMazeGenerator(Type type, int width, int height)
    {
        if (_registry.TryGetValue(type, out var creator))
            return creator(width, height);
        throw new ArgumentException("Invalid Maze Generator Type");
    }
}