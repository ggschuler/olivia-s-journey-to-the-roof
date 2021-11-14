using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    private Camera camera;
    
    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Mouse();
    }

    private void Mouse()
    {
        Vector3 mouse = Input.mousePosition;
        Vector3 offset = camera.ScreenToWorldPoint(mouse - transform.position);
        offset.Normalize();
        float rotZ = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ - 90);
    }
}