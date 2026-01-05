using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButtonHandler : MonoBehaviour
{
    public int LevelNumber;
    public AudioSource ButtonClickAudioSource;
    [SerializeField]
    private GameObject _TMPTextGameObject;
    private LevelStatus levelStatus;

    public void Start()
    {
        if (!GameManager.Levels.ContainsKey(LevelNumber))
            GameManager.Levels[LevelNumber] = LevelStatus.NotStarted;
        levelStatus = GameManager.Levels[LevelNumber];
        _TMPTextGameObject.GetComponent<TMP_Text>().text = (LevelNumber + 1).ToString();
        gameObject.GetComponent<Image>().color = levelStatus switch
        {
            LevelStatus.NotStarted => Color.gray,
            LevelStatus.Started => Color.yellow,
            LevelStatus.Completed => Color.green,
            _ => Color.gray,
        };
    }

    public void OnLevelButtonPressed()
    {
        ButtonClickAudioSource.Play();
        if (GameManager.Levels[LevelNumber] == LevelStatus.NotStarted)
        {
            GameManager.Levels[LevelNumber] = LevelStatus.Started;
        }
        GameManager.SelectedLevelIndex = LevelNumber;
        GameManager.CurrentlySelectedMazeSize = (LevelNumber * 2 + 6, LevelNumber * 2 + 6);
        GameManager.MazeGeneratorType = typeof(DFSMazeGenerator);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Game");
    }
}

public enum LevelStatus
{
    NotStarted,
    Started,
    Completed
}