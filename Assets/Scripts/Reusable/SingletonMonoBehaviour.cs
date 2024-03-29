﻿using UnityEngine;

/// <summary>
/// Be aware this will not prevent a non singleton constructor
///   such as `T myT = new T();`
/// To prevent that, add `protected T () {}` to your singleton class.
/// 
/// As a note, this is made as MonoBehaviour because we need Coroutines.
/// Version 1.3
/// </summary>
public abstract class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour{
	private static T _instance;

	protected static bool Active {
		get {
			return _instance != null;
		}
	}

	protected virtual bool DestroyOnLoad{
		get{
			return true;
		}
	}

	public static T Instance{
		get{
            if (_instance == null){
                _instance = (T) FindObjectOfType(typeof(T));

                if ( FindObjectsOfType(typeof(T)).Length > 1 ) {
                    Debug.LogError("[SingletonMonoBehaviour] Something went really wrong " +
                        " - there should never be more than 1 singletonMonoBehaviour!" +
                        " Reopening the scene might fix it.");
                    return _instance;
                }
				if (_instance == null){
					GameObject singleton = new GameObject();
					_instance = singleton.AddComponent<T>();
					singleton.name = "(singleton) "+ typeof(T);

                    if (!(_instance as SingletonMonoBehaviour<T>).DestroyOnLoad)
						DontDestroyOnLoad(singleton);
				}
			}

			return _instance;
		}
	}

	public static T I{
		get{
			return Instance;
		}
	}

	public void Close () {
		if (_instance != null)
			_instance = null;
	}

	/// <summary>
	/// When Unity quits, it destroys objects in a random order.
	/// In principle, a Singleton is only destroyed when application quits.
	/// If any script calls Instance after it have been destroyed, 
	///   it will create a buggy ghost object that will stay on the Editor scene
	///   even after stopping playing the Application. Really bad!
	/// So, this was made to be sure we're not creating that buggy ghost object.
	/// </summary>
	protected virtual void OnDestroy () 
    {
		//applicationIsQuitting = true;        
          if (_instance != null && !(_instance as SingletonMonoBehaviour<T>).DestroyOnLoad)
            _instance = null;
    }
}
