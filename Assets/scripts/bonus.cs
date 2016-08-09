using UnityEngine;
using System.Collections;

public class bonus : MonoBehaviour {
    public GameObject ob;
    public GameObject speed_anim;
    public player Player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        ob = col.transform.parent.gameObject;
       Player =  ob.GetComponent<player>();
        Player.bonus = true;
        Vector3 vec = new Vector2(0, 0);
        GameObject objec = (GameObject)Instantiate(speed_anim, (ob.transform.position + new Vector3(0,-0.74f, 0)), transform.rotation);
        objec.transform.parent = ob.transform;
        Invoke("send_bonus", 3);
        gameObject.active = false;
    }

    void send_bonus()
    {
        gameObject.active = true ;
    }
}
