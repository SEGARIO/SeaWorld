using UnityEngine;

public class DisactivatePlayer : MonoBehaviour
{
    public GameObject _player;
    public GameObject _pc;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player.SetActive(false);
        _pc.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
