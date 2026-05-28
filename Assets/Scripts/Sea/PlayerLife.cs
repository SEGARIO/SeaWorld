using UnityEngine;

public class PlayerLife : MonoBehaviour
{
    public Renderer[] _renderers;

    public Color[] _originalColors;
    public Color _hitColor;
    public int _life;
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
        if (collision.gameObject.tag == "Enemy")
        {
            for (int i = 0; i < _renderers.Length; i++)
            {
                _renderers[i].material.color = _hitColor;

                Invoke("OriginalColors", 0.1f);
            }
            _life -= 1;
            GameFeel.Instance.PlayJuice(1.5f, 0.3f);
            GameFeel.Instance.Flash(0.1f);

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
