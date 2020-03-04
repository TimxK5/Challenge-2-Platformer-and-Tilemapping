using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFOEnemyController: MonoBehaviour
{

    public Vector2 a, b;
    [Range(0, 1)]
    public float speed = 1f;

    void Start()
    {

    }

    void Update()
    {
        transform.position = Vector3.Lerp(a, b, Mathf.PingPong(Time.time * speed, 1));
    }
}