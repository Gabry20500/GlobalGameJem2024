using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdGroupBehaviour : MonoBehaviour
{
    [SerializeField] float maybeThrowTime = 5f;
    [SerializeField] float randomTimeDifference = 2f;
    [SerializeField][Range(0f, 1f)] float throwProbability = 0.5f;

    [SerializeField] GameObject objPool;
    [SerializeField] GameObject playerObject;

    Vector3 startPos;
    float throwTime = 0;
    float elapsedTime = 0f;
    float speed = 20f;

    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        RandomizeTime();
    }

    private void OnGUI()
    {
        GUILayout.Label("ElapsedTime: " + elapsedTime + ", ThrowTime: " + throwTime);
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        transform.position = new Vector2(startPos.x, Mathf.Cos(elapsedTime * speed) + startPos.y);
    }

    void FixedUpdate()
    {
        if (elapsedTime >= throwTime)
        {
            if (Random.Range(0f, 1f) <= throwProbability)
            {
                ThrowProjectile();
            }
            RandomizeTime();

        }
    }

    void RandomizeTime()
    {
        elapsedTime = 0f;
        throwTime = maybeThrowTime + Random.Range(-1f, 1f) * randomTimeDifference;
    }

    void ThrowProjectile()
    {
        objPool.GetComponent<ObjectPool>().InstantiateProjectile(transform.position, playerObject.transform.position);

    }
}
