using System.Collections.Generic;
using UnityEngine;

public class WeatherController : MonoBehaviour
{
    public SO_Fish _currentFish;
    [SerializeField] private List<SO_Fish> fishes = new();
    public Bestiary _bestiary;
    public SO_Fish.Apat _currentApat;
    public SO_Fish.Weathers _currentWeather;
    public SO_Fish.Times _currentTime;
    public GameObject _bulle;
    public Vising _vising;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RefreshFishList()
    {
        fishes.Clear();

        SO_Fish[] loadedFishes = Resources.LoadAll<SO_Fish>("");

        fishes.AddRange(loadedFishes);

        Debug.Log($"Loaded {fishes.Count} fishes.");

        for (int i = 0; i < _bestiary._fishes.Length; i++)
        {
            if ((_bestiary._fishes[i]._apat & _currentApat) != 0)
            {
                if ((_bestiary._fishes[i]._times & _currentTime) != 0)
                {
                    if ((_bestiary._fishes[i]._weather & _currentWeather) != 0)
                    {
                        fishes.Add(_bestiary._fishes[i]);
                        _currentFish = fishes[Random.Range(0, fishes.Count)];
                        Invoke("Bite", _currentFish._timeBeforeCatch);
                    }
                }
            }
        }
    }

    void Bite()
    {
        _vising._canCatch = true;
        _bulle.SetActive(true);    
    }
}
