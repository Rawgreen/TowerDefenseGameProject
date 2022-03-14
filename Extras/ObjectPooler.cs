using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int poolSize = 10;

    // every object we preload needs to be stored in a list to keep track of it
    private List<GameObject> _pool;

    // to having an organized hierarchy we need to add preloaded objects into a container *** creating container ***
    private GameObject _poolContainer;
    private void Awake()
    {
        // pool equals to a new list of game object
        _pool = new List<GameObject>();

        // creating and defining name of the container
        _poolContainer = new GameObject($"Pool - {prefab.name}");

        // after initializing pool
        CreatePooler();
    }
    //creating a method that adds a new instance of the prefab and storage in our list
    private GameObject CreateInstance()
    {
        // now pooler reloads all of the prefabs, they don't need to be activated
        GameObject newInstance = Instantiate(prefab);
        // Assigning new parent to controller
        newInstance.transform.SetParent(_poolContainer.transform);
        newInstance.SetActive(false);
        return newInstance;
    }
    //creating Pooler
    private void CreatePooler()
    {
        // for the amount of prefabs we want to create (poolsize)
        for (int i = 0; i < poolSize; i++)
        {
            _pool.Add(CreateInstance());
        }
    }
    // we need to get one instance and calling this gets instance from pool
    public GameObject GetInstanceFromPool()
    {
        // if there is any instance disabled in our hierarchy and return that instance
        for (int i = 0; i < _pool.Count; i++)
        {
            if (!_pool[i].activeInHierarchy)
            {
                return _pool[i];
            }
        }
        // if all of them are being used
        return CreateInstance();
    }
    // its static because i want to access these methods without having a reference to object
    public static void ReturnToPool(GameObject instance)
    {
        // its not active by default
        instance.SetActive(false);
    }
    public static IEnumerator ReturnToPoolWithDelay(GameObject instance, float delay)
    {
        yield return new WaitForSeconds(delay);
        instance.SetActive(false);
    }
}
