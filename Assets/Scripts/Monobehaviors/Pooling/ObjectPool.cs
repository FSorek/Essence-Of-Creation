using System.Collections;
using System.Collections.Generic;
using Data.Interfaces.Pooling;
using UnityEngine;

namespace Monobehaviors.Pooling
{
    public class ObjectPool : MonoBehaviour
    {
        private readonly Queue<GameObject> objects = new Queue<GameObject>();
        public GameObject prefab;
        public int PrespawnAmount;


        private void Awake()
        {
            AddObjects(PrespawnAmount);
        }

        public GameObject Get()
        {
            if (objects.Count == 0) AddObjects(1);

            return objects.Dequeue();
        }

        private void AddObjects(int v)
        {
            for (int i = 0; i < v; i++)
            {
                var obj = Instantiate(prefab, transform, true);
                objects.Enqueue(obj);
                obj.GetComponent<IGameObjectPooled>().Pool = this;
                obj.SetActive(false);
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
}