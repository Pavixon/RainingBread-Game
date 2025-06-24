using UnityEngine;

public class ProjectileScript : MonoBehaviour
{
    public Rigidbody2D rb;
    public float fallSpeed;
    private LogicScript logic;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
    }

    void Update()
    {
        rb.velocity = Vector2.down * (fallSpeed+(logic.playerScore/20));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (gameObject.tag == "Bread" && collision.gameObject.tag == "DeadZone")
        {
            logic.HpMenager(-1);
        }
        Destroy(gameObject);
    }
}
