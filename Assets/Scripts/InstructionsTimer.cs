using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstructionsTimer : MonoBehaviour
{
    public float lifetime = 5f;
    void Update ()
    {
        lifetime -= Time.deltaTime;
        if (lifetime <= 0) {
            Destroy (gameObject);
        }
    }
}
