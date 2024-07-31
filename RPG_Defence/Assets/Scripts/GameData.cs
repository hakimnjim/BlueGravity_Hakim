using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{
    private static GameData instance;
    public static GameData Instance
    {
        get { return instance; }
    }

    [HideInInspector]
    public bool isLoadingGame;
    public int inventoryCount;

    [SerializeField] private List<Item> gameItems;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    public void LoadScene(int id)
    {
        Time.timeScale = 1;
        SceneManager.LoadSceneAsync(id);
    }

    public List<Item> GetItemsList()
    {
        return gameItems;
    }

    public Item GetItemById(int id)
    {
        int index = gameItems.FindIndex(x => x.itemID == id);
        if (index != -1)
        {
            return gameItems[index];
        }
        return null;
    }
}
