using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;
using static UnityEditor.FilePathAttribute;

public class Vising : MonoBehaviour
{
    public int _money;
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject objectToMove;
    [SerializeField] private LayerMask planeLayer;
    [SerializeField] private float maxDistance = 10f;
    public Animator _anim;
    bool _isVising = true;
    public GameObject _cane;
    FPSController _controller;
    public WeatherController _weather;
    int _life;
    public GameObject _parentVisual;
    Animator _animParent;
    public GameObject _viser;
    public GameObject _press;
    bool _canDisplay;
    GameObject _visual;
    bool _canClick;
    public Transform _sphereApat;
    public Transform _Apat;
    [SerializeField] private Volume volume;
    [SerializeField] private float intensity = -0.5f;
    public bool _canCatch;

    private LensDistortion lensDistortion;

    private Vignette vignette;
    GameFeel _gamefeel;
    private void Start()
    {
        _gamefeel = FindObjectOfType<GameFeel>();
        if (volume.profile.TryGet(out lensDistortion))
        {
            lensDistortion.intensity.value = intensity;
        }
        if (volume.profile.TryGet(out vignette))
        {
            vignette.intensity.value = 0;
        }
        _animParent = _parentVisual.GetComponent<Animator>();
        _controller = GetComponent<FPSController>();
        _canDisplay = true;
    }


    void Update()
    {
        if(_isVising)
        {
            _controller.mouseSensitivity = 500;
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance, planeLayer))
            {
                objectToMove.transform.position = hit.point;
            }

            if (Input.GetKeyDown(KeyCode.Mouse0) )
            {
                Debug.Log("REFRESH");
                _isVising = false;
                _cane.SetActive(true);
                _anim.SetTrigger("Drop");
                _weather.RefreshFishList();
                _life = _weather._currentFish._life;
                
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Mouse0) && _canCatch)
            {
                Catching();
            }

            if(_canDisplay)
            {
                _controller.mouseSensitivity = 200;
            }
           
        }
       
        
        if(_canClick)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                ClickNext();
            }
           
        }

    }

    void Catching()
    {
        Debug.Log("Click");
        if(!_isVising && _canCatch)
        {
           
            _life -= 1;
            
            if(_life > 0)
            {
                _gamefeel.PlayJuice(0.5f, 0.05f);
                SetIntensity(-0.5f);
                SetIntensityV(-0.4f);
            }

            if (_life <= 0 && _canDisplay)
            {
                Catch();
            }
        }
    }

    void Catch()
    {
        _canDisplay = false;
        //_animParent.SetTrigger("Activate");
        _parentVisual.SetActive(true);
        _visual = Instantiate(_weather._currentFish._visual, _parentVisual.transform.position, _parentVisual.transform.rotation, _parentVisual.transform);
        Debug.Log("Catched" + _weather._currentFish);
        _controller.mouseSensitivity = 0;
        _weather._currentFish._encounters += 1;
        _money += _weather._currentFish._price;
        _cane.SetActive(false);
        _viser.SetActive(false);
        _press.SetActive(false);
        Invoke("CanClick", 1);
        SetIntensity(0);
        SetIntensityV(0);
    }
    void CanClick()
    {
        _canCatch = false;
        _canClick = true;
    }
    void ClickNext()
    {
        _canDisplay = true;
        //_animParent.SetTrigger("Activate");
        _parentVisual.SetActive(false);
        Destroy(_visual);
        
        _anim.SetTrigger("Restart");
        _viser.SetActive(true);
        _sphereApat.localScale = new Vector3(1, 0.8f  ,1);
        _Apat.localScale = new Vector3(0, 0 ,0);
        
        _canClick = false;

        //_weather.RefreshFishList();
        Invoke("IsVising", 0.5f);

    }

    void IsVising()
    {
        _isVising = true;
    }
    public void SetIntensity(float value)
    {
        if (lensDistortion != null)
        {
            lensDistortion.intensity.value = value;
        }
    }
    public void SetIntensityV(float value)
    {
        if (vignette != null)
        {
            vignette.intensity.value = value;
        }
    }
}