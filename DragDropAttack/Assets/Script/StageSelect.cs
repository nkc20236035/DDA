using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageSelect : MonoBehaviour
{
    public List<Button> stageButtons; // ステージ選択ボタンのリスト
    private int unlockedStages;

    void Start()
    {
        // 保存されたステージ進行状況を取得（初回プレイ時は1）
        unlockedStages = PlayerPrefs.GetInt("UnlockedStages", 1);

        // ボタンの有効/無効を設定
        for (int i = 0; i < stageButtons.Count; i++)
        {
            if (i < unlockedStages)
            {
                int stageIndex = i + 1; // ステージ番号
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
