using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float moveSpeed = 6f;
    public SpriteRenderer sr;
    private Vector2 moveInput;
    private LogicScript logic;
    private bool isPlayerAlive = true;
    public AudioSource eatBreadSound;
    public AudioSource eatBadMeatSound;
    private int upgarde = 25;
    private int blinkCout = 5;
    private bool isBlinking = false;

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
                logic.GameOver();
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
            logic.HpMenager(1);
            upgarde += 25;
        }
    }

    private IEnumerator Blink()
    {
        isBlinking = true;
        for (int i = 0; i < blinkCout; i++)
        {
            sr.enabled = false;
            yield return new WaitForSeconds(0.2f);
            sr.enabled = true;
            yield return new WaitForSeconds(0.2f);
        }
        isBlinking = false;
        
    }

    public void SpeedUp()
    {
        moveSpeed+=2.4f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "Player" && collision.gameObject.tag == "Bread")
        {
            sr.transform.localScale *= 1.01f;
            eatBreadSound.Play();
            logic.AddScore(1);
            Destroy(collision.gameObject);
        }
        else if(gameObject.tag == "Player" && collision.gameObject.tag == "HP")
        {
            logic.HpMenager(1);
            Destroy(collision.gameObject);
        }
        else if (gameObject.tag == "Player" && collision.gameObject.tag == "Enemy" && isBlinking == false)
        {
            StartCoroutine(Blink());
            eatBadMeatSound.Play();
            logic.HpMenager(-1);
            Destroy(collision.gameObject);
        }
    }
}
