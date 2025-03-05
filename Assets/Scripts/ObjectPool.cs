//using System;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPool<T> where T : MonoBehaviour
{
    public T Prefab { get; }
    public bool AutoExpand { get; set; }
    public Transform ItemContainer { get; }

    private List<T> _pool;

    //private readonly Action<T> _onGet; // init
    //private readonly Action<T> _onRelease;//clearing
    public ObjectPool(T prefab, int size, Transform itemContainer,bool autoExpand = false)
    {
        this.Prefab = prefab;
        this.ItemContainer = itemContainer;
        this.AutoExpand = autoExpand;
        CreatePool(size);
    }

    private void CreatePool(int size)
    {
        this._pool = new List<T>();

        for (int i = 0; i < size; i++)
        {
            CreateObject();
        }
    }

    private T CreateObject(bool isActiveByDefault = false)
    {
        var createdObject = UnityEngine.Object.Instantiate(this.Prefab, this.ItemContainer);
        createdObject.gameObject.SetActive(isActiveByDefault);
        this._pool.Add(createdObject);

        return createdObject;
    }

    public bool HasFreeElement(out T element)
    {
        foreach (var item in _pool)
        {
            if (!item.isActiveAndEnabled)
            {
                element = item;
                return true;
            }
        }

        element = null;
        return false;
    }

    public T GetFreeElement(Vector3 position = default, Quaternion rotation = default)
    {
        if (HasFreeElement(out T element))
        {
            element.transform.SetPositionAndRotation(position != default ? position : element.transform.position, rotation != default ? rotation : Quaternion.identity);
            element.gameObject.SetActive(true);
            //_onGet?.Invoke(element);
            return element;
        }
        if (this.AutoExpand)
        {
            element = CreateObject(true);
            element.transform.SetPositionAndRotation(position, rotation);
            //_onGet?.Invoke(element);
            return element;
        }
        throw new System.Exception($"pool type {typeof(T)}" + "has no free element");
    }

    public void ReturnToPool(T element)
    {
        if (element == null || !_pool.Contains(element))
        {
            Debug.LogWarning($"pool typeof {typeof(T)} dont contains {element}");
            return;
        }

        //_onRelease?.Invoke(element);
        element.gameObject.SetActive(false);
    }

    public void Clear()
    {
        foreach (var item in _pool)
        {
            if (item != null) UnityEngine.Object.Destroy(item.gameObject);
        }
        _pool.Clear();
    }
}
