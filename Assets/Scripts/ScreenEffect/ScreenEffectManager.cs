using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenEffectManager : Singleton<ScreenEffectManager>
{
    List<ScreenEffect> OnScreenEffectList = new List<ScreenEffect>();
    Dictionary<string, ScreenEffect> ScreenEffectDic = new Dictionary<string, ScreenEffect>();
    public void StartScreenEffect(string name) {
        if (ScreenEffectDic.ContainsKey(name)) {

        }
    }
}
public abstract class ScreenEffect {
    float endTIme;
    float updateCycle;
    public virtual float EndTime {
        get { return endTIme; } set { endTIme = value; }
    }
    public virtual float UpdateCycle {
        get { return updateCycle; }
        set { updateCycle = value; }
    }
    public virtual void OnStart() {
    }
    public virtual void OnUpdate() {

    }
    public virtual void OnEnd() {

    }
}
