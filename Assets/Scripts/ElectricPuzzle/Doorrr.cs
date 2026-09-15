using UnityEngine;

public class Doorrr : MonoBehaviour
{
    public NOCs _npcScript;
    public GameObject _toDestroy;
    float timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = 5;
    }

    // Update is called once per frame
    void Update()
    {
        if(_npcScript._index == 4)
        {

            GameFeel.Instance.PlayJuice(1.5f, 0.7f);
            _toDestroy.SetActive(false);
        }

        timer -= Time.deltaTime;

        if(timer < 4 && timer >3)
        {
            GameFeel.Instance.PlayJuice(8,10f);
        }
    }

    void Rumbling()
    {
        
    }
}
