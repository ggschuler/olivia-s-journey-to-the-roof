using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableSwitch : MonoBehaviour
{

    GameObject player1;
    GameObject player2;
    PlayerController control1;
    PlayerController control2;
    private int whichIsActive;

    // Start is called before the first frame update
    void Start()
    {
        player1 = transform.GetChild(0).gameObject;
        player2 = transform.GetChild(1).gameObject;
        whichIsActive = 1;
        control1 = player1.GetComponent<PlayerController>();
        control2 = player2.GetComponent<PlayerController>();
    }

    public void SwitchPlayer()
    {
        switch (whichIsActive)
        {
            case 1:
                whichIsActive = 2;
                Debug.Log("Activate player 2");
                control1.enabled = false;
                control2.enabled = true;
                break;
            case 2:
                whichIsActive = 1;
                Debug.Log("Activate player 1");
                control1.enabled = true;
                control2.enabled = false;
                break;
            default:
                Debug.Log("Fuck!");
                break;
        }
    }
}
