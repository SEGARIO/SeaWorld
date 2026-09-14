using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    public GameObject _pausePanel;
    public GameObject monBouton;

    private void Start()
    {
        _pausePanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(monBouton);
    }
    void Update()
    {
        if (Gamepad.current != null && Gamepad.current.startButton.wasPressedThisFrame)
        {
            Pause();
        }


    }

 


    void Pause()
    {
        _pausePanel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(monBouton);
        Time.timeScale = 0f;
    }

    public void Resume()
    {
        _pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}
