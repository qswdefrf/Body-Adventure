using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBase : UnitBase {
    protected abstract SO_Enemy SOEnemy {get;}
    PlayerBase ContactPlayer;
    protected virtual void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            PlayerBase player = collision.transform.GetComponent<PlayerBase>();
            ContactPlayer = player;
            if (SOEnemy.AttackPlayer) {
                DamagePlayer(player);
            } else if (SOEnemy.PushPlayer) {
                Vector2 dir = (player.transform.position - transform.position).normalized;
                player.PushPlayer(dir, SOEnemy.Power);
            }
            if (SOEnemy.IsDebuff) {
                player.GetSpeedBuff(SOEnemy.ID, SOEnemy.MaxStack, SOEnemy.SlowPower, SOEnemy.SlowDuration);
            }
            CollisionEnter2DPlayer(player);
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            PlayerBase player = collision.transform.GetComponent<PlayerBase>();
            ContactPlayer = player;
            if (SOEnemy.AttackPlayer) {
                DamagePlayer(player);
            } else if (SOEnemy.PushPlayer) {
                Vector2 dir = (player.transform.position - transform.position).normalized;
                player.PushPlayer(dir, SOEnemy.Power);
            }
            if (SOEnemy.IsDebuff) {
                player.GetSpeedBuff(SOEnemy.ID, SOEnemy.MaxStack, SOEnemy.SlowPower, SOEnemy.SlowDuration);
            }
            CollisionTriggerEnter2DPlayer(player);
        }
    }
    protected virtual void OnCollisionStay2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            PlayerBase player = ContactPlayer;
            CollisionStay2DPlayer(player);
        }
    }
    protected virtual void OnTriggerStay2D(Collider2D collision) {
        if (collision.gameObject.tag == "Player") {
            PlayerBase player = ContactPlayer;
            CollisionTriggerStay2DPlayer(player);
        }
    }
    public virtual void CollisionEnter2DPlayer(PlayerBase playerBase) {
    }
    public virtual void CollisionTriggerEnter2DPlayer(PlayerBase playerBase) {
    }
    public virtual void CollisionStay2DPlayer(PlayerBase playerBase) {
    }
    public virtual void CollisionTriggerStay2DPlayer(PlayerBase playerBase) {
    }
    void DamagePlayer(PlayerBase playerBase) {
        if (SOEnemy.AttackPlayer)
            playerBase.OnDamaged(this, SOEnemy.Power);
    }
}
