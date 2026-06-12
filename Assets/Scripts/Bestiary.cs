using UnityEngine;
using UnityEngine.UI;

public class Bestiary : MonoBehaviour
{
    public SO_Fish[] _fishes;

    public GameObject _book;
    public SO_Fish _currentDisplayFish;
    public Transform _fishDisplay;
    GameObject _fishDisplayed;
    public GameObject _noData;
    public Animator _animBook;
    int value;

    public Text _nameText;
    public Text _description;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DisplayFish();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow))
        {
            PreviousFish();
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextFish();
        }
    }

    public void OpenBook()
    {
        _book.SetActive(true);
    }

    void NextFish()
    {

        value += 1;
        _animBook.SetTrigger("Play");
        DisplayFish();
       
    }
    void PreviousFish()
    {
        value -= 1;
        _animBook.SetTrigger("Unplay");
        DisplayFish();

    }

    void DisplayFish()
    {
        
        Destroy(_fishDisplayed);
        _currentDisplayFish = _fishes[value];
        
        if (_currentDisplayFish._encounters > 0)
        {
            _fishDisplayed = Instantiate(_currentDisplayFish._visual, _fishDisplay.position, Quaternion.identity, _fishDisplay);
            _nameText.text = _fishes[value]._name;
            _description.text = _fishes[value]._description;
        }
        else
        {
            _fishDisplayed = Instantiate(_noData, _fishDisplay.position, Quaternion.identity, _fishDisplay);
            _nameText.text = "???";
            _description.text = "Not encountered yet.";
        }
    }
}
