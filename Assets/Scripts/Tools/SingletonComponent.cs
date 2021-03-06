using UnityEngine;
using System.Collections;

public class SingletonComponent<T> : MonoBehaviour where T : MonoBehaviour {
	private static T instance;
	protected bool isShuttingDown;

	public static T Instance {
		get {
			if (instance == null) {
				instance = (T) FindObjectOfType(typeof(T));
			}
		
			return instance;
		}
	}

	void OnApplicationQuit() {
		isShuttingDown = true;
	}
	
}