using UnityEngine;
using System.Collections;

public class GroundChek : MonoBehaviour {

    private player Player;

    void Start()
    {
        Player = gameObject.GetComponentInParent<player>();
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        Player.grounded = true;
    }
    void OnTriggerStay2D(Collider2D col)
    {
        Player.grounded = true;
    }
    void OnTriggerExit2D(Collider2D col)
    {
        Player.grounded = false;
    }
}
