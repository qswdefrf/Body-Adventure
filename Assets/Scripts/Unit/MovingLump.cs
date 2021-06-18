using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MovingLump : EnemyBase {
    [SerializeField] SO_Enemy SOMovingLump = null;
    [SerializeField] float MoveDuration = 1;
    public float NextMoveTime = 0.2f;
    public bool MoveStartRed = true;
    public float HandleSize = 0.3f;
    [SerializeField] Ease EaseType = Ease.InOutSine;
    [SerializeField] public Vector2 Point1 = new Vector2(0,1);
    [SerializeField] public Vector2 Point2 = new Vector2(0, -1);
    [SerializeField] bool Preview;
    protected override SO_Enemy SOEnemy { get { return SOMovingLump; } }
    protected override void Awake() {
        base.Awake();
        if (MoveStartRed) {
            transform.position = Point1;
            StartMovePoint(MoveStartRed);
        } else {
            transform.position = Point2;
            StartMovePoint(MoveStartRed);
        }
    }

    void StartMovePoint(bool moveStartRed) {
        Vector2 point;
        if (moveStartRed) {
            point = Point2;
            moveStartRed = false;
        } else {
            point = Point1;
            moveStartRed = true;
        }
        Vector2 trasPos = transform.position;
        transform.DOMove(point, MoveDuration).SetEase(EaseType).OnComplete(() => { MovePoint(moveStartRed);});
    }

    void MovePoint(bool moveStartRed) {
        StartCoroutine(C_MovePoint(moveStartRed));
    }
    IEnumerator C_MovePoint(bool moveStartRed) {
        Vector2 point;
        if (moveStartRed) {
            point = Point2;
            moveStartRed = false;
        } else {
            point = Point1;
            moveStartRed = true;
        }
        yield return new WaitForSeconds(NextMoveTime);
        transform.DOMove(point, MoveDuration).SetEase(EaseType).OnComplete(() => { MovePoint(moveStartRed); });
    }

    ~MovingLump() {
        transform.DOKill();
    }
}
