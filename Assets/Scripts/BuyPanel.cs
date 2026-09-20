using UnityEngine;

public class BuyPanel : MonoBehaviour
{
    public SO_Items _item;
    public PlayerController _playerController;
    public PlayerLife _life;
    public TriggerShoot _shoot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Yes()
    {
        _playerController.speed += _item._speed;
        //_life._maxlife += _item._life;
        _shoot.cooldown -= _item._shootSpeed / 10;
    }
}
