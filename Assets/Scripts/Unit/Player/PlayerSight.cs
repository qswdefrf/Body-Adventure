using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using DG.Tweening;
public class PlayerSight : MonoBehaviour
{
    [SerializeField] Light2D PlayerSightLight = null;
    float normalInnerRadius;
    public float NormalInnerRadius {
        get { return normalInnerRadius; }
    }

    float normalOutterRadius;
    public float NormalOutterRadius {
        get { return normalOutterRadius; }
    }
    public void Start() {
        normalInnerRadius = PlayerSightLight.pointLightInnerRadius;
        normalOutterRadius = PlayerSightLight.pointLightOuterRadius;
    }
    public void IncreasinglyChangeSight(float innerRadius, float minInnerRadius, float outterRadius, float minOutterRadius, float duration) {
        innerRadius = Mathf.Max(innerRadius, minInnerRadius);
        outterRadius = Mathf.Max(outterRadius, minOutterRadius);
        DOTween.To(() => PlayerSightLight.pointLightInnerRadius, x => PlayerSightLight.pointLightInnerRadius = x, innerRadius, duration);
        DOTween.To(() => PlayerSightLight.pointLightOuterRadius, x => PlayerSightLight.pointLightOuterRadius = x, outterRadius, duration);
    }
    public void SetPlayerSight(float innerRadius, float outterRadius) {
        PlayerSightLight.pointLightInnerRadius = innerRadius;
        PlayerSightLight.pointLightOuterRadius = outterRadius;
    }
    public void SetShadowIntensity(float value) {
        value = Mathf.Clamp(value, 0, 1);
        PlayerSightLight.shadowIntensity = value;
    }
    public void SetColor(Color color) {
        PlayerSightLight.color = color;
    }
}
