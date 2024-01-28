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

    [SerializeField] GameObject crowdLayer1;
    [SerializeField] GameObject crowdLayer2;
    [SerializeField] GameObject crowdLayer3;
    Vector3 startLayer1;
    Vector3 startLayer2;
    Vector3 startLayer3;

    CrowdBehaviour crowd;
    float throwTime = 0;
    float elapsedTime = 0f;
    float speed = 13f;
    bool isDisabled = false;

    public float horizontalSpeed;
    public float verticalSpeed;


    float throwCountdown = 0;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.stats.died += DisableCrowd;
        crowd = this.gameObject.GetComponentInParent<CrowdBehaviour>();
        RandomizeTime();
        GetDifficulty();

        startLayer1 = crowdLayer1.transform.position;
        startLayer2 = crowdLayer2.transform.position;
        startLayer3 = crowdLayer3.transform.position;

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

        if (throwCountdown > 0)
        {
            throwCountdown -= Time.deltaTime;
        }

        verticalSpeed = 0.01f * throwCountdown;

        crowdLayer1.transform.position = new Vector2(Mathf.Sin(elapsedTime * speed) * horizontalSpeed + startLayer1.x, Mathf.Sin(elapsedTime * speed) * verticalSpeed + startLayer1.y);
        crowdLayer2.transform.position = new Vector2(Mathf.Sin(elapsedTime * speed + 1.5f) * horizontalSpeed + startLayer2.x, Mathf.Sin(elapsedTime * speed + 1.5f) * verticalSpeed + startLayer2.y);
        crowdLayer3.transform.position = new Vector2(Mathf.Sin(elapsedTime * speed + 2.4f) * horizontalSpeed + startLayer3.x, Mathf.Sin(elapsedTime * speed + 2.4f) * verticalSpeed + startLayer3.y);
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
        throwCountdown = 15f;
        StartCoroutine(Wait(1f));
    }

    public void DisableCrowd()
    {
        isDisabled = true;
    }


    IEnumerator Wait(float seconds)
    {
        yield return new WaitForSeconds(seconds);

        objPool.GetComponent<ObjectPool>().InstantiateProjectile(transform.position + new Vector3(4f + Random.Range(.5f, 3f), 0f, 0f), playerObject.transform.position);
    }
}
