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



    private void Awake()
    {
        int rand = Random.Range(0, objectSprite.Count);
        gameObject.GetComponent<SpriteRenderer>().sprite = objectSprite[rand];
    }
    
    private void OnEnable()
    {
        startPos = transform.position;    
    }


    public void Throw(Vector3 MidPos, Vector3 EndPos)
    {
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
