using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public SpriteRenderer sr;
    public AudioSource eatBreadSound;
    public AudioSource eatBadMeatSound;
    [SerializeField]
    private float moveSpeed = 6f;
    private Vector2 moveInput;
    private LogicScript logic;
    private bool isPlayerAlive = true;
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
        Vector2 input = context.ReadValue<Vector2>();
        moveInput = new Vector2(input.x, 0f);
        if(logic.playerScore == upgarde)
        {
            SpeedUp();
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
        moveSpeed+=2.5f;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag != "Player") return;

        string tag = collision.gameObject.tag;

        switch (tag)
        {
            case "Bread":
                if (logic.playerScore < 50)
                {
                    sr.transform.localScale *= 1.02f;
                }
                eatBreadSound.Play();
                logic.AddScore(1);
                Destroy(collision.gameObject);
                break;

            case "HP":
                if (logic.playerHP < 5)
                {
                    logic.HpMenager(1);
                }
                Destroy(collision.gameObject);
                break;

            case "Enemy":
                if (!isBlinking)
                {
                    StartCoroutine(Blink());
                    eatBadMeatSound.Play();
                    logic.HpMenager(-1);
                    Destroy(collision.gameObject);
                }
                break;
        }
    }
}
