using UnityEngine;
using UnityEngine.SceneManagement;

public class MazeHandler : MonoBehaviour
{
    public void OnMazeEnd()
    {
        if(GameManager.Levels.ContainsKey(GameManager.SelectedLevelIndex))
            GameManager.Levels[GameManager.SelectedLevelIndex] = LevelStatus.Completed;
        GameManager.SelectedLevelIndex = -1;
        GameManager.MessageForStartMenuScene = (true, "GoToPlayMenu");
        SceneManager.LoadScene("StartMenu");
    }

    public void OnMazeLeave()
    {
        GameManager.SelectedLevelIndex = -1;
        GameManager.MessageForStartMenuScene = (true, "GoToPlayMenu");
        SceneManager.LoadScene("StartMenu");
    }
}
