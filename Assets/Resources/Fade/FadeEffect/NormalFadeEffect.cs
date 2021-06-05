using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using DG.Tweening;
[Serializable]
public class NormalFadeEffect : FadeEffect {
    public override string FadeEffectName { get { return "NormalFadeEffect"; } }
    public override string FadeImageName { get { return "Normal"; } }
    public override void FadeOut(float duration, params Action[] callback) {
        Image CreateImage =  SceneUtilityManager.Instance.CreateFadeImage(FadeImageName,1,1, new Color(0, 0, 0, 0), duration);
        if (callback == null)
            CreateImage.DOFade(1, duration).SetUpdate(true);
        else
            CreateImage.DOFade(1, duration).SetUpdate(true).OnStart(() => { CreateImage.raycastTarget = false; }).
    OnComplete(() => { if (callback.Length > 0 && callback != null) { foreach (Action Callback in callback) Callback(); } else return; });
    }
    public override void FadeIn(float duration, params Action[] callback) {
        Image CreateImage = SceneUtilityManager.Instance.CreateFadeImage(FadeImageName,1,1, new Color(0, 0, 0, 1), duration);

        if (callback == null) {
            CreateImage.DOFade(0, duration).SetUpdate(true);
        } else
            CreateImage.DOFade(0, duration).SetUpdate(true).OnStart(() => { CreateImage.raycastTarget = false; }).
OnComplete(() => { if (callback.Length > 0 && callback != null) { foreach (Action Callback in callback) Callback(); } else return; });

    }
}
