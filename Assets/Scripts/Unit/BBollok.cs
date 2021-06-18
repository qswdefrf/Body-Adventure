using UnityEngine;
using DG.Tweening;
public class BBollok : EnemyBase
{
    [SerializeField] SO_Enemy SOBBollok = null;
    protected override SO_Enemy SOEnemy { get { return SOBBollok; } }
    [Header("위로 이동 = 체크, 아래로 이동 = 체크해제")]
    [SerializeField] bool UpAndDown = false;
    [SerializeField] float MoveDistance = 0.5f;
    [SerializeField] float MoveDuration = 1;
    [SerializeField] float CircleRadius = 0.2f;
    [SerializeField] Color CircleColor = Color.blue;
    [Space(20)]
    [SerializeField] Vector2 PlayerSensingPoint = new Vector2(0,0);
    [SerializeField] Vector2 PlayerSensingSize = new Vector2(1,1);
    [SerializeField] float PlayerSensingAngle = 0;
    [SerializeField] Color SensingBoxColor = Color.green;
    bool CheckPlayer;

    public void Update() {
        Vector2 point = (Vector2)transform.position + PlayerSensingPoint;
        //float angle = Mathf.Atan2()
        Collider2D[] collisions = Physics2D.OverlapBoxAll(point, PlayerSensingSize, transform.eulerAngles.z + PlayerSensingAngle);
        foreach (Collider2D collision in collisions) {
            if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Invincibility") {
                if(CheckPlayer == false) {
                    Moving();
                    CheckPlayer = true;
                }
            }
        }
    }
    private void Moving() {
        int movedir = 270;
        if (UpAndDown)
            movedir = 90;

        float downdir = movedir + transform.eulerAngles.z;
        downdir *= Mathf.Deg2Rad;
        Vector2 MovePoint = new Vector2(Mathf.Cos(downdir), Mathf.Sin(downdir)) * MoveDistance;
        transform.DOMove((Vector2)transform.position + MovePoint, MoveDuration);

    }

    private void OnDrawGizmos() {
        Vector2 point = (Vector2)transform.position + PlayerSensingPoint;
        float SensingSizeX= PlayerSensingSize.x / 2;
        float SensingSizeY = PlayerSensingSize.y / 2;
        float diagonalPower = Mathf.Sqrt((SensingSizeX * SensingSizeX) + (SensingSizeY * SensingSizeY));
        float dir = Mathf.Atan2(SensingSizeY, SensingSizeX) * Mathf.Rad2Deg;
        float dir1 = (dir + transform.eulerAngles.z + PlayerSensingAngle);
        dir1 *= Mathf.Deg2Rad;
        float dir2 = (-dir + transform.eulerAngles.z + PlayerSensingAngle);
        dir2 *= Mathf.Deg2Rad;
        float dir3 = (dir + 180 + transform.eulerAngles.z + PlayerSensingAngle);
        dir3 *= Mathf.Deg2Rad;
        float dir4 = (-dir + 180 + transform.eulerAngles.z + PlayerSensingAngle);
        dir4 *= Mathf.Deg2Rad;

        Vector2[] dirPoint = new Vector2[] {
            new Vector2(Mathf.Cos(dir1), Mathf.Sin(dir1)) * diagonalPower,
            new Vector2(Mathf.Cos(dir2), Mathf.Sin(dir2)) * diagonalPower,
            new Vector2(Mathf.Cos(dir3), Mathf.Sin(dir3)) * diagonalPower,
            new Vector2(Mathf.Cos(dir4), Mathf.Sin(dir4)) * diagonalPower
        };
        Gizmos.color = SensingBoxColor;
        Gizmos.DrawLine(point + dirPoint[0], point + dirPoint[1]);
        Gizmos.DrawLine(point + dirPoint[1], point + dirPoint[2]);
        Gizmos.DrawLine(point + dirPoint[2], point + dirPoint[3]);
        Gizmos.DrawLine(point + dirPoint[3], point + dirPoint[0]);

        Gizmos.color = CircleColor;
        int movedir = 270;
        if (UpAndDown)
            movedir = 90;
        float downdir = movedir + transform.eulerAngles.z;
        downdir *= Mathf.Deg2Rad;
        Vector2 asd = new Vector2(Mathf.Cos(downdir), Mathf.Sin(downdir)) * MoveDistance;
        Gizmos.DrawLine(transform.position, (Vector2)transform.position + asd);
        Gizmos.DrawWireSphere((Vector2)transform.position + asd, CircleRadius);
    }
}
