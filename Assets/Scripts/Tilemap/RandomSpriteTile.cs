using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(PolygonCollider2D))]
public class RandomSpriteTile : MonoBehaviour
{
    [SerializeField] List<Sprite> Sprites = new List<Sprite>();
    Sprite sprite;
    void Start()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        renderer.sprite = Sprites[Random.Range(0, Sprites.Count)];
        PolygonCollider2D collider = GetComponent<PolygonCollider2D>();
        sprite = renderer.sprite;
        List<Vector2> shapde = new List<Vector2>();
        sprite.GetPhysicsShape(0, shapde);
        collider.points = shapde.ToArray();
    }
}
