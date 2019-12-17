using System;
using System.Collections.Generic;
using UnityEngine;

public class GlobalList<T> : ScriptableObject
{
    public event Action<T> OnItemAdded = delegate {  };
    public event Action<T> OnItemRemoved = delegate {  };
    private readonly List<T> items = new List<T>();
    public List<T> Items => items;
    public void Add(T item)
    {
        if(!items.Contains(item))
        {
            items.Add(item);
            OnItemAdded(item);
        }
    }

    public void Remove(T item)
    {
        if (items.Contains(item))
        {
            items.Remove(item);
            OnItemRemoved(item);
        }
    }

    private void OnEnable()
    {
        items?.Clear();
    }
}