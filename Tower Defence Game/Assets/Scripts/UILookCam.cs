using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UILookCam : MonoBehaviour
{
    [SerializeField] private Camera  cam;

    private void Awake() {
        if (cam == null)
        {
            cam = Camera.main;
        }
    }
    private void LateUpdate()
    {
        transform.LookAt(transform.position + cam.transform.rotation * Vector3.forward, cam.transform.rotation * Vector3.up);  
    }
}
