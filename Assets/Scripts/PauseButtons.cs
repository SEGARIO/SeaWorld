using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseButtons : MonoBehaviour
{
    public GameObject _pausePanel;
    public void OnSelect(BaseEventData eventData)
    {
        transform.localScale = Vector3.one * 2f;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        transform.localScale = Vector3.one;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void Update()
    {
        if (_pausePanel.activeSelf)
        {
            if (Gamepad.current != null && Gamepad.current.startButton.wasPressedThisFrame)
            {
                Resume();
            }
            if (Gamepad.current != null && Gamepad.current.buttonEast.wasPressedThisFrame)
            {
                Resume();
            }
        }
    }

    public void Resume()
    {
        _pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
