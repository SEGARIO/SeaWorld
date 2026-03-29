using UnityEngine;

public class blockChangeScriot : MonoBehaviour
{
    public Renderer[] _renderers;
    public Material[] _wetSprites;
    public Material[] drySprites;

    WeatherManager _weatherManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _weatherManager = FindObjectOfType<WeatherManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_weatherManager._isDry)
        {
            for (int i = 0; i < _renderers.Length; i++)
            {
                _renderers[i].material = drySprites[i];
            }
        }
        if (!_weatherManager._isDry)
        {
            for (int i = 0; i < _renderers.Length; i++)
            {
                _renderers[i].material = _wetSprites[i];
            }
        }
    }


}
