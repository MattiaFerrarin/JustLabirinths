using TMPro;
using UnityEngine;

public class EscapeMenuHandler : MonoBehaviour
{
    [SerializeField]
    private GameObject _escapeMenuOverlay;
    [SerializeField]
    private TMP_Text _levelText;
    [SerializeField]
    private GameObject _player;

    private bool Active = false;

    void Start()
    {
        _escapeMenuOverlay.SetActive(false);
        _levelText.text = GameManager.SelectedLevelIndex == -1 ? "Custom Level" : $"Level {GameManager.SelectedLevelIndex+1}";
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Active = !Active;
        }

        if (Active)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _player.GetComponent<PlayerController>().IsLocked = true;
            _escapeMenuOverlay.SetActive(true);
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _player.GetComponent<PlayerController>().IsLocked = false;
            _escapeMenuOverlay.SetActive(false);
        }
    }

    public void Deactivate()
    {
        Active = false;
    }
}
