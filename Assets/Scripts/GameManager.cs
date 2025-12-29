using System;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    public static (int width,int height) CurrentlySelectedMazeSize { get; set; } = (0,0);
    public static Type MazeGeneratorType { get; set; }
    public static Dictionary<int, LevelStatus> Levels { get; set; } = new Dictionary<int, LevelStatus>()
    {
        {0, LevelStatus.NotStarted },
        {1, LevelStatus.NotStarted },
        {2, LevelStatus.NotStarted },
        {3, LevelStatus.NotStarted },
        {4, LevelStatus.NotStarted },
        {5, LevelStatus.NotStarted },
        {6, LevelStatus.NotStarted },
        {7, LevelStatus.NotStarted },
        {8, LevelStatus.NotStarted },
        {9, LevelStatus.NotStarted },
        {10, LevelStatus.NotStarted },
        {11, LevelStatus.NotStarted },
        {12, LevelStatus.NotStarted },
        {13, LevelStatus.NotStarted },
        {14, LevelStatus.NotStarted },
        {15, LevelStatus.NotStarted },
        {16, LevelStatus.NotStarted },
        {17, LevelStatus.NotStarted },
        {18, LevelStatus.NotStarted },
        {19, LevelStatus.NotStarted },
        {20, LevelStatus.NotStarted },
        {21, LevelStatus.NotStarted },
        {22, LevelStatus.NotStarted },
        {23, LevelStatus.NotStarted },
        {24, LevelStatus.NotStarted }
    };
    public static int SelectedLevelIndex { get; set; } = -1;
    public static (bool, string) MessageForStartMenuScene { get; set; } = (false, "");
}