using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceShipLife : MonoBehaviour
{
    public int _life;
    public GameObject _fadeRed;
    public AdvancingScript _advance;
    public GameObject _visual;
    public GameObject _explo;
    bool canexplode;
    public GameObject _dial;
    public GameObject _text;
    public GameObject _fire;
    public GameObject[] _flames;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canexplode = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(_life <= 0)
        {
            Death();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.tag == "Enemy")
        {
            _life -= 1;
            _flames[(18 - _life)/2].SetActive(true);
            GameFeel.Instance.PlayJuice(1.5f, 0.6f);
            GameFeel.Instance.Flash(0.5f);

        }
    }

    void Death()
    {
        if(canexplode)
        {
            Destroy(_dial);
            Destroy(_fire);
            Destroy(_visual);
            Destroy(_text);
            Instantiate(_explo, this.transform.position, Quaternion.identity);
            GameFeel.Instance.PlayJuice(1.5f, 0.6f);
            canexplode = false;
        }

        _advance.enabled = false;
        _fadeRed.SetActive(true);
        Invoke("Restart", 3);
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
