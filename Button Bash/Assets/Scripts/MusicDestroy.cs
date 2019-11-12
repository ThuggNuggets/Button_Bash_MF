using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicDestroy : MonoBehaviour
{
    private int number;
    // Start is called before the first frame update
    private void Awake()
    {
		gameObject.SetActive(true);
		number = 0;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        number++;
        if (number == 3)
        {
			gameObject.SetActive(false);
        }
    }
}
