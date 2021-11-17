using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableSwitch : MonoBehaviour
{
    GameObject player1;
    GameObject player2;
    GameObject playerCenter;
    PlayerController control1;
    PlayerController control2;
    [SerializeField] private float delayTime = .3f;
    [SerializeField] private int whichIsActive;

    // Start is called before the first frame update
    void Start()
    {
        player1 = transform.GetChild(0).gameObject; // gets player 1;
        player2 = transform.GetChild(1).gameObject; // gets player 2;

        playerCenter = player1.transform.GetChild(0).gameObject; // gets pivot point, which always start with player 1;

        control1 = player1.GetComponent<PlayerController>(); // gets controller script reference;
        control2 = player2.GetComponent<PlayerController>();

        whichIsActive = 1;
    }

    public void WhichIsActive()
    {
        switch (whichIsActive)
        {
            case 1:
                whichIsActive = 2;
                Debug.Log("Activate player 2");
                StartCoroutine(Switch(control1, control2));
                break;
            case 2:
                whichIsActive = 1;
                Debug.Log("Activate player 1");
                StartCoroutine(Switch(control2, control1));
                break;
            default:
                break;
        }
    }

    private IEnumerator Switch(PlayerController toDisable, PlayerController toAble) // receives which controller script to disable and able, and executes the change.
    {
        yield return new WaitForSeconds(delayTime);
        toDisable.enabled = false;
        toAble.enabled = true;
        playerCenter.transform.SetParent(toAble.transform);
        playerCenter.transform.localPosition = Vector2.zero;
        
    }
}
