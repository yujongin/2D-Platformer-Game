using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public class LevelInfo
{
    public string LevelName;
    public Sprite LevelThumb;
    public GameObject LevelPrefab;
}

public class LevelManager : MonoBehaviour
{
    public GameObject SelectedPrefab;
    public List<LevelInfo> levels;
    private static LevelManager instance;
    public static LevelManager Instance
    {
        get { return instance; }
        private set
        {
            instance = value;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartLevel(int index){
        SelectedPrefab = levels[index].LevelPrefab;
        SceneManager.LoadScene("GameScene");
    }
}
