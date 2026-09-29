using UnityEngine;
using UnityEngine.SceneManagement;

public class AdvancingScript : MonoBehaviour
{
    public float _speed;
    public float _spaceSpeed;
    public Animator _anim;
    public Animator _fadeout;
    public Camera _cam;
    public AdvancingScript _trainSpaceScript;
    public Advance advanceStar;
    public GameObject _viser;
    public bool _canVibrate;
    public SpaceShipLife _life;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + Time.deltaTime * _speed);

        if(this.transform.position.z >= 2367 && this.transform.position.x == 0)
        {
            _anim.enabled = true;
            _anim.SetTrigger("Up");
          
            Invoke("Space", 9);
        }
        if (this.transform.position.z >= 6000)
        {
            _anim.enabled = true;
            _anim.SetTrigger("Right");
           
        }

        if(_canVibrate)
        {
            GameFeel.Instance.PlayJuice(1.5f, 0.6f);
        }

        if (_anim.GetCurrentAnimatorStateInfo(0).IsName("SpaceshipRight") && _anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
        {
            SceneManager.LoadScene("Credits");
        }
    }

    void Space()
    {
        Destroy(_viser);
        _cam.clearFlags = CameraClearFlags.Skybox;
        _fadeout.SetTrigger("Y");
        _anim.enabled = false;
        _speed = _spaceSpeed;
        _trainSpaceScript.enabled = true;
        // advanceStar.enabled = true;
        _life._life = 20;
        for (int i = 0; i < 9; i++)
        {
            _life._flames[i].SetActive(false);
        }
    }
}
