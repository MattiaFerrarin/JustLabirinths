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

    private bool active = false;

    void Start()
    {
        _escapeMenuOverlay.SetActive(false);
        _levelText.text = GameManager.SelectedLevelIndex == -1 ? "Custom Level" : $"Level {_levelText.text}";
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            active = !active;
        }

        if (active)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            _escapeMenuOverlay.SetActive(true);
            _player.GetComponent<PlayerController>().IsLocked = true;
        }
        else
        {
            _escapeMenuOverlay.SetActive(false);
            _player.GetComponent<PlayerController>().IsLocked = false;
        }
    }
}
