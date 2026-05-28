using Unity.VisualScripting.ReorderableList;
using UnityEngine;
using UnityEngine.Playables;

public class CaseController : MonoBehaviour
{
    public Renderer _rend;
    public Color _originalColor;
    public Color _attackCol;
    public Color _attackCol2;
    public Color _moveCol;
    public Color _moveCol2;
    public Color _mouseOverCol;

    public bool _mouseOnIt;
    bool _caseSelectedA;
    bool _caseSelectedM;
    PlayerManager _playerManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _playerManager = FindObjectOfType<PlayerManager>();
        _originalColor = _rend.material.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            RefreshCases();
        }
    }

    private void OnMouseOver()
    {
        _mouseOnIt = true;
        Debug.Log("Mouse on it");
        if (!_caseSelectedA && !_caseSelectedM)
        {
            _rend.material.color = _mouseOverCol;
        }

        if (_caseSelectedA)
        {
            _rend.material.color = _attackCol;
        }
        if (_caseSelectedM)
        {
            _rend.material.color = _moveCol;
        }
    }

    private void OnMouseExit()
    {
        _mouseOnIt = false;
        if(!_caseSelectedA && !_caseSelectedM)
        {
            _rend.material.color = _originalColor;
        }

        if (_caseSelectedA)
        {
            _rend.material.color = _attackCol2;
        }
        if (_caseSelectedM)
        {
            _rend.material.color = _moveCol2;
        }

    }

    private void OnMouseDown()
    {
        Clicked();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Move")
        {
            if(!_mouseOnIt)
            {
                _rend.material.color = _moveCol2;
            }
            else
            {
                _rend.material.color = _moveCol;
            }
            _caseSelectedM = true;
            _caseSelectedA = false;
        }
        if (collision.gameObject.tag == "Attack")
        {
            if (!_mouseOnIt)
            {
                _rend.material.color = _attackCol2;
            }
            else
            {
                _rend.material.color = _attackCol;
            }
            _caseSelectedA = true;
            _caseSelectedM = false;
        }
        if (collision.gameObject == null)
        {
            if (!_mouseOnIt)
            {
                _rend.material.color = _originalColor;
            }
            else
            {
                _rend.material.color = _mouseOverCol;
            }
            _caseSelectedA = false;
            _caseSelectedM = false;
        }
    }

    void Clicked()
    {
        if(_caseSelectedM)
        {
            _playerManager._destination.SetActive(true);
            _playerManager._mana -= _playerManager._character._character._moveCost;
            Invoke("DestroyDest", 1);
            _playerManager._destination.transform.position = this.transform.position;
            _playerManager._canMoveCharacter = true;
            _playerManager.t = 0;
            _playerManager._character.Plays();
            RefreshCases();
        }
        if (_caseSelectedA)
        {
            _playerManager._mana -= _playerManager._character._character._attackCost;
           Instantiate(_playerManager._character._character._attackPrefab, this.transform.position, Quaternion.identity);
        }

    }
    void RefreshCases()
    {
        if (!_mouseOnIt)
        {
            _rend.material.color = _originalColor;
        }
        else
        {
            _rend.material.color = _mouseOverCol;
        }
        _caseSelectedA = false;
        _caseSelectedM = false;
    }

    void DestroyDest()
    {
        _playerManager._destination.SetActive(false);
        _playerManager._canMoveCharacter = false;
    }
}
