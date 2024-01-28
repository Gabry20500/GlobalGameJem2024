using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrowdGroupBehaviour : MonoBehaviour
{
    [SerializeField] float maybeThrowTime = 5f;
    [SerializeField] float randomTimeDifference = 2f;
    [SerializeField][Range(0f, 1f)] float throwProbability = 0.5f;
    [SerializeField] float diffconstant = 10;
    float internaldiff;

    [SerializeField] GameObject objPool;
    [SerializeField] GameObject playerObject;

    [SerializeField] GameObject crowdBehaviour;
    CrowdBehaviour crowd;
    Vector3 startPos;
    float throwTime = 0;
    float elapsedTime = 0f;
    float speed = 20f;
    bool isDisabled = false;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.stats.died += DisableCrowd;
        startPos = transform.position;
        crowd = this.gameObject.GetComponentInParent<CrowdBehaviour>();
        RandomizeTime();
        GetDifficulty();
    }

    void GetDifficulty()
    {
        internaldiff = crowd.getDifficulty();
    }
    public void GetDifficulty(int diff)
    {
        internaldiff = diff;
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        transform.position = new Vector2(startPos.x, Mathf.Cos(elapsedTime * speed) + startPos.y);
    }

    void FixedUpdate()
    {
        if (elapsedTime >= throwTime && !isDisabled)
        {
            float val = (internaldiff/diffconstant);

            if (Random.Range(0f, 1f) <= throwProbability + val)
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

    public void DisableCrowd()
    {
        isDisabled = true;
    }
}
