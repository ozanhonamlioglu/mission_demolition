using Unity.VisualScripting;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public static GameObject POI; // The static point of interest

    [SerializeField] private float easing = 0.05f;

    [SerializeField] public Vector2 minXY = Vector2.zero;

    private float camZ;

    private void Awake()
    {
        camZ = transform.position.z;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }

    private void FixedUpdate()
    {
        //if (POI.IsUnityNull()) return;
        // Get the position of the poi
        // var destination = POI.transform.position;

        Vector3 destination;
        if (POI.IsUnityNull())
        {
            destination = Vector3.zero;
        }
        else
        {
            destination = POI.transform.position;
            if (POI.CompareTag("Projectile"))
                if (POI.GetComponent<Rigidbody>().IsSleeping())
                {
                    POI = null;
                    return;
                }
        }

        destination = Vector3.Lerp(transform.position, destination, easing);
        destination.z = camZ;
        destination.x = Mathf.Max(minXY.x, destination.x);
        destination.y = Mathf.Max(minXY.y, destination.y);
        transform.position = destination;
        Camera.main.orthographicSize = destination.y + 10;
    }
}