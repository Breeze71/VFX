using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnifyingObject : MonoBehaviour
{
    private const string Object_Screen_Position = "_ObjScreenPos";

    private Renderer _renderer;
    private Camera cam;

    private void Start() 
    {
        _renderer = GetComponent<Renderer>();
        cam = Camera.main;    
    }

    private void Update() 
    {
        Vector3 screenPoint = cam.WorldToScreenPoint(transform.position);

        screenPoint.x = screenPoint.x / Screen.width;
        screenPoint.y = screenPoint.y / Screen.height;

        _renderer.material.SetVector(Object_Screen_Position, screenPoint);            
    }
}
