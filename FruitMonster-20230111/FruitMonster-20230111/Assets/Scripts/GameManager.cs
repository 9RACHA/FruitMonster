using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    // Instancia única del GameManager
    public static GameManager instance;

    // Prefabs de las frutas disponibles en el juego
    public GameObject[] fruitPrefabs;

    // Transform del monstruo en el juego
    public Transform monster;

    // Transform del carrusel de frutas en el juego
    public Transform fruitCarrousel;

    // Punto de generación de las frutas en el juego
    public Transform fruitSpawnPoint;

    // Sonido de fallo al perder una vida
    public AudioClip failAudioClip;

    // Sonido de éxito al capturar una fruta
    public AudioClip winAudioClip;

    // Contador de vidas
    private int lifeCount = 3;

    // Puntuación
    private int score = 0;

    // Bandera de pausa del juego
    private bool paused = false;

    // Bandera de fin de juego
    private bool gameOver = false;

    // Siguiente incremento de velocidad del monstruo
    private int nextSpeedIncrement = 50;

    // Tiempo desde la última captura de fruta
    private float lastCaptureTime = -1;

    // Tiempo en el que se duplica la puntuación
    private float doublePointsTime = 0.8f;

    // Bandera para obtener una vida adicional
    private bool additionalLife = true;

    // Referencia a la fruta actualmente en juego
    private GameObject fruit;

    // Valor temporal para almacenar el tiempo escala anterior
    private float oldTimeScale = 0;

    // Diferencia máxima permitida en ángulo entre el monstruo y la fruta
    private const float MAX_ANGLE_DIFFERENCE = 4f;

    void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start() {
        if(fruitPrefabs == null || fruitPrefabs.Length == 0) {
            Debug.Log("GameManager: La variable fruitPrefabs no está correctamente inicializada");
        }
        if(monster == null) {
            Debug.Log("GameManager: La variable monster no está correctamente inicializada");
        }
        if(fruitCarrousel == null) {
            Debug.Log("GameManager: La variable fruitCarrousel no está correctamente inicializada");
        }
        if(fruitSpawnPoint == null) {
            Debug.Log("GameManager: La variable fruitSpawnPoint no está correctamente inicializada");
        }

        SpawnFruit();
    }

    // Update is called once per frame
    void Update() {
        if(Input.GetKeyDown(KeyCode.P)) {
            float auxTimeScale = Time.timeScale;
            Time.timeScale = oldTimeScale;
            oldTimeScale = auxTimeScale;
            paused = !paused;
        }

        if(paused || gameOver) {
            return;
        }

        if(Input.GetKeyDown(KeyCode.Space)) {
            if(Mathf.Abs(monster.eulerAngles.z - fruitCarrousel.eulerAngles.z) < MAX_ANGLE_DIFFERENCE) {
                CaptureFruit();
            } else {
                LoseLife();
            }
        }
    }

    // Comprueba si el juego está pausado
    public bool IsGamePaused() {
        return paused;
    }

    // Comprueba si el juego ha terminado
    public bool IsGameOver() {
        return gameOver;
    }

    // Captura una fruta con éxito
    void CaptureFruit() {
        GetComponent<AudioSource>().PlayOneShot(winAudioClip);
        score += fruit.GetComponent<Fruit>().points;

        if(Time.time - lastCaptureTime < doublePointsTime) {
            Debug.Log("Doble puntuación");
            score += fruit.GetComponent<Fruit>().points;
            GetComponent<AudioSource>().PlayOneShot(winAudioClip);
        }

        lastCaptureTime = Time.time;

        // Comprueba si se ha alcanzado el umbral de incremento de velocidad
        if(score >= nextSpeedIncrement) {
            // Incrementa la velocidad del monstruo
            monster.GetComponent<Monster>().IncrementSpeed();
            // Fija el siguiente umbral de incremento de velocidad
            nextSpeedIncrement += 50;
        }

        if(additionalLife) {
            if(score >= 100) {
                additionalLife = false;
                lifeCount++;
            }
        }

        Destroy(fruit);
        SpawnFruit();
    }

    // Pierde una vida
    void LoseLife() {
        GetComponent<AudioSource>().PlayOneShot(failAudioClip);
        lifeCount--;
        Debug.Log("Vidas " + lifeCount);
        if(lifeCount <= 0) {
            gameOver = true;
            oldTimeScale = Time.timeScale;
            Time.timeScale = 0;
        }
    }

    // Genera una nueva fruta en el juego
    void SpawnFruit() {
        Vector3 eulerAngles = fruitCarrousel.eulerAngles;
        eulerAngles.z = Random.Range(0, 360);
        fruitCarrousel.eulerAngles = eulerAngles;

        int fruitIndex = Random.Range(0, fruitPrefabs.Length);

        fruit = Instantiate(fruitPrefabs[fruitIndex], fruitSpawnPoint.position, Quaternion.identity);
    }

    // Muestra la puntuación y las vidas en la interfaz gráfica del juego
    void OnGUI() {
        GUIStyle myStyle = new GUIStyle();
        myStyle.fontSize = 32;
        myStyle.onNormal.textColor = Color.white;
        GUI.Label(new Rect(10, 10, 100, 30), "Vidas: " + lifeCount, myStyle);
        GUI.Label(new Rect(10, 40, 100, 30), "Score: " + score, myStyle);
    }
}
