using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeToEnglish : MonoBehaviour
{
    public TMP_Text _text;
    public string _englishText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(LanguageManager._language == 1)
        {
            _text.text = _englishText;
        }
      
    }

    public void English()
    {
        LanguageManager._language = 1;
        Debug.Log("1");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("2");
    }
    public void French()
    {
        LanguageManager._language = 0;
        Debug.Log("1");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("2");
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
