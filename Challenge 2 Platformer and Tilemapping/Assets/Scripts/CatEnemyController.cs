using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatEnemyController : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private SpriteRenderer sprite;
    private CapsuleCollider2D capsuleCollider2d;

    public Vector2 a, b;
    public float c, d;
    [Range(0, 1)]
    public float speed = 1f;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        capsuleCollider2d = GetComponent<CapsuleCollider2D>();
    }

    void Update()
    {
        transform.position = Vector3.Lerp(a, b, Mathf.PingPong(Time.time * speed, 1));

        if (rb2d.worldCenterOfMass.x <= c)
        {
            sprite.flipX = false;
            capsuleCollider2d.offset = new Vector2(-.2f, 0f);
        }
        if (rb2d.worldCenterOfMass.x >= d)
        {
            sprite.flipX = true;
            capsuleCollider2d.offset = new Vector2(.2f, 0f);
        }
    }
}
