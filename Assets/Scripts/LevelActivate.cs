using UnityEngine;

public class LevelActivate : MonoBehaviour
{
    public int _levelToActivate;
    GameObject _water;
    public GameObject _visual;
    public EnemyScript _script;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _water = GameObject.FindWithTag("Water");
    }

    // Update is called once per frame
    public void WaterRise()
    {
        if(_levelToActivate != _water.transform.position.y)
        {
            _visual.SetActive(false);
            _script.enabled = false;
        }
        else
        {
            _visual.SetActive(true);
            _script.enabled = true;
        }
    }
}
