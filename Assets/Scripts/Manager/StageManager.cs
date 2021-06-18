using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using GameSave;

public class StageManager : MonoBehaviour
{
    private void Awake() {
        EventManager<PlayerEvent>.Instance.AddListener(PlayerEvent.Die, this, PlayerDieEvent);
        EventManager<GameEvent>.Instance.AddListener(GameEvent.StageRestart, this, Restart);
    }
    void PlayerDieEvent(PlayerEvent eventType, Component component, object param) {
        string nowSceneName = SceneManager.GetActiveScene().name;
        SO_Stage stageData = StageDataManager.Instance.GetStageData(nowSceneName);
        SceneUtilityManager.Instance.FadeOut("NormalFadeEffect", 5);
        if (stageData != null) {
            SceneUtilityManager.Instance.FadeAndSceneChange(stageData.SceneName, stageData.FadeInEffectName, stageData.FadeInDuration);
        } else {
            Debug.LogWarning(nowSceneName + " 해당 씬은 스테이지 초기화 리스트에 없습니다.");
            SceneUtilityManager.Instance.FadeAndSceneChange(nowSceneName, "NormalFadeEffect", 2);
        }
    }
    void Restart(GameEvent eventType, Component component, object param) {
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
