using UnityEngine;

public class RigidbodySleep : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        var rb = GetComponent<Rigidbody>();
        if (rb != null) rb.Sleep();
    }

    // Update is called once per frame
    private void Update()
    {
    }
}