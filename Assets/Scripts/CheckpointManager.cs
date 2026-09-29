using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static bool _hasWolf;
    public static Vector3 _position = new Vector3(85.1f, -0.8f, 48.6f);

    public GameObject _player;
    public GameObject _wolf;
    public GameObject _talkingWolf;
    public bool hasWolf;
    public Vector3 _check;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player.transform.position = _position;
        if (_hasWolf)
        {
            _wolf.SetActive(true);
            Destroy(_talkingWolf);
            _wolf.transform.position = _position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        hasWolf = _hasWolf;
        _check = _position;

    }
}
