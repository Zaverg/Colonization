using UnityEngine;
using System.Collections.Generic;

public abstract class ObjectPool<T> : MonoBehaviour where T : Component, IReleasable<T>
{
    [SerializeField] private T _prefab;

    private Queue<T> _availableObjects;
    private List<T> _allObjects;

    private int _counter = 0;

    public void OnDisable()
    {
        foreach (T obj in _allObjects)
        {
            obj.Released -= PutObject;
        }

        _availableObjects.Clear();
        _allObjects.Clear();
    }

    public virtual void Initialize()
    {
        _availableObjects = new Queue<T>();
        _allObjects = new List<T>();
    }

    protected T GetObject()
    {
        T newObject = null;

        if (_availableObjects.Count == 0)
        {
            _counter++;
            newObject = Instantiate(_prefab);
            newObject.Released += PutObject;

            _allObjects.Add(newObject);

            return newObject;
        }

        newObject = _availableObjects.Dequeue();
        newObject.gameObject.SetActive(true);

        return newObject;
    }
     
    private void PutObject(T obj)
    {
        _availableObjects.Enqueue(obj);
        obj.transform.position = transform.position;
        obj.gameObject.SetActive(false);
    }
}
