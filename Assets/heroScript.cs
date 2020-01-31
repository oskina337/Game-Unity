using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class heroScript : MonoBehaviour
{
    public float speed = 10f;
  
    bool facingRight = true;

    bool grounded = false; 
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;


    float posX, posY;
    public float life;
    public float score = 0;
    Rigidbody2D rig;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        posX = rig.position.x;
        posY = rig.position.y;
        life = 5;
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);// проверка стоит ли спрайт на земле

        float move;
        move = Input.GetAxis("Horizontal");
        rig.velocity = new Vector2(move * speed, rig.velocity.y); //движение вправо и влево

        if (Input.GetKeyDown(KeyCode.UpArrow) && grounded) //прыжок
        {
          rig.AddForce(new Vector2(0, 700f));  
        } 

        if ((move < 0) && facingRight)
            Flip();
        else if ((move > 0) && !facingRight)
            Flip();     
    }
    void Flip() // метод поварачивает спрайт в направлении движения
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    void OnTriggerEnter2D(Collider2D col)// счетчик монет
    {
        if (col.GetComponent<PolygonCollider2D>().tag == "mony")
        {
            Destroy(col.gameObject);
            score++;
        }
    }

    void OnCollisionEnter2D(Collision2D col) 
    {
       if(col.gameObject.tag == "monstr")// монстр
       {
           life = life - 1;
           if (life == 0)
            {
                rig.position = new Vector2(posX,posY);
                life = 5;
            }
       } 
       if (col.gameObject.tag == "cliff") //выход за карту 
       {
           rig.position = new Vector2(posX,posY);
           life = 5;
       }
       if (col.gameObject.tag == "friend")
       {
           Application.LoadLevel(Application.loadedLevel + 1);
       }
    }

    

    void OnGUI()
    {
        GUI.Box (new Rect(0, 0, 100, 50), "Score = " + score);
        GUI.Box (new Rect(100, 0, 100, 50), "Life = " + life);
    }
}
