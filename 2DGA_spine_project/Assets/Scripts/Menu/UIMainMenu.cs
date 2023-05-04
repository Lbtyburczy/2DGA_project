using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] Button newGameButton;

    // Start is called before the first frame update
    void Start()
    {
        newGameButton.onClick.AddListener(StartNewGame);
    }

    private void StartNewGame() {
        ScenesManager.Instance.LoadScene(ScenesManager.Scene.Level01);
    }
}
