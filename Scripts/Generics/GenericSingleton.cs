using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            // Check if an instance already exists
            if (instance == null)
            {
                // If not, try to find an instance in the scene
                instance = FindObjectOfType<T>();

                // If no instance is found, create a new GameObject with the singleton script attached
                if (instance == null)
                {
                    GameObject singletonObject = new GameObject(typeof(T).Name);
                    instance = singletonObject.AddComponent<T>();
                }

                // Ensure the instance persists between scene changes
                //DontDestroyOnLoad(instance.gameObject);
            }

            return instance;
        }
    }

    protected virtual void Awake()
    {
        // Check if another instance of the same type already exists
        //if (instance != null && instance != this && instance.gameObject != gameObject)
        //{
        //    Debug.Log("Action start");
        //    // Destroy this instance to enforce the singleton pattern
        //    Destroy(gameObject);
        //}
    }
}
