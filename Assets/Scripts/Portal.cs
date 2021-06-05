using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;

public class Portal : MonoBehaviour
{
    [Header("다음 스테이지 씬 이름")]
    [SerializeField] string SceneName = null;
    [Header("페이드 이펙트")]
    [SerializeField] string FadeEffectName = "NormalFadeEffect";
    [SerializeField] float FadeEffectDuration = 2;
    public void OnTriggerEnter2D(Collider2D collision) {
        
        if(collision.gameObject.tag == "Player") {
            SceneUtilityManager.Instance.FadeAndSceneChange(SceneName, FadeEffectName, FadeEffectDuration);
        }
    }
}
