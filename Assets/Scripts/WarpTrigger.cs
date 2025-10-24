using UnityEngine;

public class WarpTrigger : MonoBehaviour
{

    public float _cameraWarpX;

    public float _cameraWarpY;

    public float _spawnPosX;

    public float _spawnPosY;

    public float cameraWarpX
    {
        get { return _cameraWarpX; }
        set { _cameraWarpX = value; }
    }

    public float cameraWarpY
    {
        get { return _cameraWarpY; }
        set { _cameraWarpY = value; }
    }

    public float spawnPosX
    {
        get { return _spawnPosX; }
        set { _spawnPosX = value; }
    }

    public float spawnPosY
    {
        get { return _spawnPosY; }
        set { _spawnPosY = value; }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
