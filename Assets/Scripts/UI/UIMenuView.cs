using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMenuView : UIView
{
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (PageStack.Count == 0) {
                Push("Menu");
                Time.timeScale = 0;
            } else
                Pop();
        }
    }
    public override void Pop() {
        base.Pop();
        if (PageStack.Count == 0)
            Time.timeScale = 1;
    }
    public void OnChangeSceneCilck(string sceneName) {
        SceneUtilityManager.Instance.FadeAndSceneChange(sceneName, "NormalFadeEffect", 2);
    }
}
