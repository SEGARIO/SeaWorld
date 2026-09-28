using TMPro;
using UnityEngine;

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
    }
    public void French()
    {
        LanguageManager._language = 0;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
