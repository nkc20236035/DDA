using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    public List<Button> stageButtons; // �X�e�[�W�I���{�^���̃��X�g
    private int unlockedStages;

    void Start()
    {
        // �ۑ����ꂽ�X�e�[�W�i�s�󋵂��擾�i����v���C����1�j
        unlockedStages = PlayerPrefs.GetInt("UnlockedStages", 1);

        // �{�^���̗L��/������ݒ�
        for (int i = 0; i < stageButtons.Count; i++)
        {
            if (i < unlockedStages)
            {
                int stageIndex = i + 1; // �X�e�[�W�ԍ�
                stageButtons[i].interactable = true;
                stageButtons[i].onClick.AddListener(() => LoadStage(stageIndex));
            }
            else
            {
                stageButtons[i].interactable = false;
            }
        }
    }

    void LoadStage(int stageNumber)
    {
        SceneManager.LoadScene("BattleScene" + stageNumber);
    }

    public void UnlockNextStage()
    {
        if (unlockedStages < stageButtons.Count)
        {
            unlockedStages++;
            PlayerPrefs.SetInt("UnlockedStages", unlockedStages);
            PlayerPrefs.Save();
        }
    }
}
