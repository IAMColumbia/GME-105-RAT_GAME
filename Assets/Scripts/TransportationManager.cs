using UnityEngine;

public class TransportationManager : MonoBehaviour
{
    /// <summary>
    ///                 triggerTag - 
    ///                 cameraPosition - 
    ///                 playerDestination - 
    ///                 transportPoints - 
    ///                 cameraObject - 
    ///                 ratPlayer - 
    /// </summary>
    [System.Serializable]
    public class TransportPoint
    {
        public string triggerTag;
        public Vector2 cameraPosition;
        public Transform playerDestination;
    }

    public TransportPoint[] transportPoints;

    private CameraMove cameraObject;
    private Transform ratPlayer;


    // initiallizes stuff.
    void Start()
    {
        cameraObject = Camera.main.GetComponent<CameraMove>();
        ratPlayer = GameObject.FindGameObjectWithTag("Player").transform;
    }


    // when player enters a trigger collider, it checks if it matches a tag and if so, teleports player and camera.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (var point in transportPoints)
        {
            if (collision.CompareTag(point.triggerTag))
            {
                TeleportPlayer(point);
                return;
            }
        }
        Debug.Log("Trigger hit: " + collision.tag);
    }

    // teleports player and camera.
    private void TeleportPlayer(TransportPoint point)
    {
        cameraObject.WarpCamera(point.cameraPosition.x, point.cameraPosition.y);
        ratPlayer.position = point.playerDestination.position;
    }
}
