using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class CharaControl : MonoBehaviour
{
    public SO_Characters _character;
    PlayerManager _playerManager;
    public GameObject _ui;
    public GameObject _moveOpt;
    public GameObject _attackOpt;

    public Text _AttackCost;
    public Text _MoveCost;

    public UnityEngine.UI.Button _attackButton;
    public UnityEngine.UI.Button _moveButton;

    public float speed = 2f;

    private float t = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _ui.SetActive(false);
        _playerManager = FindObjectOfType<PlayerManager>();
        _playerManager._mana -= _character._invocationCost;
    }

    // Update is called once per frame
    void Update()
    {
        if(_playerManager._mana < _character._attackCost)
        {
            _attackButton.interactable = false;
        }
        if (_playerManager._mana < _character._moveCost)
        {
            _moveButton.interactable = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    Clicked();
                }
            }
        }


      
    }

    void Clicked()
    {
        _ui.SetActive(true);
        _MoveCost.text = "Cost : " + _character._moveCost;
        _AttackCost.text = "Cost : " + _character._attackCost;
        _playerManager._character = this;
    }


    private void OnMouseOver()
    {
        Debug.Log("Mouse Over");
    }

    public void MoveOptions()
    {
        _moveOpt.SetActive(true);
        _attackOpt.SetActive(false);
        _ui.SetActive(false);
    }

    public void AttackOptions()
    {
        _attackOpt.SetActive(true);
        _moveOpt.SetActive(false);
        _ui.SetActive(false);
    }

    public void Plays()
    {
        _attackOpt.SetActive(false);
        _moveOpt.SetActive(false);
        _ui.SetActive(false);
    }
}
