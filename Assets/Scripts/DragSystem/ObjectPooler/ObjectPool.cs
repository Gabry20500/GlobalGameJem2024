using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPool : MonoBehaviour
{
    public static ObjectPool SharedInstance;
    public List<GameObject> pooledObjects;
    [SerializeField] public GameObject objectToPool;
    [SerializeField] public int amountToPool;

    void Awake()
    {
        SharedInstance = this;
    }

    void Start()
    {
        pooledObjects = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(objectToPool);
            tmp.SetActive(false);
            pooledObjects.Add(tmp);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        GameObject newObj;

        newObj = Instantiate(objectToPool);
        newObj.SetActive(false);
        pooledObjects.Add(newObj);

        return newObj;
    }


    public void InstantiateProjectile(Vector2 startPosition, Vector2 endPosition)
    {
        var projectile = GetPooledObject();
        projectile.SetActive(true);

        projectile.transform.position = startPosition;

        float x = (startPosition.x + endPosition.x) / 2;
        float y = ((startPosition.y + endPosition.y) / 2) * 1.5f;
        Vector3 midPos = new Vector3(x,y,0.0f);

        projectile.GetComponent<Projectile>().Throw(midPos, endPosition);

    }
}
