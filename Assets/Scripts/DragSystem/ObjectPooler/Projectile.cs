using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] List<Sprite> objectSprite;

    Vector3 startPos;
    Vector3 midPos;
    Vector3 endPos;
    float duration = 1f;
    float radius = 0.5f;
    float elapsedTime = 0f;
    float startRot;
    float spinSpeed;


    private void Update()
    {
        elapsedTime += Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, elapsedTime * spinSpeed);
    }
    
    private void OnEnable()
    {
        startPos = transform.position;

        startRot = Random.Range(0f, 360f);
        spinSpeed = 150f * Random.Range(1f, 2f);
        transform.rotation = Quaternion.Euler(0, 0, startRot);

        gameObject.GetComponent<SpriteRenderer>().sprite = objectSprite[Random.Range(0, objectSprite.Count)];
    }


    public void Throw(Vector3 StartPos, Vector3 MidPos, Vector3 EndPos)
    {
        startPos = StartPos;

        startPos.x = startPos.x + Random.Range(-1f, 1f) * 1.5f;
        endPos = EndPos;
        midPos = MidPos;

        StartCoroutine(parabolicMovement());
    }

    private IEnumerator parabolicMovement()
    {
        float time = 0;

        while(time <= duration)
        {
            Vector3 position = CalculateBezierPoint(time, startPos, midPos, endPos);
            transform.position = position;

            time += Time.deltaTime / duration;
            yield return null;
        }

        CastCollision();
    }

    Vector3 CalculateBezierPoint(float t, Vector3 p0, Vector3 p1, Vector3 p2)
    {
        float u = 1 - t;
        float tt = t * t;
        float uu = u * u;
        float uuu = uu * u;
        float ttt = tt * t;

        Vector3 p = uuu * p0;
        p += 2 * uu * t * p1;
        p += tt * p2;

        return p;
    }

    void CastCollision()
    {
        Collider2D[] objs = Physics2D.OverlapCircleAll(transform.position, radius);

        foreach (var obj in objs)
        {
            var playerquery = obj.GetComponent<StatsComponent>();

            if (playerquery != null)
            {
                playerquery.HealthChange(-1);
            }
        }

        gameObject.SetActive(false);

    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    Debug.Log("Sei TU?");
        
    //    if (other.gameObject.CompareTag("PlayerHitbox"))
    //    {
    //        other.gameObject.GetComponent<StatsComponent>().TakeDamage();

    //        transform.gameObject.SetActive(false);
    //    }
    //}
}
