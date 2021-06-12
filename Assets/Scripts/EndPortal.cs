using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPortal : MonoBehaviour
{
    [Header("다음 스테이지 씬 이름")]
    [SerializeField] string SceneName = null;
    [Header("페이드 이펙트")]
    [SerializeField] string FadeEffectName = "NormalFadeEffect";
    [SerializeField] float FadeEffectDuration = 2;
    [SerializeField] Vector2 ColliderSize = new Vector2(1, 1);
    private void Update() {
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, ColliderSize, 0);
        foreach (Collider2D collider in colliders) {
            if (collider.tag == "Player")
                SceneUtilityManager.Instance.FadeAndSceneChange(SceneName, FadeEffectName, FadeEffectDuration);
        }
    }
    public void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(transform.position, ColliderSize);
    }
}
