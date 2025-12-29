using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayMenuHandler : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField _mazeSizeInputField;
    [SerializeField]
    private GameObject _levelsContainer;

    public void OnMazeButtonPressed(string type)
    {
        if(type == "static") // Static Maze
        {
            if (int.TryParse(_mazeSizeInputField.text, out int size) && size > 0)
            {
                GameManager.MazeGeneratorType = typeof(DFSMazeGenerator);
                GameManager.CurrentlySelectedMazeSize = (size, size);
                SceneManager.LoadScene("Game");
            }
        }
        else if(type == "dynamic") // Dynamic Maze
        {
            if (int.TryParse(_mazeSizeInputField.text, out int size) && size > 0)
            {
                GameManager.MazeGeneratorType = typeof(OriginShiftMazeGenerator);
                GameManager.CurrentlySelectedMazeSize = (size, size);
                SceneManager.LoadScene("Game");
            }
        }
    }

    public void OnContinueButtonPressed()
    {
        List<LevelStatus> list = fromIntTDictToTList(GameManager.Levels);
        for(int i=0; i<list.Count; i++)
        {
            if (list[i] != LevelStatus.Completed)
            {
                _levelsContainer.GetComponent<LevelContainerHandler>().LevelButtons[i].OnLevelButtonPressed();
                return;
            }
        }
    }

    private List<T> fromIntTDictToTList<T>(Dictionary<int,T> dict)
    {
        int max = 0;
        foreach (var key in dict.Keys)
        {
            if (key > max)
                max = key;
        }
        T[] list = new T[max + 1];
        foreach (var kvp in dict)
        {
            list[kvp.Key] = kvp.Value;
        }
        return list.ToList();
    }
}
