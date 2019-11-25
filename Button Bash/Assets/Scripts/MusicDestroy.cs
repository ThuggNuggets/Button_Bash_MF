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
		number = 0;
        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        number++;
        if (number % 4 == 0)
        {
            gameObject.SetActive(false);
            number = 0;
        }
        //if (number == 2 && turnOn)
        //{
        //    gameObject.SetActive(true);
        //    turnOn = false;
        //    number = 1;
        //}
    }
}
