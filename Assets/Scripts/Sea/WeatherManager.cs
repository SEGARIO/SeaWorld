using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    public bool _isDry;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Change()
    {
        if(_isDry)
        {
            _isDry = false; 
        }
        else
        {
            _isDry = true;
        }
    }
}
