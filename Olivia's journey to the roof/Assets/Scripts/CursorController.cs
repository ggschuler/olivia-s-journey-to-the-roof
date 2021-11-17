using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    private Camera mainCamera;
    private PlayableSwitch switcher;
    private GameObject cursorAimPointer;
    private int offsetRotation = 90; 

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        switcher = GameObject.Find("Characters").GetComponent<PlayableSwitch>(); // get empty parent 'Characters', whose children is the player objects.
        cursorAimPointer = transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        CursorAim();
    }

    private void CursorAim()
    {
        Vector2 mouse = Input.mousePosition;
        Vector3 offset = mainCamera.ScreenToWorldPoint(mouse);

        float rotZ = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ - offsetRotation); // adjusts pivot point rotation depending on mouse position.

        if (Input.GetButtonDown("Fire1"))
        {
            Fire(offset);
        }
    }

    private void Fire(Vector3 offset)
    {
        RaycastHit2D aimHitInfo = Physics2D.Raycast(cursorAimPointer.transform.position, cursorAimPointer.transform.up);

        Debug.DrawRay(cursorAimPointer.transform.position, cursorAimPointer.transform.up + offset, Color.blue, 1f);

        if (aimHitInfo.transform.CompareTag("Player"))
        {
            Debug.Log("Player hit!");
            switcher.WhichIsActive();
            Debug.Log("Sucessful");
        }
    }

}