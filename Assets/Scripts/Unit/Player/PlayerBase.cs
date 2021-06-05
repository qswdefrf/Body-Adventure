using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using UnityEngine.UI;
using UnityEngine.Experimental.Rendering.Universal;
[Serializable]
class PlayerSprite {
    public Sprite sprite = null;
    public Vector2 CollisionSize = new Vector2(1,1);
}
class SpeedDeBuff {
    PlayerBase Target;
    int id;
    public int ID { get { return id; } }
    int maxStack;
    public int MaxStack { get { return maxStack; } }
    int buffStack;
    public int BuffStack { get { return buffStack; } }
    float percentPerStack;
    public float PercentPerStack { get { return percentPerStack; } }

    public float TotalPercent { get { return Mathf.Min(1, percentPerStack * buffStack); } }
    float duration;
    public float Duration { get { return duration; } }
    public float LastDuration;

    public float Value(float calculateSpeed) {
        return calculateSpeed * (1 - TotalPercent);
    }
    public SpeedDeBuff(PlayerBase target,int _id,int _maxStack,int _stack,float _percentPerStack, float _duration) {
        id = _id;
        Target = target;
        maxStack = _maxStack;
        buffStack = Mathf.Clamp(_stack, 0, maxStack);
        percentPerStack = Mathf.Clamp(_percentPerStack, 0, 1);
        duration = _duration;
        LastDuration = _duration;
    }
    public void AddStack(int value) {
        LastDuration = duration;
        buffStack = Mathf.Clamp(buffStack + value, 0, maxStack);
    }
}
public class PlayerBase : UnitBase
{
    [SerializeField] float speed = 2; public float Speed { get { return TotalSpeed(); } }
    float TotalSpeed() {
        float totalSpeed = speed;
        foreach(SpeedDeBuff debuff in BuffList) {
            totalSpeed = debuff.Value(totalSpeed);
        }
        return totalSpeed;
    }
    List<SpeedDeBuff> BuffList = new List<SpeedDeBuff>();
    int hp = 4;
    public ParticleSystem DieParticle;
    public PlayerSight playerSight;
    [SerializeField] Color NormalColor = new Color(1,1,1,1);
    [Header("피격시 무적 시간")]
    public float InvincibilityDuration = 1;
    [Header("피격시 스턴 시간")]
    public float StunDuration = 0.5f;

    protected override void Start() {
        base.Start();
        playerSight = GetComponent<PlayerSight>();
        StartCoroutine(SpeedBuffTimer());
    }
    public void Update() {
        if (Input.GetKeyDown(KeyCode.K)) {
            Die();
        }
    }

    #region 스피드 버프
    public void GetSpeedBuff(int id,int maxStack,float percent, float duration) {
        foreach(SpeedDeBuff debuff in BuffList) {
            if (debuff.ID == id) {
                debuff.AddStack(1);
                return;
            }
        }
        SpeedDeBuff speedBuff = new SpeedDeBuff(this, id, maxStack, 1, percent, duration);
        BuffList.Add(speedBuff);
    }
    IEnumerator SpeedBuffTimer() {
        while (true) {
            if (BuffList.Count > 0 && BuffList != null) {
                for (int i = 0; i < BuffList.Count; i++) {
                    BuffList[i].LastDuration -= Time.deltaTime;
                    if (BuffList[i].LastDuration <= 0) {
                        BuffList.Remove(BuffList[i]);
                    }
                }
            }
            yield return null;
        }
    }
    #endregion
    public override void OnDamaged(UnitBase attacker, float power) {
        hp -= 1;
        switch (hp) {
            case 3:
                transform.localScale = new Vector2(3.6f, 3.6f);
                NormalColor = new Color (0.8f, 1f, 0.8f, 1);
                DamagedEffect(attacker, power);
                break;
            case 2:
                transform.localScale = new Vector2(4.2f, 4.2f);
                NormalColor = new Color(0.6f, 0.8f, 0.6f, 1);
                DamagedEffect(attacker, power);
                break;
            case 1:
                transform.localScale = new Vector2(4.8f, 4.8f);
                NormalColor = new Color(0.4f, 0.6f, 0.4f, 1);
                DamagedEffect(attacker, power);
                break;
            case 0:
                Die();
                return;
        }      
    }
    void DamagedEffect(UnitBase attacker, float power) {
        Vector2 Recoilvec = (transform.position - attacker.transform.position).normalized;
        PushPlayer(Recoilvec, 1);
        spriteRenderer.DOKill();
        CameraController.Instance.ShakeCamera(transform, 0.5f, 0.2f, 0.05f);
        StartCoroutine(C_Invincibility());
        EventManager<PlayerEvent>.Instance.PostEvent(PlayerEvent.Damaged, this, power);
    }
    public void PushPlayer(Vector2 dir, float power) {
        rigid.AddForce(dir * power, ForceMode2D.Impulse);
        Stun(StunDuration);
    }
    IEnumerator C_Invincibility() {
        float invincibilityDuration = InvincibilityDuration;
        gameObject.tag = "Invincibility";
        gameObject.layer = LayerMask.NameToLayer("Invincibility");
        while (invincibilityDuration > 0) {
            Color spriteColor = spriteRenderer.color;
            spriteRenderer.color = new Color(spriteColor.r, spriteColor.g, spriteColor.b, 0.2f);
            invincibilityDuration -= Time.deltaTime;
            yield return null;
        }
        gameObject.tag = "Player";
        gameObject.layer = LayerMask.NameToLayer("Player");
        spriteRenderer.color = NormalColor;
    }
    protected override void Die() {
        stun = true;
        gameObject.tag = "Invincibility";
        gameObject.layer = LayerMask.NameToLayer("Invincibility");
        CameraController.Instance.ShakeCamera(1, 1, 0.05f);
        StartCoroutine(DieEffect(2));
    }
    IEnumerator DieEffect(float waitTime) {
        yield return new WaitForSeconds(waitTime);
        Instantiate(DieParticle, transform.position, Quaternion.identity);
        EventManager<PlayerEvent>.Instance.PostEvent(PlayerEvent.Die, this, null);
        EventManager<GameEvent>.Instance.PostEvent(GameEvent.StageRestart, this, null);
        Debug.Log("사망");
        gameObject.SetActive(false);
    }
}

