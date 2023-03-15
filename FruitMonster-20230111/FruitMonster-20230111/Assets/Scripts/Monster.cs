using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

    private float rotationSpeed = 100f;

    // Update is called once per frame
    void Update() {
        if(GameManager.instance.IsGamePaused() || GameManager.instance.IsGameOver()) {
            return;
        }
        transform.Rotate(Vector3.forward, rotationSpeed*Time.deltaTime);
    }

    public void IncrementSpeed() {
        rotationSpeed += 20;
    }
}
