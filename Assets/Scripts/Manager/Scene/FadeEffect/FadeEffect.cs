using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
[Serializable]
public abstract class FadeEffect{
    // 이 페이드 이펙트의 이름
    public abstract string FadeEffectName {
        get;
    }
    // 이 페이드 이펙트에 사용할 이미지 이름
    public abstract string FadeImageName {
        get;
    }
    public abstract void FadeOut(float duration, params Action[] callback);
    public abstract void FadeIn(float duration, params Action[] callback);
}