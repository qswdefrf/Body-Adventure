using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
[ExecuteInEditMode]
public class MovingLump : EnemyBase {
    [SerializeField] SO_Enemy SOMovingLump = null;
    [SerializeField] float MoveDuration = 1;
    public float NextMoveTime = 0.2f;
    public bool MoveStartRed = true;
    public float HandleSize = 0.3f;
    [SerializeField] Ease EaseType = Ease.InOutSine;
    [HideInInspector] public Vector2 Point1 = new Vector2(0,1);
    [HideInInspector] public Vector2 Point2 = new Vector2(0, -1);
    protected override SO_Enemy SOEnemy { get { return SOMovingLump; } }
    WaitForSeconds nextMoveTime;
    protected override void Start() {
        base.Start();
        if (MoveStartRed) {
            transform.position = Point1;
            MovePoint(MoveStartRed);
        } else {
            transform.position = Point2;
            MovePoint(MoveStartRed);
        }
    }
    void MovePoint(bool moveStartRed) {
        StartCoroutine(C_MovePoint(moveStartRed));
    }
    IEnumerator C_MovePoint(bool moveStartRed) {
        Vector2 point;
        if (moveStartRed) {
            point = Point1;
            moveStartRed = false;
        } else {
            point = Point2;
            moveStartRed = true;
        }
        Vector2 trasPos = transform.position;
        yield return new WaitForSeconds(NextMoveTime);
        transform.DOMove(point, MoveDuration).SetEase(EaseType).OnComplete(() => { MovePoint(moveStartRed); });
    }
}
