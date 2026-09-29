using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class CheckpointManager : MonoBehaviour
{
    public static bool _hasWolf;
    public static Vector3 _position = new Vector3(93.08f, 2.178f, 48.21f);

    public GameObject _player;
    public GameObject _wolf;
    public GameObject _talkingWolf;
    public bool hasWolf;
    public Vector3 _check;
    public static Color _sunColor = new Color(255f, 244f, 214f) / 255f;
    public Light _light;
    public static string _lastSceneName =  "Subway";
    public MenuEvents _events;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(_events != null)
        {
            _events._sceneName = _lastSceneName;
        }
        _player.transform.position = _position;
        _light.color = _sunColor;
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

    public void ResetCoordinates()
    {
        _sunColor = new Color(255f, 244f, 214f) / 255f;
        _position = new Vector3(93.08f, 2.178f, 48.21f);
        _hasWolf = false;

    }
}
