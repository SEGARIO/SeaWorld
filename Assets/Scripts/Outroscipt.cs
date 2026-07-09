using UnityEngine;
using UnityEngine.SceneManagement;

public class Outroscript : MonoBehaviour
{
    [SerializeField] private string _sceneName;
    private void Start()
    {
        Invoke("ChangeScene", 2);
    }
    public void ChangeScene()
    {
        SceneManager.LoadScene(_sceneName);
    }
}