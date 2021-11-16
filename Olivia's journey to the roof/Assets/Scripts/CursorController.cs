using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    private Camera camera;
    private PlayableSwitch switcher;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
        switcher = GameObject.Find("Playable").GetComponent<PlayableSwitch>();
    }

    // Update is called once per frame
    void Update()
    {
        Mouse();
    }

    private void Mouse()
    {
        Vector3 mouse = Input.mousePosition;
        Vector3 offset = camera.ScreenToWorldPoint(mouse);
        
        float rotZ = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ - 90);
        if (Input.GetButtonDown("Fire1"))
        {
            RaycastHit2D infoHit = Physics2D.Raycast(transform.GetChild(0).position,transform.GetChild(0).up);
            Debug.DrawRay(transform.GetChild(0).position, transform.GetChild(0).up + offset, Color.blue,1f);
            Debug.Log("Fire!");
            if (infoHit.transform.CompareTag("Player"))
            {
                Debug.Log("Player hit!");
                switcher.SwitchPlayer();
                Debug.Log("Sucessful");
            }
        }
    }

    
}