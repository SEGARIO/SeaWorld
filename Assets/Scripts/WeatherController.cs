using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    [Header("Lightings")]
    public GameObject _morning;
    public GameObject _day;
    public GameObject _evening;
    public GameObject _night;

    float timer;
    bool isbiting;

    public float _timePasses;
    public int _timeDisplay;
    public Text _timeDisplayText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       NewWeather();
        _timePasses = 360;
    }

    // Update is called once per frame
    void Update()
    {
        _timePasses += Time.deltaTime;
        _timeDisplay = (int)_timePasses / 60;
        _timeDisplayText.text = $"{_timeDisplay}:00";
        
        if(_timePasses >= 1440)
        {
            _timePasses = 0;
        }

        if(_timePasses >= 360 && _timePasses < 540)
        {
            _currentTime = Times.Morning;
        }
        if (_timePasses >= 540 && _timePasses < 1080)
        {
            _currentTime = Times.Day;
        }
        if (_timePasses >= 1080 && _timePasses < 1260)
        {
            _currentTime = Times.Evening;
        }

        if (_timePasses >= 1260 || _timePasses < 360)
        {
            _currentTime = Times.Night;
        }
        if (_currentTime == Times.Morning)
        {
            Debug.Log("Morning");
            _morning.SetActive(true);
            _day.SetActive(false);
            _evening.SetActive(false);
            _night.SetActive(false);
        }
        if (_currentTime == Times.Day)
        {

            _morning.SetActive(false);
            _day.SetActive(true);
            _evening.SetActive(false);
            _night.SetActive(false);
        }
        if (_currentTime == Times.Evening)
        {

            _morning.SetActive(false);
            _day.SetActive(false);
            _evening.SetActive(true);
            _night.SetActive(false);
        }
        if (_currentTime == Times.Night)
        {

            _morning.SetActive(false);
            _day.SetActive(false);
            _evening.SetActive(false);
            _night.SetActive(true);
        }
        if (isbiting)
        {
            timer -= Time.deltaTime;

            if (timer < 0)
            {
                Run();
            }
        }
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
        isbiting = true;
        Debug.Log("Bite");
        _vising._canCatch = true;
        _bulle.SetActive(true);
        timer = _currentFish._timeBeforeRun;
    }

    void Run()
    {
        isbiting = false;
        Debug.Log("Run");
        _vising._canCatch = false;
        _bulle.SetActive(false);
        RefreshFishList();
    }
}
