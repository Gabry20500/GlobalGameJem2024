using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyTitleComponent : MonoBehaviour
{
    float speed = 1.1f;
    float magnitude = 0.2f;
    float yStart;
    float elapsedTime = 0;

    void Start()
    {
        yStart = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        transform.position = new Vector2(transform.position.x, Mathf.Sin(elapsedTime * speed) * magnitude + yStart);
    }
}
