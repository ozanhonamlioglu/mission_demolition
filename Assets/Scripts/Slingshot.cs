using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slingshot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    private GameObject _launchPoint;

    private void Awake()
    {
        // Find the launch point GameObject in the scene
        Transform launchPointTransform = GameObject.Find("LaunchPoint")?.transform;
        _launchPoint = launchPointTransform != null ? launchPointTransform.gameObject : null;
        _launchPoint?.SetActive(false);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _launchPoint?.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _launchPoint?.SetActive(false);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        
    }
}
