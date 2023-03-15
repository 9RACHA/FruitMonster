using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    
    public static GameManager instance;

    public GameObject[] fruitPrefabs;
    public Transform monster;
    public Transform fruitCarrousel;
    public Transform fruitSpawnPoint;

    public AudioClip failAudioClip;
    public AudioClip winAudioClip;
    
    private int lifeCount = 3;
    private int score = 0;
    private bool paused = false;
    private bool gameOver = false;
    private int nextSpeedIncrement = 50;
    private float lastCaptureTime = -1;
    private float doublePointsTime = 0.8f;
    private bool additionalLife = true;
    private GameObject fruit;

    private float oldTimeScale = 0;
    private const float MAX_ANGLE_DIFFERENCE = 4f;

    void Awake() {
        instance = this;
    }
    // Start is called before the first frame update
    void Start() {
        if(fruitPrefabs ==  null || fruitPrefabs.Length == 0) {
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

    public bool IsGamePaused() {
        return paused;
    }

    public bool IsGameOver() {
        return gameOver;
    }

    void CaptureFruit() {
        GetComponent<AudioSource>().PlayOneShot(winAudioClip);
        score += fruit.GetComponent<Fruit>().points;

        if(Time.time - lastCaptureTime < doublePointsTime) {
            Debug.Log("Doble puntuación");
            score += fruit.GetComponent<Fruit>().points;
            GetComponent<AudioSource>().PlayOneShot(winAudioClip);
        }

        lastCaptureTime = Time.time;
        //Comprobamos si hemos pasado una barrera de puntos múltiplo de 50
        if(score >= nextSpeedIncrement) {
            //Incrementamos la velocidad del monstruo
            monster.GetComponent<Monster>().IncrementSpeed();
            //Fijamos el siguiente umbral de incremento de velocidad
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



    void SpawnFruit() {
        Vector3 eulerAngles = fruitCarrousel.eulerAngles;
        eulerAngles.z  = Random.Range(0, 360);
        fruitCarrousel.eulerAngles = eulerAngles;

        int fruitIndex = Random.Range(0, fruitPrefabs.Length);

        fruit = Instantiate(fruitPrefabs[fruitIndex], fruitSpawnPoint.position, Quaternion.identity);
    }

    void OnGUI() {
        GUIStyle myStyle = new GUIStyle(); 
        myStyle.fontSize = 32;
        myStyle.onNormal.textColor = Color.white;
        GUI.Label(new Rect(10, 10, 100, 30), "Vidas: " + lifeCount, myStyle);
        GUI.Label(new Rect(10, 40, 100, 30), "Score: " + score, myStyle);
    }
}
