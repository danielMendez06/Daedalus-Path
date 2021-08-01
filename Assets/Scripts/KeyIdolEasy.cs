using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyIdolEasy : MonoBehaviour
{
    public GameObject WinLight;
    public GameObject GameOverMenu;
    public GameObject UICanvas;
    public GameObject WinSound;

    void OnTriggerEnter(Collider collider){
        if (collider.gameObject.name == "PlayerCapsule" && KeyItem.keyCount>3) {
            WinLight.SetActive(true);
            WinSound.SetActive(true);
            GameOverMenu.SetActive(true);
            UICanvas.SetActive(false);
            ScoreEasy.UpdateHighscore();
            Time.timeScale = 0f;
        }
    }
}