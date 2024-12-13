using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameStartupManager : MonoBehaviour
{
    public static GameStartupManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        StartCoroutine(CreateUserAndStartGame());
    }

    IEnumerator CreateUserAndStartGame()
    {
        yield return null; // Add any initialization logic here

        GameStart gameStart = FindObjectOfType<GameStart>();
        gameStart.StartGame();
    }

    public void StartGame()
    {
        // Implement your game starting logic here
    }


   
}
