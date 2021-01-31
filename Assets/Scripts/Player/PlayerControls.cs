using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerControls : MonoBehaviour
{
    public GameManager levelManager;
    public Joystick joystick;
    public Animator animator;
    public Rigidbody2D rigid;
    public BreathBar myBreathBar;
    public SpriteRenderer mySprite;
    public float swimSpeedHorizontally = 40f;
    public float impulseForce = 20f;
    float horizontalMove = 0f;
    public AudioSource reproducer;
    public AudioClip pickFragment, getDamaged;


    public Text Lifes;
    public int LifesnUmber;
    float InvisibilityTime;
    bool Damaged;
    public void Start()
    {
        Lifes.text = LifesnUmber.ToString();
        InvisibilityTime = 3.0f;
        myBreathBar.SetNewTarget(0.0f, 0.1f);
    }
    // Update is called once per frame
    void Update()
    {

        horizontalMove = joystick.Horizontal * swimSpeedHorizontally;
        //verticalMove = joystick.Vertical * swimSpeedVertically;
        if(Damaged)
        {
            InvisibilityTime -= Time.deltaTime;
            if(mySprite.enabled)
            {
                mySprite.enabled = false;
            }
            else
            {
                mySprite.enabled = true;
            }
            if(InvisibilityTime <= 0)
            {
                mySprite.enabled = true;
                Damaged = false;
                InvisibilityTime = 3.0f;
            }
        }
      
    }


    void FixedUpdate()
    {
        // Move our character
        rigid.velocity = new Vector2(horizontalMove * Time.fixedDeltaTime,rigid.velocity.y);
        if(rigid.velocity.x != 0)
        {
            animator.SetBool("moving", true);
            if(rigid.velocity.x > 0)
            {
                this.gameObject.GetComponent<SpriteRenderer>().flipY = false;
            }
            else
            {
                this.gameObject.GetComponent<SpriteRenderer>().flipY = true;
            }
        }
        else
        {
            animator.SetBool("moving", false);
        }
        //rigid.velocity = new Vector2(rigid.velocity.x, verticalMove * Time.fixedDeltaTime);
    }

   public void GiveImpulseTop()
    {
        animator.SetTrigger("Jump");
        rigid.AddForce(new Vector2(0,1) *impulseForce, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       

        if(collision.CompareTag("Memory"))
        {
            //Got the memory, play the animation and then leave something behind to mark it as taken
            levelManager.TakeMemory(collision.gameObject);
            collision.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            reproducer.PlayOneShot(pickFragment);
        }

        if(collision.CompareTag("BreathArea"))
        {
           
            myBreathBar.SetNewTarget(1.0f, 0.1f);
        }
        if(!Damaged)
        {
            if (collision.CompareTag("Enemies"))
            {
                //CAUSE DAMAGE;
                //AND ADD A INVICIBILITY TIMER\
                Damaged = true;
                reproducer.PlayOneShot(getDamaged);
                LifesnUmber -= 1;
                Lifes.text = LifesnUmber.ToString();
                if(LifesnUmber == 0)
                {
                    levelManager.LoseGame("YOu got hit to many time!~");
                }
            }
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
      
        if (collision.CompareTag("BreathArea"))
        {
            myBreathBar.SetNewTarget(0.0f, 0.1f);
        }
    }

}
