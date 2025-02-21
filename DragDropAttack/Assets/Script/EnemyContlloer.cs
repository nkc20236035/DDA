using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EnemyContlloer : MonoBehaviour
{
    public EnemyData enemyData;
    public Text timertext;
    public AudioClip HitDamage;
    public Slider HPSlider;

    private int currentHP;
    private int currentAttack;
    private float currentAttackTime;
    private int MaxHp;
    EnemyGenerator EnemyGene;
    private int Damage = 0;
    private float Timer = 0f;
    SimplePlayerController playercontlloer;
    StageManager stagemanager;
    AudioSource aud;

    void Start()
    {
        EnemyGene = GameObject.Find("EnemyGene").GetComponent<EnemyGenerator>();
        playercontlloer = GameObject.Find("Wizard").GetComponent<SimplePlayerController>();
        stagemanager = GameObject.Find ("StageManager").GetComponent<StageManager>();
        aud = GetComponent<AudioSource>();

        if (enemyData != null)
        {
            currentHP = enemyData.maxHP;
            currentAttack = enemyData.attackPower;
            currentAttackTime = enemyData.attackTime;
        }
        else
        {
            Debug.LogError("EnemyDataなし");
        }
        
        HPSlider.maxValue = currentHP;
        HPSlider.value = currentHP;

    }

    
    void Update()
    {
        if (stagemanager.isPlaying)
        {
            if (Timer > 0)
            {
                Timer -= Time.deltaTime;

                if (Timer <= 0 && Damage > 0)
                {
                    TakeDamage(Damage);
                    Damage = 0;
                }
            }

            if (currentAttackTime > 0)
            {
                timertext.text = currentAttackTime.ToString("0");
                currentAttackTime -= Time.deltaTime;
                if (currentAttackTime < 0)
                {
                    playercontlloer.getDamage(currentAttack);
                    aud.clip = HitDamage;
                    aud.Play();
                    currentAttackTime = enemyData.attackTime;
                }
            }

        }

    }

    public void SetAttack(int damage,float time)
    {
        Damage = damage;
        Timer = time;
    }

    public void TakeDamage(int damage)
    {
        
        currentHP -= damage;
        playercontlloer.Attack();
        aud.clip = HitDamage;
        aud.Play();
        Debug.Log($"{enemyData.enemyName} は {damage} ダメージを受けた");
        HPSlider.value = currentHP;
        if (currentHP <= 0)
        {
            Die();
        }

        
    }

    void Die()
    {
        Debug.Log("死亡");
        EnemyGene.OnEnemyDestroyed(); // EnemyGeneratorのOnEnemyDestroyedクラスを呼び出す
        Destroy(gameObject);
    }


    
}
