using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    
    public static T Instance
    {
        get{
            if (_instance == null){
                _instance = GameObject.FindObjectOfType<T>();

                if (_instance == null){
                    GameObject singleton = new GameObject();
                    _instance = singleton.AddComponent<T>();
                }
            }
            return _instance;   
        }
    }

    private void Awake() {
        if (_instance != null){
            Destroy(this);
        }
        else{
            _instance = this as T;
            DontDestroyOnLoad(_instance);
        }
    }
}
