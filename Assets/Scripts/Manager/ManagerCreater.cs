using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerCreater : MonoBehaviour
{
    [SerializeField] List<GameObject> MonoSingletonObjectList = new List<GameObject>();
    private void Awake() {
        foreach (GameObject manager in MonoSingletonObjectList)
            Instantiate(manager);
    }
}
