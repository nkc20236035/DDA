using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    private float timer = 0;
    public GameObject EnemyGene;
    public GameObject CommandGene;
    public bool Commandflag;
    public bool isPlaying;
    //public Text text;
    public Image image1;
    public Image image2;
    private bool isFading = true;
    private float alpha = 0f;
    private float fedeSpeed = 1.0f;
    private float commandTime;
    SimplePlayerController playerController;


    void Start()
    {
        //text.text = "";
        Color color1 = image1.color;
        color1.a = 0f;
        image1.color = color1;

        Color color2 = image2.color;
        color2.a = 0f;
        image2.color = color2;
        Commandflag = true;
        commandTime = 0;
        isPlaying = true;
        playerController = GameObject.Find("Wizard").GetComponent<SimplePlayerController>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        if(timer > 4)
        {
            Color color = image2.color;
            color.a = 0f;
            image2.color = color;
            EnemyGene.SetActive(true);
            CommandGene.SetActive(true);
        }
        else if(timer >= 3)
        {
            Color color1 = image1.color;
            color1.a = 0f;
            image1.color = color1;
            Color color2 = image2.color;
            color2.a = 1f;
            image2.color = color2;
            //text.text = "スタート";
        }
        else if(timer >= 1)
        {
            if(isFading)
            {
                alpha += fedeSpeed * Time.deltaTime;
                alpha = Mathf.Clamp01(alpha);
                Color color1 = image1.color;
                color1.a = alpha;
                image1.color = color1;
                if(alpha >= 1f)
                {
                    isFading = false;
                }
            }
            //text.text = "バトル";
        }

        if (commandTime > 0)
        {
            commandTime -= Time.deltaTime;

            if (commandTime < 0)
            {
                Commandflag = true;
            }
        }

        if(playerController.HP <= 0)
        {
            isPlaying = false;
        }

    }
    public void getTime(float t)
    {
        commandTime = t;
    }
}
