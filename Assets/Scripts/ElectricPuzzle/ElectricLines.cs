using UnityEngine;

public class ElectricLines : MonoBehaviour
{
    public GameObject _destroyableBox;
    public GameObject _toDisactivate;
    public GameObject _toActivate;
    public Material[] _materials;
    public Renderer _rend;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if( _destroyableBox != null )
        {
            _rend.material = _materials[0];
        }
        else
        {
            _rend.material = _materials[1];
            _toDisactivate.SetActive(false);
            _toActivate.SetActive(true);
            Destroy(this);
        }
    }
}
