using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyItem : MonoBehaviour
{
    public static int keyCount;

    void OnTriggerEnter(Collider collider){
        if (collider.gameObject.name == "PlayerCapsule") {
            keyCount+=1;
            gameObject.SetActive(false);
        }
    }
}
