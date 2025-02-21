using Unity.VisualScripting;
using UnityEditor.EditorTools;
using UnityEngine;
using UnityEngine.UI;

public class SimplePlayerController : MonoBehaviour
{
    public int MaxHP = 150;
    public int HP;
    public Slider HPSlider;
    public Image LoseSprite;
    public AudioClip HitDamage;
    public AudioClip Attackvoice;
    public AudioClip GameOverAudio;
    public AudioClip ClearAudio;
    public GameObject enemy;
    public Text timeText;

    private Rigidbody2D rb;
    private Animator anim;
    Vector3 movement;
    private int direction = 1;
    private bool alive = true;
    private float timer = 0;
    private bool isfading = false;
    private float alpha = 0f;
    private float fedeSpeed = 1f;
    private float Attacktime = 0;

    AudioSource AudSou;
    EnemyGenerator enemygene;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        HP = MaxHP;
        HPSlider.maxValue = MaxHP;
        HPSlider.value = HP;
        Color color = LoseSprite.color;
        color.a = 0f;
        LoseSprite.color = color;
        AudSou = GetComponent<AudioSource>();
        enemygene = enemy.GetComponent<EnemyGenerator>();
        timeText.text = "0.0";
    }

    void Update()
    {
        if(HP > MaxHP)
        {
            HP = MaxHP;
        }

        HPSlider.value = HP;

        if(timer > 0)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                isfading = true;
            }
        }

        if(Attacktime > 0)
        {
            Attacktime -= Time.deltaTime;
            timeText.text = Attacktime.ToString("0.0");
            if(Attacktime <= 0)
            {
                Attacktime = 0;
                timeText.text = "0.0";
            }
        }


        if(isfading)
        {
            alpha += fedeSpeed * Time.deltaTime;
            alpha = Mathf.Clamp01(alpha);
            Color color1 = LoseSprite.color;
            color1.a = alpha;
            LoseSprite.color = color1;
            AudSou.clip = GameOverAudio;
            AudSou.Play();
            if (alpha >= 1f)
            {
                isfading = false;
            }

        }
    }

    public void getDamage(int damage)
    {
        HP -= damage;
        
        if (HP > 0)
        {
            Hurt();
            Debug.Log(HP);
        }
        else
        {
            Die();
        }
    }

    public void getHeal(int heal)
    {
        HP += heal;
        Debug.Log(HP);
    }

    public void Attack()
    {
        if (alive)
        {
            anim.SetTrigger("attack");
            AudSou.clip = Attackvoice;
            AudSou.Play();
        }
    }
    void Hurt()
    {
        if (alive)
        {
            anim.SetTrigger("hurt");
            AudSou.clip = HitDamage;
            AudSou.Play();
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
            timer = 1f;
        }
    }

    public void won()
    {
        AudSou.clip = ClearAudio;
        AudSou.Play();
    }

    public void AttackTimer(float t)
    {
        Attacktime = t;
    }

}
