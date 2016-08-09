using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
public class player : NetworkBehaviour {
    private float maxSpeed = 8;
    System.Net.IPHostEntry host;
    public float speed = 50f;
    public float jumpPower = 150f;
    public bool grounded;
    public bool bonus;
    public bool Rd = true;
    public int speds;


    private Rigidbody2D rb2d;
   // [SerializeField]

    //private Transform[] groundPoints;
    // Use this for initialization
    private Animator anim;
    public bool bonusSpeed;
    private float movementSpeed = 8;
    private float jumpPowers = 15;
    private float gravity = 40;
    public int joystickNumber;
    private Vector3 movementVector;
    public int Andro = 400;
    private bool back = false;

    void Start () {

        rb2d = gameObject.GetComponent<Rigidbody2D>();
        anim = gameObject.GetComponent<Animator>();
        
	}
    public override void OnStartServer()
    {
        //GameObject player = (GameObject)Instantiate(this);
        //NetworkServer.Spawn(player);
    }
    // Update is called once per frame

    void Update () {

        if (!isLocalPlayer)
        {
          return;
        }
        if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer)
        {
            Andro = 400;
            //rb2d = GetComponent<Rigidbody2D>();
            anim.SetBool("Grounded", grounded);

            Vector3 dir = Vector3.zero;
            dir.x = -Input.acceleration.x;
            //dir.z = Input.acceleration.x;

            if (bonus == true)
            {
                // rb2d.velocity = new Vector2(200, rb2d.velocity.y);
                if (Rd)
                {
                    speds = Random.Range(0, 2);
                    Rd = false;
                }
                if (speds == 0)
                {
                    Andro = 800;

                }
                if (speds == 1)
                {
                    Andro = 150;
                }
                //  maxSpeed = 8;
                Invoke("get_bonus", 3);

            }
            if (dir.x > -0.2 && dir.x < 0.2)
            {

                dir.Normalize();
                anim.SetFloat("Speed", 0);

                dir *= Time.deltaTime;
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            }

            if (dir.x < -0.2)
            {

                dir.Normalize();
                anim.SetFloat("Speed", Mathf.Abs(dir.x));
                anim.SetBool("Back", false);

                dir *= Time.deltaTime;
                rb2d.velocity = new Vector2(Mathf.Abs(dir.x) * Andro, rb2d.velocity.y);
            }


            if (dir.x > 0.2)
            {

                dir.Normalize();
                anim.SetFloat("Speed", -dir.x);
                anim.SetBool("Back", true);
                dir *= Time.deltaTime;
                rb2d.velocity = new Vector2(-dir.x * Andro, rb2d.velocity.y);
            }
            if (Input.GetTouch(0).phase == TouchPhase.Began && grounded)
            {
                rb2d.AddForce(Vector2.up * jumpPower);
            }

        }
        maxSpeed = 8;
        anim.SetBool("Grounded",grounded);
        anim.SetBool("Back", back);
        anim.SetFloat("Speed",Input.GetAxis("Horizontal"));
        if(Input.GetAxis("Horizontal") < -0.1f)
        {
            back = true;
            // transform.localScale = new Vector3(-1, 1, 1);
            // GetComponent<SpriteRenderer>().flipX = true;

        }
        if (Input.GetAxis("Horizontal") > 0.1f)
        {
            back = false;
            //  transform.localScale = new Vector3(1, 1, 1);
            // GetComponent<SpriteRenderer>().flipX = false;
        }
        if (Input.GetButtonDown("Jump") && grounded)
        {
            rb2d.AddForce(Vector2.up * jumpPower);
        }


        if (bonus == true)
        {
            // rb2d.velocity = new Vector2(200, rb2d.velocity.y);
            if (Rd)
            {
                speds = Random.Range(0, 2);
                Rd = false;
            }
            if(speds == 0)
            {
                maxSpeed = 15;

            }
            if(speds == 1)
            {
                maxSpeed = 2;
            }
            //  maxSpeed = 8;
            Invoke("get_bonus", 3);

        }
    }
    void get_bonus()
    {
        bonus = false;
        Rd = true;
        // maxSpeed = 8;
        //Debug.Log("ddd");
    }

    public void button()
    {
        rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
    }
    void FixedUpdate(){
        if (!isLocalPlayer)
        {
            return;
        }
    
        float h = Input.GetAxis("Horizontal");

            rb2d.AddForce((Vector2.right * speed) * h);

            if (rb2d.velocity.x > maxSpeed)
            {
               rb2d.velocity = new Vector2(maxSpeed, rb2d.velocity.y);
            }
            if (rb2d.velocity.x < -maxSpeed)
            {
               rb2d.velocity = new Vector2(-maxSpeed, rb2d.velocity.y);
            }
        
    }

}
