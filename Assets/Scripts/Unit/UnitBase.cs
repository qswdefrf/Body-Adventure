using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using DG.Tweening;
public class UnitBase : MonoBehaviour
{
    public bool stun;
    protected Rigidbody2D rigid;
    protected SpriteRenderer spriteRenderer;


    private void Init() {
        spriteRenderer.color = new Color(0, 0, 0, 1);
    }
    protected virtual void Awake() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigid = GetComponent<Rigidbody2D>();
    }
    protected virtual void Start() {
    }
    public virtual void OnDamaged(UnitBase Attacker, float power) { }
    #region 스턴
    protected void Stun(float stunTime) {
        StopCoroutine("C_Stun");
        StartCoroutine(C_Stun(stunTime));
    }
    IEnumerator C_Stun(float stunTime) {
        while (stunTime >= 0) {
            stun = true;
            yield return null;
            stunTime -= Time.deltaTime;
        }
        stun = false;
    }
    #endregion
    public void SpawnParticle(ParticleSystem particle, float duration, bool infinite) {
        if (infinite)
            Instantiate(particle, transform);
        else {
            GameObject spawnParticle = Instantiate(particle, transform).gameObject;
            Destroy(spawnParticle, duration);
        }

    }
    protected virtual void Die() {
        Destroy(gameObject);
    }
    private void OnDestroy() {
        transform.DOKill();
    }
}
