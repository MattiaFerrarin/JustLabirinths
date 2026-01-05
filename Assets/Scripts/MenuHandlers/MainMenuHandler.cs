using UnityEngine;

public class MainMenuHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject _playMenuGameObject;
    [SerializeField]
    private GameObject _optionsMenuGameObject;
    public void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if(GameManager.MessageForStartMenuScene.Item1)
            switch (GameManager.MessageForStartMenuScene.Item2)
            {
                case "GoToPlayMenu":
                    _playMenuGameObject.SetActive(true);
                    gameObject.SetActive(false);
                    break;
            }

        PlayerPrefsHandler.LoadLevels();
    }
    public void OnExitButtonPressed()
    {
        Application.Quit();
    }
}
