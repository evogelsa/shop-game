using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestrian : MonoBehaviour {

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private float maxSpeed = 3f;
    private float minSpeed = 2f;
    private float sidewalkWidth = 2f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        Vector3 startPos = transform.position;
        startPos.y -= Random.value * sidewalkWidth;
        transform.position = startPos;
        rb.velocity = Vector2.left * (Random.value*(maxSpeed-minSpeed)+minSpeed);

        sprite.sortingOrder = (int) (-transform.position.y/sidewalkWidth * 100);
    }
}
