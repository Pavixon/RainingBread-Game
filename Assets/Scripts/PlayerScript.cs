using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 5f;
    public SpriteRenderer sr;
    private Vector2 moveInput;
    private LogicScript logic;
    private bool isPlayerAlive = true;
    public AudioSource eatBreadSound;
    public AudioSource eatBadMeatSound;
    private int upgarde = 25;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    void Update()
    {
        if (logic.gameScreen.activeSelf)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) && isPlayerAlive == true)
            {
                sr.flipX = true;
            }
            else
            {
                sr.flipX = false;
            }

            if (logic.playerHP <= 0)
            {
                isPlayerAlive = false;
                Destroy(gameObject);
                logic.gameOver();
            }
            else
            {
                rb.velocity = moveInput * moveSpeed;
            }
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        if(logic.playerScore == upgarde)
        {
            SpeedUp();
            logic.LoseHP(-1);
            upgarde += 25;
        }
    }

    public void SpeedUp()
    {
        moveSpeed+=2.4f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "Player" && collision.gameObject.tag == "Bread")
        {
            eatBreadSound.Play();
            logic.AddScore(1);
            Destroy(collision.gameObject);
        }
        else if (gameObject.tag == "Player" && collision.gameObject.tag == "Enemy")
        {
            eatBadMeatSound.Play();
            logic.LoseHP(1);
            Destroy(collision.gameObject);
        }
    }
}
