using UnityEngine;

public class TouchedByBullet : MonoBehaviour
{
    public Renderer[] _renderers;
    
    public Color[] _originalColors;
    public Color _hitColor;
    public EnemyScript _enemyScript;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _renderers = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < _renderers.Length; i++)
        {
            _originalColors[i] = _renderers[i].material.color;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag ==  "Bullet")
        {
            for (int i = 0; i < _renderers.Length; i++)
            {
                _renderers[i].material.color = _hitColor;
                
                Invoke("OriginalColors", 0.1f);
            }
            _enemyScript._life -= 1;
           Destroy(collision.gameObject);
        }
    }

    void OriginalColors()
    {
        for (int i = 0; i < _renderers.Length; i++)
        {
            _renderers[i].material.color = _originalColors[i];
        }
    }
}
