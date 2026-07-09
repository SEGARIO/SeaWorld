using UnityEngine;

public class Doorrr : MonoBehaviour
{
    public NOCs _npcScript;
    public GameObject _toDestroy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_npcScript._index == 4)
        {

            GameFeel.Instance.PlayJuice(1.5f, 0.7f);
            _toDestroy.SetActive(false);
        }
    }
}
