using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using GameSave;

public class StageManager : MonoBehaviour
{
    private void Awake() {
        EventManager<GameEvent>.Instance.AddListener(GameEvent.StageRestart, this, PlayerDieEvent);
    }
    void PlayerDieEvent(GameEvent eventType, Component component, object param) {
        string nowSceneName = SceneManager.GetActiveScene().name;
        SO_Stage stageData = StageDataManager.Instance.GetStageData(nowSceneName);
        if (stageData != null) {
            SceneUtilityManager.Instance.FadeAndSceneChange(stageData.SceneName, stageData.FadeInEffectName, stageData.FadeInDuration);
        } else {
            Debug.LogWarning(nowSceneName + " 해당 씬은 스테이지 초기화 리스트에 없습니다.");
            SceneUtilityManager.Instance.FadeAndSceneChange(nowSceneName, "NormalFadeEffect", 2);
        }
    }
}
