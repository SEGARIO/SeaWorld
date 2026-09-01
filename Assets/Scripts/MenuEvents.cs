using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuEvents : MonoBehaviour
{
    public string _sceneName;

    [SerializeField] float rotationZ = 20f;
    [SerializeField] float t;
    public GameObject _camera;
    public float speed;
    bool _isTurning;
    public Animator _animatorFade;
    public Animator _CosmoAnim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _CosmoAnim.SetBool("Space", true);
    }

    // Update is called once per frame
    void Update()
    {
        if(_isTurning)
        {
            t = Mathf.Lerp(t, 1f, Time.deltaTime * speed);
            _camera.transform.localRotation = Quaternion.Lerp(Quaternion.identity, Quaternion.Euler(0, rotationZ, 0), t);
        }
    }

    public void ChangeScene()
    {
        _animatorFade.SetTrigger("Fade");
        Invoke("Scenee", 1);
    }

    public void LevelSelection()
    {
       
        _isTurning = true;
    }

    void Scenee()
    {
        SceneManager.LoadScene(_sceneName);
    }

    public void Finger()
    {
        _CosmoAnim.SetTrigger("Finger");
    }
}
