using UnityEngine;

public class MazeRenderer : MonoBehaviour
{
    [SerializeField]
    private MazeCell _mazeCellPrefab;
    [SerializeField]
    private GameObject _groundPlane;
    [SerializeField]
    private GameObject _entrance;
    [SerializeField]
    private GameObject _exit;
    [SerializeField]
    private GameObject _playerObject;

    private int _mazeWidth;
    private int _mazeDepth;

    private float _mazeStepForOriginShiftTimeDelay = 1.0f;
    private float _mazeStepForOriginShiftTimeRate = 0.1f;

    private MazeCell[,] _mazeGrid;

    private MazeGenerator generator;

    private void Start()
    {
        _mazeWidth = GameManager.CurrentlySelectedMazeSize.width;
        _mazeDepth = GameManager.CurrentlySelectedMazeSize.height;
        _groundPlane.transform.localScale = new Vector3(Mathf.Max(_mazeWidth / 10f, 0.15f) + 0.02f, 1, Mathf.Max(_mazeDepth / 10f, 0.15f) + 0.02f);
        _groundPlane.transform.position = new Vector3((_mazeWidth-1) / 2f, 0, (_mazeDepth-1) / 2f);
        _entrance.transform.position = new Vector3(0, -0.001f, -1.6f);
        _entrance.transform.rotation = Quaternion.Euler(0, 180, 0);
        _exit.transform.position = new Vector3(_mazeWidth - 1, -0.001f, _mazeDepth - 1 + 1.6f);
        _exit.transform.rotation = Quaternion.Euler(0, 0, 0);
        _playerObject.transform.position = new Vector3(0, 0.5f, -0.5f);

        generator = MazeGeneratorFactory.CreateMazeGenerator(GameManager.MazeGeneratorType, _mazeWidth, _mazeDepth);
        MazeNode[,] mazeData = generator.Generate();

        _mazeGrid = new MazeCell[_mazeWidth, _mazeDepth];
        UpdateGraphics(mazeData);

        if(generator is OriginShiftMazeGenerator)
        {
            InvokeRepeating(nameof(OriginShiftStep), _mazeStepForOriginShiftTimeDelay, _mazeStepForOriginShiftTimeRate);
        }
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
