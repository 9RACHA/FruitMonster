using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour {

    // Velocidad de rotación del monstruo
    private float rotationSpeed = 100f;

    // Update is called once per frame
    void Update() {
        // Comprobamos si el juego está pausado o si el juego ha terminado
        if(GameManager.instance.IsGamePaused() || GameManager.instance.IsGameOver()) {
            return;
        }
        // Rotamos el monstruo alrededor de su eje Z utilizando la velocidad de rotación y el tiempo transcurrido desde el último frame
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    // Método para incrementar la velocidad de rotación del monstruo
    public void IncrementSpeed() {
        rotationSpeed += 20;
    }
}
