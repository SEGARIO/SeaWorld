using System.Collections.Generic;
using UnityEngine;
using static SO_Fish;

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

    [Header("Weather")]
    public GameObject _rain;
    public GameObject _sun;
    public GameObject _storm;
    public GameObject _snow;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       NewWeather();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void NewWeather()
    {
        _currentWeather = (SO_Fish.Weathers)(1 << Random.Range(0, 4));
        Invoke("NewWeather", Random.Range(30, 120));

        if (_currentWeather == Weathers.Snow)
        {

            _snow.SetActive(true);
            _rain.SetActive(false);
            _storm.SetActive(false);
            _sun.SetActive(false);
        }
        if (_currentWeather == Weathers.Sunny)
        {

            _snow.SetActive(false);
            _rain.SetActive(false);
            _storm.SetActive(false);
            _sun.SetActive(true);
        }
        if (_currentWeather == Weathers.Rainy)
        {

            _snow.SetActive(false);
            _rain.SetActive(true);
            _storm.SetActive(false);
            _sun.SetActive(false);
        }
        if (_currentWeather == Weathers.Thunder)
        {

            _snow.SetActive(false);
            _rain.SetActive(false);
            _storm.SetActive(true);
            _sun.SetActive(false);
        }
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
                        
                    }
                }
            }
        }
        Invoke("Bite", _currentFish._timeBeforeCatch);
    }

    void Bite()
    {
        Debug.Log("Bite");
        _vising._canCatch = true;
        _bulle.SetActive(true);
        Invoke("Run", _currentFish._timeBeforeRun);
    }

    void Run()
    {
        Debug.Log("Run");
        _vising._canCatch = false;
        _bulle.SetActive(false);
        RefreshFishList();
    }
}
