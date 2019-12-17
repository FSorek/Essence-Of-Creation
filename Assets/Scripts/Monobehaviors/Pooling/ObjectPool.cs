using System;
using System.Collections;
using System.Collections.Generic;
using Data.Interfaces.Pooling;
using UnityEngine;

namespace Monobehaviors.Pooling
{
    public class ObjectPool : MonoBehaviour
    {
        private readonly Queue<GameObject> objects = new Queue<GameObject>();
        private Transform poolRoot;
        public GameObject prefab;
        public int prespawnAmount;


        private void OnEnable()
        {
            if (poolRoot == null)
                poolRoot = this.transform;
            AddObjects(prespawnAmount);
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
                var obj = Instantiate(prefab, poolRoot, true);
                objects.Enqueue(obj);
                if (obj.GetComponent<IGameObjectPooled>() == null)
                    obj.AddComponent<PooledGameObject>().Pool = this;
                else
                    obj.GetComponent<IGameObjectPooled>().Pool = this;
                obj.SetActive(false);
            }
        }

        public void ReturnToPool(GameObject obj)
        {
            obj.SetActive(false);
            objects.Enqueue(obj);
        }
        
        public IEnumerator DelayedReturnToPool(GameObject obj, float delay)
        {
            yield return new WaitForSeconds(delay);
            obj.SetActive(false);
            objects.Enqueue(obj);
        }

        private void OnDisable()
        {
            Destroy(poolRoot);
        }
    }
}