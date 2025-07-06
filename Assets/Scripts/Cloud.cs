using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Cloud : MonoBehaviour
{
    [SerializeField] private GameObject cloudSphere;
    [SerializeField] private int numSpheresMin = 6;
    [SerializeField] private int numSpheresMax = 10;
    [SerializeField] private Vector3 sphereOffsetScale = new(5, 2, 1);
    [SerializeField] private Vector2 sphereScaleRangeX = new(4, 8);
    [SerializeField] private Vector2 sphereScaleRangeY = new(3, 4);
    [SerializeField] private Vector2 sphereScaleRangeZ = new(2, 4);
    [SerializeField] private float scaleYMin = 2f;

    private List<GameObject> spheres;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        MakeCloud();
    }

    private void MakeCloud()
    {
        spheres = new List<GameObject>();
        var num = Random.Range(numSpheresMin, numSpheresMax);
        for (var i = 0; i < num; i++)
        {
            var sp = Instantiate(cloudSphere
            );
            spheres.Add(sp);
            var spTrans = sp.transform;
            spTrans.SetParent(transform);
            var offset = Random.insideUnitSphere;
            offset.x *= sphereOffsetScale.x;
            offset.y *= sphereOffsetScale.y;
            offset.z *= sphereOffsetScale.z;
            spTrans.localPosition = offset;

            var scale = Vector3.one;
            scale.x = Random.Range(sphereScaleRangeX.x, sphereScaleRangeX.y);
            scale.y = Random.Range(sphereScaleRangeY.x, sphereScaleRangeY.y);
            scale.z = Random.Range(sphereScaleRangeZ.x, sphereScaleRangeZ.y);
            scale.y *= 1 - Mathf.Abs(offset.x) / sphereOffsetScale.x;
            scale.y = Mathf.Max(scale.y, scaleYMin);
            spTrans.localScale = scale;
        }
    }

    private void Update()
    {
        // if (Keyboard.current.spaceKey.wasPressedThisFrame) Restart();
    }

    private void Restart()
    {
        foreach (var sp in spheres) Destroy(sp);
        MakeCloud();
    }
}