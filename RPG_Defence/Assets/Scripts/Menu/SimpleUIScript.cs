using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleUIScript : MonoBehaviour
{
    [SerializeField] private Button LoadButton;

    private void OnEnable()
    {
        LoadButton.interactable = SaveSystem.HasSavedData();
    }

    public void StartGame(bool isNewGame)
    {
        GameData.Instance.isLoadingGame = !isNewGame;
        GameData.Instance.LoadScene(1);
    }
}
