using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RandomText : MonoBehaviour
{
    public TMP_Text text;
    public string[] _texts;
    public static int _index;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(_index <= 3)
        {
            text.text = _texts[_index];
        }
        else
        {
            text.text = _texts[Random.Range(0, _texts.Length)];
        }
        _index += 1;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
