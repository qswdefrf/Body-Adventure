using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class OptionManager : DontDestroySingletonBehaviour<OptionManager>
{
    int ScreenWidth = 1280;
    int ScreenHwight = 1024;

    public void Start() {
        SetScreenResolution(ScreenWidth, ScreenHwight);
    }
    public void SetScreenResolution(int width, int height) {
        Screen.SetResolution(width, height, true);
        Debug.Log("화면 해상도를 " + width + " X " + height + "로 설정했습니다.");
    }
}
