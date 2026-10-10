using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Piece : MonoBehaviour
{
    public bool _isPlayer;
    public bool _isBeingPlayed;
    public GameObject[] _outlineRends;

    public Color _playerColor;
    public Color _enemyColor;
    public Color _playedColor;
    public Color _overColor;

    public SO_ChessPiece _chessPiece;


    public UnityEvent onMouseEnter;
    public UnityEvent onMouseExit;
    public UnityEvent onMouseClick;

    private void Start()
    {
        for (int i = 0; i < _outlineRends.Length; i++)
        {
            if (_isPlayer)
            {
                _outlineRends[i].GetComponent<Renderer>().material.color = _playerColor;
            }
            else
            {
                _outlineRends[i].GetComponent<Renderer>().material.color = _enemyColor;
            }
        }
    }
    private void OnMouseEnter()
    {
        onMouseEnter?.Invoke();
        for (int i = 0; i < _outlineRends.Length; i++)
        {
            _outlineRends[i].GetComponent<Renderer>().material.color = _overColor;
        }
    }

    private void OnMouseExit()
    {
        onMouseExit?.Invoke();
       
        for (int i = 0; i < _outlineRends.Length; i++)
        {
            if (_isPlayer)
            {
                _outlineRends[i].GetComponent<Renderer>().material.color = _playerColor;
            }
            else
            {
                _outlineRends[i].GetComponent<Renderer>().material.color = _enemyColor;
            }
        }
    }

    private void OnMouseDown()
    {
        onMouseClick?.Invoke();
        for (int i = 0; i < _outlineRends.Length; i++)
        {
            _outlineRends[i].GetComponent<Renderer>().material.color = _playedColor;
        }
    }
}
