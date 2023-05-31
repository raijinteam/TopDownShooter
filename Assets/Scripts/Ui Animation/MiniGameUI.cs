using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameUI : MonoBehaviour
{
    [SerializeField] private GameObject miniGamesObjects;
    [SerializeField] private Camera miniGameCamera;
    [SerializeField] private Button btn_MiniGameStart;

    private void OnEnable()
    {
        miniGamesObjects.SetActive(true);
        miniGameCamera.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        miniGamesObjects.SetActive(false);
        miniGameCamera.gameObject.SetActive(false);
        MiniGameManager.instance.IsMiniGameStart = false;
        btn_MiniGameStart.gameObject.SetActive(true);
    }

    public void OnClick_StartMiniGame()
    {
        MiniGameManager.instance.IsMiniGameStart = true;
        btn_MiniGameStart.gameObject.SetActive(false);
    }
}
