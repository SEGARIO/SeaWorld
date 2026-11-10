using System.Threading;
using TMPro;
using UnityEngine;

public class MealGestion : MonoBehaviour
{
    public bool _hasMeal;
    public GameObject _ActiveClient;

    public string[] _DialoguesGood;
    public string[] _DialoguesMedium;
    public string[] _DialoguesBad;

     float _timer;
    public float _maxTimer = 100;
    public TMP_Text _dialogueText;
    public TMP_Text _timerText;
    public GameObject _dialogueBox;
    public GameObject _bag;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _timer = _maxTimer;
    }

    // Update is called once per frame
    void Update()
    {
        _timer -= Time.deltaTime;
        _timerText.text = "" + (int)_timer;
        if (_hasMeal)
        {
            _ActiveClient.SetActive(true);
            _bag.SetActive(true);
        }
        else
        {
           
        }

        if(_timer <= 0)
        { _timer = 0; }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_hasMeal && other.gameObject.tag == "Player")
        {
            _ActiveClient.SetActive(false);
            _bag.SetActive(false);
            FindObjectOfType<BuildingPicker>()._adressText.text = "";
            Invoke("NewOrder", 3);
            if(_timer >= _maxTimer / 2)
            {
                _dialogueText.text = _DialoguesGood[Random.RandomRange(0, _DialoguesGood.Length)];
            }
            if(_timer <= _maxTimer / 2 && _timer != 0)
            {
                _dialogueText.text = _DialoguesMedium[Random.RandomRange(0, _DialoguesMedium.Length)];
            }
            if (_timer <= 0)
            {
                _dialogueText.text = _DialoguesBad[Random.RandomRange(0, _DialoguesBad.Length)];
            }
            _dialogueBox.SetActive(true);
            _hasMeal = false;
        }
    }

    void NewOrder()
    {
        _dialogueBox.SetActive(false);
        FindObjectOfType<BuildingPicker>().NewAdress();
        _timer = _maxTimer;
    }
}
