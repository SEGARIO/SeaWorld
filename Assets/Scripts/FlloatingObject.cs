using UnityEngine;

public class FlloatingObject : MonoBehaviour
{
    public GameObject _water;
    bool _isFloating;
    public GameObject _waterVisual;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _water = GameObject.FindWithTag("Water");
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.y == _water.transform.position.y)
        {
            _isFloating = true;
        }

        if(_isFloating)
        {
            this.transform.position = new Vector3(this.transform.position.x, _waterVisual.transform.position.y, this.transform.position.z);
        }
    }
}
