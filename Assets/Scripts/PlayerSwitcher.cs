using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerSwitcher : MonoBehaviour
{
    public GameObject _currentPlayer;
    
    public GameObject[] _players;
    public PlayerAI[] _ai;
    public Viser[] _visers;
    public PlayerLife[] _lifes;
    public TriggerShoot[] _shooters;
    public LerpPosition _camPivot;
    public int _deaths;
    public GameObject _deathPanel;

    public PlayerController _controller;
    public GameObject _visual;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    /*void Start()
    {
        for (int i = 0; i < _players.Length; i++)
        {
            if (_players[i] != _currentPlayer)
            {
                PlayerController _controller = _players[i].GetComponent<PlayerController>();
                _controller.enabled = false;
                _visers[i].enabled = false;
                _shooters[i]._isCurrentPlayer = false;
                _lifes[i]._isCurrentPlayer = false;
            }
            else
            {
                PlayerController _controller = _players[i].GetComponent<PlayerController>();
                _controller.enabled = true;
                _camPivot.target = _players[i].transform;
                _visers[i].enabled = true;
                _shooters[i]._isCurrentPlayer = true;
                _lifes[i]._isCurrentPlayer = true;
            }


        }
        ChangePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        if(_deaths >= _players.Length)
        {
            Death();
        }
    }

    public void ChangePlayer()
    {
        if (_currentPlayer == null || _players == null || _players.Length == 0)
            return;

        GameObject closestPlayer = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject player in _players)
        {
            if (player == null || player == _currentPlayer)
                continue;

            float distance = Vector3.Distance(
                _currentPlayer.transform.position,
                player.transform.position
            );

            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestPlayer = player;
            }
        }

        if (closestPlayer != null)
        {
            _currentPlayer = closestPlayer;
            Debug.Log("Nouveau joueur : " + _currentPlayer.name);
        }

        for(int i = 0; i < _players.Length; i++) 
        {
            if (_players[i] != _currentPlayer)
            {
                PlayerController _controller = _players[i].GetComponent<PlayerController>();
                _controller.enabled = false;
                _visers[i].enabled = false;
                _ai[i].enabled = true;
                _shooters[i]._isCurrentPlayer = false;
                _lifes[i]._isCurrentPlayer = false;
            }
            else
            {
                PlayerController _controller = _players[i].GetComponent<PlayerController>();
                _controller.enabled = true;
                _ai[i].enabled = false;
                _camPivot.target = _players[i].transform;
                _visers[i].enabled = true;
                _shooters[i]._isCurrentPlayer = true;
                _lifes[i]._isCurrentPlayer = true;
            }
           
        
        }

    }*/
    public void Death()
    {
        _controller.enabled = false;
        _visual.SetActive(false);
        Invoke("DeathPanel", 2);
    }
    void DeathPanel()
    {
        _deathPanel.SetActive(true);
    }
}

