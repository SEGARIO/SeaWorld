using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    public Renderer[] _renderers;

    public Color[] _originalColors;
    public Color _hitColor;
    public float _life;
    public PlayerController _controller;
    public Transform _checkpointPosition;

    [SerializeField] private Volume volume;
    
    private Vignette vignette;
    public RectTransform _pivotLife;
    public RectTransform _pivotwLife;
    float _maxlife;

    public Image _image;
    public Color[] _colorsBar;
    public bool _isCurrentPlayer;
    public GameObject _deathPart;
    public bool _isMainChar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _maxlife = _life;
        if (volume.profile.TryGet(out vignette))
        {

            vignette.intensity.value = 0;
        }
       
        _renderers = GetComponentsInChildren<Renderer>();
        for (int i = 0; i < _renderers.Length; i++)
        {
            _originalColors[i] = _renderers[i].material.color;
        }
        _checkpointPosition = FindObjectOfType<GameManager>()._checkpoint;
        this.transform.position = _checkpointPosition.position;

    }

    // Update is called once per frame
    void Update()
    {
        if(_isCurrentPlayer)
        {
            if (_life > _maxlife)
            {
                _life = _maxlife;
            }
            vignette.intensity.value -= Time.deltaTime / 3;
            _pivotLife.localScale = new Vector3(_life / _maxlife, _pivotLife.localScale.y, _pivotLife.localScale.z);

            _pivotwLife.localScale = new Vector3(
        Mathf.Lerp(_pivotwLife.localScale.x, _pivotLife.localScale.x, 2 * Time.deltaTime),
        _pivotwLife.localScale.y,
        _pivotwLife.localScale.z
    );



            if (_life >= _maxlife / 2)
            {
                _image.color = _colorsBar[0];
            }
            if (_life >= _maxlife / 4 && _life < _maxlife / 2)
            {
                _image.color = _colorsBar[1];
            }
            if (_life < _maxlife / 4)
            {
                _image.color = _colorsBar[2];
            }
        }
      

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("TouchEnemy");
            for (int i = 0; i < _renderers.Length; i++)
            {
                _renderers[i].material.color = _hitColor;

                Invoke("OriginalColors", 0.1f);
            }
            _life -= 1;
            SetVignette(0.4f);
            GameFeel.Instance.PlayJuice(1.5f, 0.3f);
            GameFeel.Instance.Flash(0.1f);

            if(_life <= 0)
            {
                _life = 0;
                Death();
                
            }
        }
    }

    private void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("TouchEnemy");
            for (int i = 0; i < _renderers.Length; i++)
            {
                _renderers[i].material.color = _hitColor;

                Invoke("OriginalColors", 0.1f);
            }
            _life -= 0.01f;
            SetVignette(0.4f);
            GameFeel.Instance.PlayJuice(1.5f, 0.3f);
            GameFeel.Instance.Flash(0.1f);

            if (_life <= 0)
            {
                _life = 0;
                Death();

            }
        }
    }

    void OriginalColors()
    {
        for (int i = 0; i < _renderers.Length; i++)
        {
            _renderers[i].material.color = _originalColors[i];
        }
    }

    void Death()
    {
       

        if(_isMainChar)
        {
            FindObjectOfType<PlayerSwitcher>().Death();
        }
        else
        {
            Instantiate(_deathPart, this.transform.position, Quaternion.identity);
             Destroy(gameObject);
        }
    }

   
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Resetter()
    {
        FindObjectOfType<GameManager>()._checkpoint = null;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void SetVignette(float intensity)
    {
        if (vignette != null)
        {
            vignette.intensity.value = Mathf.Clamp01(intensity);
        }
    }
}
