using UnityEngine;

public class LevelContainerHandler : MonoBehaviour
{
    [SerializeField]
    private LevelButtonHandler _levelButtonPrefab;

    public LevelButtonHandler[] LevelButtons { get; private set; }

    void Start()
    {
        LevelButtons = new LevelButtonHandler[GameManager.Levels.Count];
        for(int i=0; i<GameManager.Levels.Count; i++)
        {
            LevelButtonHandler button = Instantiate(_levelButtonPrefab,transform);
            button.transform.localPosition = new Vector3((i % 5) * 30  - 60, 150 - ((i / 5) * 30  - 60) - 150, 0);
            button.LevelNumber = i;
            LevelButtons[i] = button;
        }
    }
}
