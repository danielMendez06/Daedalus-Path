using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyIdolMedium : MonoBehaviour
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
            Time.timeScale = 0f;
            ScoreMedium.UpdateHighscore();
        }
    }
}