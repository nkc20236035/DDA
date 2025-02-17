using UnityEngine;

public class SimplePlayerController : MonoBehaviour
{
    public int MaxHP = 300;

    private Rigidbody2D rb;
    private Animator anim;
    Vector3 movement;
    private int direction = 1;
    private bool alive = true;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
    }

    public void getDamage(int damage)
    {
        MaxHP -= damage;
        if (MaxHP > 0)
        {
            Hurt();
            Debug.Log(MaxHP);
        }
        else
        {
            Die();
        }
    }

    public void Attack()
    {
        if (alive)
        {
            anim.SetTrigger("attack");
        }
    }
    void Hurt()
    {
        if (alive)
        {
            anim.SetTrigger("hurt");
            if (direction == 1)
                rb.AddForce(new Vector2(-5f, 1f), ForceMode2D.Impulse);
            else
                rb.AddForce(new Vector2(5f, 1f), ForceMode2D.Impulse);
        }
    }
    void Die()
    {
        if (alive)
        {
            anim.SetTrigger("die");
            alive = false;
        }
    }
}
