using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageButton : MonoBehaviour
{
    public int stageNumber; // ボタンごとにステージ番号を設定

    void Start()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnStageSelected);
        }

        // ステージ解放判定（プレイヤー進行状況を確認）
        int unlockedStages = PlayerPrefs.GetInt("UnlockedStages", 1);
        button.interactable = (stageNumber <= unlockedStages);
    }

    public void OnStageSelected()
    {
        SceneManager.LoadScene("BattleScene" + stageNumber);
    }
}
