using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

[Serializable]
public class StageButton {
    public Button button;
    public string StageName;
}
public class UIStageRoom : MonoBehaviour
{
    [SerializeField] List<StageButton> StageButtonList = new List<StageButton>();
    private void Start() {
        foreach (StageButton stageButton in StageButtonList) {
            SO_Stage stageData = StageDataManager.Instance.GetStageData(stageButton.StageName);
            if (stageData != null) {
                if (!stageData.IsJoin) {
                    ColorBlock cb = stageButton.button.colors;
                    cb.normalColor = Color.gray;
                    cb.selectedColor = Color.gray;
                    stageButton.button.colors = cb;
                } else
                    stageButton.button.onClick.AddListener(() => { OnStageButtonClick(stageData.SceneName); });
            } else {
                stageButton.button.onClick.AddListener(() => { OnStageButtonClick(stageButton.StageName); });               
                ColorBlock cb = stageButton.button.colors;
                cb.normalColor = Color.black;
                stageButton.button.colors = cb;
            }
        }
    }
    void OnStageButtonClick(string stageName) {
        SceneUtilityManager.Instance.FadeAndSceneChange(stageName, "NormalFadeEffect", 2);
    }
    public void ExitGame() {
        Debug.Log("게임 종료");
        Application.Quit();
    }
}
