using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[Serializable]
public class PlayerController : MonoBehaviour
{
    public Animator animator;

    private Rigidbody2D rb2d;
    private SpriteRenderer sprite;
    private CapsuleCollider2D capsuleCollider2d;
    public GameObject life1, life2, life3;

    public static bool isDead = false;
    public bool isGrounded = false;
    public Transform isGroundedChecker;
    public float checkerRadius;
    public LayerMask platform;

    public float speed;

    public Text score;
    public Text livesText;
    public Text winText;
 
    public static int scoreValue = 0;
    public int lives = 3;
    
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        capsuleCollider2d = GetComponent<CapsuleCollider2D>();
        SetText();
        winText.text = "";

        life1.gameObject.SetActive(true);
        life2.gameObject.SetActive(true);
        life3.gameObject.SetActive(true);

    }

    void Update()
    {
        switch (lives)
        {
            case 3:
                life1.gameObject.SetActive(true);
                life2.gameObject.SetActive(true);
                life3.gameObject.SetActive(true);
                break;
            case 2:
                life1.gameObject.SetActive(true);
                life2.gameObject.SetActive(true);
                life3.gameObject.SetActive(false);
                break;
            case 1:
                life1.gameObject.SetActive(true);
                life2.gameObject.SetActive(false);
                life3.gameObject.SetActive(false);
                break;
            case 0:
                life1.gameObject.SetActive(false);
                life2.gameObject.SetActive(false);
                life3.gameObject.SetActive(false);
                SetText();
                isDead = true;
                break;
        }
    }

    void FixedUpdate()
    {

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        animator.SetFloat("horizontal", Mathf.Abs(Input.GetAxis("Horizontal")));
        animator.SetFloat("vertical", (Input.GetAxis("Vertical")));
        animator.SetFloat("horizontalVelocity", Mathf.Abs(rb2d.velocity.x));
        animator.SetFloat("verticalVelocity", rb2d.velocity.y);
        animator.SetBool("jump", Input.GetKey(KeyCode.W));
        animator.SetBool("grounded", isGrounded);
        animator.SetBool("isDead", isDead);

        if (isDead != true)
        {
            rb2d.AddForce(new Vector2(moveHorizontal * speed, moveVertical * speed));

            if (moveHorizontal < 0)
            {
                sprite.flipX = true;
                capsuleCollider2d.offset = new Vector2(.2f, 0f);
            }
            else if (moveHorizontal > 0)
            {
                sprite.flipX = false;
                capsuleCollider2d.offset = new Vector2(-.2f, 0f);
            }
        }

        CheckIfGrounded();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            SetText();
            Destroy(collision.collider.gameObject);
            if (scoreValue == 4)
            {
                transform.position = new Vector2(100f, -2f);
                lives = 3;
            }
        }

        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.SetActive(false);
            lives--;
            SetText();
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (isGrounded && Input.GetKey(KeyCode.W))
        {
            rb2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
        }
    }
    
    void CheckIfGrounded()
    {
        Collider2D collider = Physics2D.OverlapCircle(isGroundedChecker.position, checkerRadius, platform);

        if (collider != null)
        {
            isGrounded = true;
        } 
        else
        {
            isGrounded = false;
        }
    }

    void SetText()
    {
        score.text = "Score: " + scoreValue.ToString();

        if (scoreValue >= 8)
        {
            winText.text = "You Win! Game created by Hiram Sun!";
            Time.timeScale = 0;
        }

        if (lives <= 0)
        {
            winText.text = "Game Over! Game created by Hiram Sun!";
            isDead = true;
        }
    }
}
