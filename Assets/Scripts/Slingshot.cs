using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class Slingshot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    [SerializeField] private GameObject prefabProjectile;

    [SerializeField] public float velocityMult = 8f;

    private Vector3 launchPos;
    private GameObject projectile;
    private bool aimingMode;
    private GameObject launchPoint;
    private Rigidbody projectileRigidbody;
    private SphereCollider sphereCollider;

    private void Awake()
    {
        // Find the launch point GameObject in the scene
        var launchPointTrans = GameObject.Find("LaunchPoint").transform;
        launchPoint = launchPointTrans.gameObject;
        launchPoint.SetActive(false);
        launchPos = launchPointTrans.position;
        
        sphereCollider = GetComponent<SphereCollider>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (!aimingMode) return;

        Vector2 mousePos2D = Mouse.current.position.ReadValue();
        float zDistanceFromCamera = -Camera.main.transform.position.z; // depth to convert screen to world
        Vector3 mousePos3D = Camera.main.ScreenToWorldPoint(new Vector3(mousePos2D.x, mousePos2D.y, zDistanceFromCamera));

        Vector3 mouseDelta = mousePos3D - launchPos;
        float maxMagnitude = sphereCollider.radius;

        if (mouseDelta.magnitude > maxMagnitude)
        {
            mouseDelta.Normalize();
            mouseDelta *= maxMagnitude;
        }

        // Move the projectile to this new position
        var projPos = launchPos + mouseDelta;
        projectile.transform.position = projPos;
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            aimingMode = false;
            projectileRigidbody.isKinematic = false;
            projectileRigidbody.linearVelocity = -mouseDelta * velocityMult;
            projectile = null;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        aimingMode = true;
        projectile = Instantiate(prefabProjectile);
        projectile.transform.position = launchPos;
        
        projectileRigidbody = projectile.GetComponent<Rigidbody>();
        projectileRigidbody.isKinematic = true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        launchPoint?.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        launchPoint?.SetActive(false);
    }
}