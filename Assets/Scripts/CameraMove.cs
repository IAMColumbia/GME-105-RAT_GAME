using Unity.VisualScripting;
using UnityEngine;
public class CameraMove : MonoBehaviour
{

    public float camMoveX = 5.33f;

    public float camMoveY = 4.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Vector3 myPos;
    void Start()
    {
        myPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveCamera(float newX, float newY)
    {
        Debug.Log("this is the camera.");

        myPos.x += newX * camMoveX;
        myPos.y += newY * camMoveY;
        transform.position = myPos;
    }
}
