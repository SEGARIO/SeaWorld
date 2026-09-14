using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class PauseButtons : MonoBehaviour
{
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
}
