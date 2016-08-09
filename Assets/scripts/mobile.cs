using UnityEngine;
using System.Collections;

public class mobile : MonoBehaviour {

    public GameObject perso;
    private Rigidbody2D rb2d;
    public int maxSpeed = 8;

    public void button()
    {
        Debug.Log("Je suis le boutton");
        perso.transform.position = new Vector3(0,100,0);
        //rb2d = perso.GetComponent<Rigidbody2D>();
        //rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
