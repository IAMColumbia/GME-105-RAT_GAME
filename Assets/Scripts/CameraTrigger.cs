using UnityEngine;

public class CameraTrigger : MonoBehaviour
{

    public float _cameraIncX = 1;

    public float _cameraIncY = 1;

    public float cameraIncX 
    { 
        get { return _cameraIncX; } 
        set { _cameraIncX = value; }
    }

    public float cameraIncY
    {
        get { return _cameraIncY; }
        set { _cameraIncY = value; }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void flipCameraInc()
    {
        cameraIncX *= -1;
        cameraIncY *= -1;
    }

}
