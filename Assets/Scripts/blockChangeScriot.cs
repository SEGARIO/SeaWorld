using UnityEngine;

public class blockChangeScriot : MonoBehaviour
{
    public Color _wetColor;
    public Renderer[] _renderers;
    public Material[] _wetSprites;
    public Material[] drySprites;
    public int _distance;
    public GameObject _water;
    bool _canChangeRandom;
    int random ;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _water = GameObject.FindWithTag("Water");
        random = Random.Range(0, 2);
        _canChangeRandom = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(this.transform.position.y > _water.transform.position.y + _distance)
        {
            for (int i = 0; i < _renderers.Length; i++)
            {
                _renderers[i].material = drySprites[i];
            }
        }
        if (this.transform.position.y == _water.transform.position.y + _distance + 1)
        {
            

            if(random == 0)
            {
                for (int i = 0; i < _renderers.Length; i++)
                {
                    _renderers[i].material = _wetSprites[i];
                }
            }
            else
            {
                for (int i = 0; i < _renderers.Length; i++)
                {
                    _renderers[i].material = drySprites[i];
                }
            }
          
        }

        if (this.transform.position.y < _water.transform.position.y + _distance + 1)
        {
            for (int i = 0; i < _renderers.Length; i++)
            {
                _renderers[i].material = _wetSprites[i];
            }
        }
        if (this.transform.position.y == _water.transform.position.y)
        {
            for (int i = 0; i < _renderers.Length; i++)
            {
                _renderers[i].material.color = _wetColor;
            }
        }
        if (this.transform.position.y == _water.transform.position.y + 1)
        {
            if (random == 0)
            {
                for (int i = 0; i < _renderers.Length; i++)
                {
                    _renderers[i].material.color = _wetColor;
                }
            }
           
        }
    }


}
