using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using GameSave;
using System.Threading;

public class StageDataManager : DontDestroySingletonBehaviour<StageDataManager> {
    [SerializeField] List<SO_Stage> StageDataList = new List<SO_Stage>();
   
    bool isLoad = true;
    public override void Awake() {
        base.Awake();
        Init();
        if (isLoad)
            LoadStageData();
    }
    public void Init() {
        for (int i = 0; i < StageDataList.Count; i++) {
            StageDataList[i].Init();
        }
    }
    public SO_Stage GetStageData(string StageName) {
        for (int i = 0; i < StageDataList.Count; i++) {
            if (StageName == StageDataList[i].SceneName) {
                return StageDataList[i];
            }
        }
        Debug.LogWarning(StageName + "라는 스테이지는 없습니다.");
        return null;
    }
    private void Start() {
        bool checkStage = false;
        for (int i = 0; i < StageDataList.Count; i++) {
            if (SceneManager.GetActiveScene().name == StageDataList[i].SceneName) {
                SceneUtilityManager.Instance.FadeIn(StageDataList[i].FadeInEffectName, StageDataList[i].FadeInDuration);
                checkStage = true;
                break;
            }
        }
        if (checkStage == false)
            SceneUtilityManager.Instance.FadeIn("NormalFadeEffect", 2);
        EventManager<GameEvent>.Instance.AddListener(GameEvent.ChangeStage, this, StageChangeEvent);
    }

    void StageChangeEvent(GameEvent eventType, Component component, object param) {
        string SceneName = (string)param;
        for(int i = 0; i < StageDataList.Count; i++) {
            if(SceneName == StageDataList[i].SceneName) {
                if(SceneUtilityManager.Instance != null)
                SceneUtilityManager.Instance.FadeIn(StageDataList[i].FadeInEffectName, StageDataList[i].FadeInDuration);
                StageDataList[i].IsJoin = true;

                if (StageDataList[i].BGM != null)
                    SoundManager.Instance.PlayBGM(StageDataList[i].BGM, 1);
                return;
            }
        }
        SceneUtilityManager.Instance.FadeIn("NormalFadeEffect", 2);
        Debug.LogWarning(SceneName + " 해당 씬은 스테이지 데이터 리스트에 없습니다.");
    }
    public void ChangeStage(string stageName) {
        for (int i = 0; i < StageDataList.Count; i++) {
            if (stageName == StageDataList[i].SceneName) {
                SceneUtilityManager.Instance.FadeAndSceneChange(stageName, StageDataList[i].FadeInEffectName, StageDataList[i].FadeInDuration);
                return;
            }
        }
        SceneUtilityManager.Instance.FadeAndSceneChange(stageName, "NormalFadeEffect", 2);
        Debug.LogWarning(stageName + " 해당 씬은 스테이지 데이터 리스트에 없습니다.");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode level) {
        EventManager<GameEvent>.Instance.PostEvent(GameEvent.ChangeStage, this, scene.name);
    }
    public void SaveStageData() {
        SaveData stageSaveData = new SaveData();
        if (stageSaveData != null) {
            foreach (SO_Stage stageData in StageDataList) {
                int isJoin = 0;
                if (stageData.IsJoin)
                    isJoin = 1;
                stageSaveData.AddData(stageData.SceneName, new SaveData(isJoin));
            }
            SaveManager.SaveSerailizeData("Stage", "StagesData", stageSaveData);
        }
    }
    public void LoadStageData() {
        SaveData stageSaveData = SaveManager.LoadDeSerailizedData("Stage", "StagesData");
        if (stageSaveData != null) {
            foreach (SO_Stage stageData in StageDataList) {
                SaveData StageLoadData = stageSaveData.GetData(stageData.SceneName);
                if (StageLoadData != null) {
                    if (StageLoadData.GetInt() == 1) {
                        stageData.IsJoin = true;
                    }
                    Debug.Log(stageData.SceneName + "  " + "isJoin : " + stageData.IsJoin);
                }
            }
        }
        isLoad = false;
    }
    private void OnEnable() {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    private void OnApplicationQuit() {
        SaveStageData();
    }
}

