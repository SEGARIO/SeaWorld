using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip _clip;
    public AudioClip _nextClip;
    public AudioSource _source;
    bool _changes;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_changes)
        {
            _source.volume -= Time.deltaTime/3;
        }
        else {
            _source.volume += Time.deltaTime / 3;
        }

        if(_source.volume >= 0.2f)
        {
            _source.volume = 0.2f;
        }
    }
    public void ChangeMusic()
    {
        Invoke("MusicChanger", 1);
        _changes = true;
    }

    void MusicChanger()
    {
        _clip = _nextClip;
        _source.clip = _clip;
        _changes = false;
        _source.Play(); 
    }
}
