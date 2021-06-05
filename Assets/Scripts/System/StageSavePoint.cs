using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSave;
public class StageSavePoint : MonoBehaviour
{
    [HideInInspector]public int SavePointIndex = 0;
    public Vector2 LoadGastrinStartPos;

    public void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "Invincibility") {
            EventManager<GameEvent>.Instance.PostEvent(GameEvent.SavePoint, this, SavePointIndex);
        }
    }
}
