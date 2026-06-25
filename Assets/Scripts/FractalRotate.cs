using UnityEngine;

public class FractaleRotate : MonoBehaviour
{
    public float rotateX;
    public float rotateY;
    public float rotateZ;

    private void Update()
    {
        transform.Rotate(
            rotateX * Time.deltaTime,
            rotateY * Time.deltaTime,
            rotateZ * Time.deltaTime
        );
    }
}