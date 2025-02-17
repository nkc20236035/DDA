using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReSetButton : MonoBehaviour
{
    public string targetTag = "Command"; // �폜�������I�u�W�F�N�g�̃^�O

    public void DestroyAllWithTag()
    {
        GameObject[] objects = GameObject.FindGameObjectsWithTag(targetTag); // �^�O�̂����I�u�W�F�N�g���擾

        foreach (GameObject obj in objects)
        {
            Destroy(obj); // �I�u�W�F�N�g���폜
        }
    }
}
