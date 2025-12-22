using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor.Rendering;
using UnityEngine;

public class MazeRenderer : MonoBehaviour
{
    [SerializeField]
    private MazeCell _mazeCellPrefab;
    [SerializeField]
    private int _mazeWidth;
    [SerializeField]
    private int _mazeDepth;

    private float _mazeStepForOriginShiftTimeDelay = 1.0f;
    private float _mazeStepForOriginShiftTimeRate = 0.1f;

    private MazeCell[,] _mazeGrid;

    private MazeGenerator generator;

    private void Start()
    {
        generator = new OriginShiftMazeGenerator(_mazeWidth, _mazeDepth);
        MazeNode[,] mazeData = generator.Generate();

        _mazeGrid = new MazeCell[_mazeWidth, _mazeDepth];
        UpdateGraphics(mazeData);

        InvokeRepeating(nameof(OriginShiftStep), _mazeStepForOriginShiftTimeDelay, _mazeStepForOriginShiftTimeRate);
    }

    private void OriginShiftStep()
    {
        if (generator is OriginShiftMazeGenerator gen)
        {
            UpdateGraphics(gen.Step());
        }
    }

    private void UpdateGraphics(MazeNode[,] mazeData)
    {
        for (int x = 0; x < _mazeWidth; x++)
        {
            for (int z = 0; z < _mazeDepth; z++)
            {
                MazeNode node = mazeData[x, z];
                MazeCell cell = _mazeGrid[x, z];
                if (cell == null)
                {
                    cell = Instantiate(_mazeCellPrefab, new Vector3(x, 0, z), Quaternion.identity);
                    _mazeGrid[x, z] = cell;
                }

                cell.ResetWalls();
                cell.Visit();
                if (!node.WallLeft) cell.ClearLeftWall();
                if (!node.WallRight) cell.ClearRightWall();
                if (!node.WallFront) cell.ClearFrontWall();
                if (!node.WallBack) cell.ClearBackWall();
            }
        }

        _mazeGrid[0, 0].ClearBackWall();
        _mazeGrid[_mazeWidth - 1, _mazeDepth - 1].ClearFrontWall();
    }
}
