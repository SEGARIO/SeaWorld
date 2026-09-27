using UnityEngine;
using UnityEngine.SceneManagement;

public class BossDeath : MonoBehaviour
{
    public GameObject[] _objectsToDestroy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        GameFeel.Instance.PlayJuice(1.5f, 0.6f);
        for (int i = 0; i < _objectsToDestroy.Length; i++)
        {
            Destroy(_objectsToDestroy[i]);
        }
        Invoke("ChangeScene", 12);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("SpaceTravel");
    }
}
