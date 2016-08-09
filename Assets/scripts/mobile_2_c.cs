using UnityEngine;
using System.Collections;

public class mobile_2_c : MonoBehaviour {

    //public float menuPosY = Screen.height / 2 - 80 / 2;
    public Rect mainMenu = new Rect(3000, 350, 1000, 115);
    public int sizeButtonX = 200;
    public int sizeButtonY = 200;
    private Rigidbody2D rb2d;
    public float speed = 300;
    public float jumpPower = 150f;
    private Animator anim;
    public bool grounded;



    void OnGUI() {

        GUI.BeginGroup(mainMenu, "");

        if (GUI.RepeatButton(new Rect(0, 20, sizeButtonX, sizeButtonY), "back"))
        {
            rb2d = GetComponent< Rigidbody2D >();
            rb2d.velocity = new Vector2(-8, rb2d.velocity.y);
        }
        if (GUI.RepeatButton(new Rect(750, 20, sizeButtonX, sizeButtonY), "forwad"))
        {
            rb2d = GetComponent< Rigidbody2D > ();
            rb2d.velocity = new Vector2(8, rb2d.velocity.y);
        }
        GUI.EndGroup();
    }
    // Use this for initialization
    void Start () {
        anim = gameObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        rb2d = GetComponent<Rigidbody2D>();
        anim.SetBool("Grounded", grounded);
        Debug.Log("#debug" + grounded);

        anim.SetFloat("Speed", Input.GetAxis("Horizontal"));
        Vector3 dir = Vector3.zero;
        dir.x = -Input.acceleration.x;
        //dir.z = Input.acceleration.x;

        Debug.Log(dir.sqrMagnitude);
        if (dir.x > -0.2 && dir.x < 0.2)
        {
           // Debug.Log("#debug" + "Stop");

            dir.Normalize();

            dir *= Time.deltaTime;
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }

        if (dir.x < -0.2)
        {
            //Debug.Log("#debug" + dir.x);

            dir.Normalize();

            dir *= Time.deltaTime;
            rb2d.velocity = new Vector2(Mathf.Abs(dir.x) * speed, rb2d.velocity.y);
        }


        if (dir.x > 0.2)
        {
            //Debug.Log("#debug" + "> 0.2");

            dir.Normalize();

            dir *= Time.deltaTime;
            rb2d.velocity = new Vector2(-dir.x * speed, rb2d.velocity.y);
        }
        if (Input.GetTouch(0).phase == TouchPhase.Began && grounded)
        {
            rb2d.AddForce(Vector2.up * jumpPower);
            Debug.Log("#debug" + "Touch");
        }

        //transform.Translate(dir * speed);

    }
}
