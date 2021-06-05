using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SO_Villis", menuName = "SO/Villus", order = int.MaxValue)]
public class SO_Villus : SO_Enemy
{
    [SerializeField, Header("접촉 중 몇초마다 디버프 수치 증가 할건지"), Range(0,30)]
    float contactTime = 2;
    public float ContactTime { get { return contactTime; } }
}
