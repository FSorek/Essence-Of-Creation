using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public int PrespawnAmount = 0;
    private Queue<GameObject> objects = new Queue<GameObject>();


    private void Awake()
    {
        AddObjects(PrespawnAmount);
    }

    public GameObject Get()
    {
        if (objects.Count == 0)
        {
            AddObjects(1);
        }

        return objects.Dequeue();
    }

    private void AddObjects(int v)
    {
        for (int i = 0; i < v; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.SetActive(false);
            objects.Enqueue(obj);
            obj.GetComponent<IGameObjectPooled>().Pool = this;
            obj.transform.SetParent(this.transform);
        }
    }

    public void ReturnToPool(GameObject obj, float delay = 0f)
    {
        StartCoroutine(DelayedReturnToPool(obj, delay));
    }

    private IEnumerator DelayedReturnToPool(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        obj.SetActive(false);
        objects.Enqueue(obj);
    }
}
