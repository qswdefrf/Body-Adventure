using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Lump : EnemyBase {
    [SerializeField] SO_Enemy SOLump = null;
    protected override SO_Enemy SOEnemy { get { return SOLump; } }
    [SerializeField, Range(0,360), Header("0-오른 90-위 180-왼 270-아래")]
    float StartAngle = 270;
    [SerializeField] float Speed = 1;
    protected override void Start() {
        base.Start();
        float angle = StartAngle;
        angle *= Mathf.Deg2Rad;
        Vector2 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
        transform.rotation = Quaternion.Euler(0, 0, StartAngle);
        rigid.AddForce(dir * Speed, ForceMode2D.Impulse);
    }
    public void SetLump(float speed) {
        Speed = speed;
    }
    public void SetLump(float angle, float speed) {
        StartAngle = angle;
        Speed = speed;
    }
    protected override void OnTriggerEnter2D(Collider2D collision) {
        base.OnTriggerEnter2D(collision);

        if (collision.gameObject.layer == LayerMask.NameToLayer("Wall") 
            || collision.gameObject.layer == LayerMask.NameToLayer("ObstacleWall")) {
            Destroy(gameObject);
        }
    }
    ~Lump() {
        transform.DOKill();
    }
}
