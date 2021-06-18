using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class TeamLogo : MonoBehaviour
{
    [SerializeField] Image LogoImage = null;
    [SerializeField] float FadeOutStart = 2;
    [SerializeField] float FadeOut = 2;
    private void Awake() {
        StartCoroutine(LogoEvent());
    }
    IEnumerator LogoEvent() {
        yield return new WaitForSeconds(FadeOutStart);
        SceneUtilityManager.Instance.FadeAndSceneChange("Title", "NormalFadeEffect", FadeOut);
    }
}
