using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Villus : EnemyBase {
    [SerializeField]SO_Villus SOVillus = null;
    float PlayerContactTime = 0;
    protected override SO_Enemy SOEnemy { get { return SOVillus; } }

    public override void CollisionTriggerEnter2DPlayer(PlayerBase playerBase) {
        PlayerContactTime = SOVillus.ContactTime;
    }
    public override void CollisionTriggerStay2DPlayer(PlayerBase playerBase) {
        PlayerContactTime -= Time.deltaTime;
        if (PlayerContactTime <= 0) {
            PlayerContactTime = SOVillus.ContactTime;
            playerBase.GetSpeedBuff(SOVillus.ID, SOVillus.MaxStack, SOVillus.SlowPower, SOVillus.SlowDuration);
        }
    }
}
