using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSetButton : MonoBehaviour
{
    public CommandContlloer commandC;
    public PlayerCommandContlloer PcommandC;

    public void ClickButton()
    {
        commandC.commandReset();
        PcommandC.CommandReset();
    }
}
