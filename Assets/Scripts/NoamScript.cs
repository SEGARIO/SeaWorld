using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NoamScript : MonoBehaviour
{
    public Camera _cam;
    public Text _names;
    public float _creditsDuration;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(Star._starNumber != 3)
        {
            gameObject.SetActive(false);
        }
        else
        {
            _cam.backgroundColor = Color.white;
            _names.color = Color.black;
        }

        Invoke("ChangeScene", _creditsDuration);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ChangeScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
