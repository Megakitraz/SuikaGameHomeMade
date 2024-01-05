using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float cooldownFinnish;
    public float cooldownDrop;

    public float limit;

    [SerializeField] private Transform _limitBucket;

    private void Awake()
    {
        Instance = this;
        limit = _limitBucket.position.y;
    }

    public void GameOver()
    {
        Debug.Log("Game Over");
        SceneManager.LoadScene("GameScene");
    }
    


}
