using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StageButton : MonoBehaviour
{
    public int stageNumber; // �{�^�����ƂɃX�e�[�W�ԍ���ݒ�

    void Start()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnStageSelected);
        }

        // �X�e�[�W�������i�v���C���[�i�s�󋵂��m�F�j
        int unlockedStages = PlayerPrefs.GetInt("UnlockedStages", 1);
        button.interactable = (stageNumber <= unlockedStages);
    }

    public void OnStageSelected()
    {
        SceneManager.LoadScene("BattleScene" + stageNumber);
    }
}
