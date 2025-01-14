using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using TMPro;
using System;

public class CollisionHandler : MonoBehaviour
{
    public float levelLoadDelay = 1f;
    public int score;
    int saveScore;

    [SerializeField] AudioClip[] audioClip;
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem crashParticle;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI yourscoreText;
    AudioSource audioSource;
    public Canvas canvasPopUp;

    bool isTransitioning = false;
    bool collisionDisable = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        isTransitioning = false;
        // Ambil skor dari Scene2Manager
        score = Scene2Manager.Instance.playerScore;

        //kondisi saat pertama kali mulai skor 0
        nameText.text = "Name: " + Scene1Manager.Instance.namaInput.text;

        scoreText.text = "Score: " + score;
        
    }
    private void Update()
    {
        RespondToDebugKeys();
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKey(KeyCode.L))
        {
            NextLevel();
        }
        else if (Input.GetKeyDown(KeyCode.C))
        {
            collisionDisable = !collisionDisable; //toggle collision, inilah metode yang dinamakan mengalihkan
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if (isTransitioning || collisionDisable) { return; } //artinya jika isTransitioning == true maka dia akan return dan tidak boleh melanjutkan statementnya lagi
                                        // atau tidak boleh melangkah lebih jauh eksekusi(running) jadi statement switch tidak akan di eksekusi
        switch (other.gameObject.tag) 
        {
            /*case "Fuel":
                Debug.Log("Your Fuel is Full");
                break;*/
            case "Finish":
                StartSuccessSequence();
                break;
            case "Friendly":
                Debug.Log("This thing is friendly");
                break;
            default:
                StartCrashSequence();
                /*Invoke("ReloadLevel", .5f);*/ //karna waktunya second maka menggunakan float atau f 
                break;
        }

        
    }
    private void OnTriggerEnter(Collider other)
    {
        // Periksa apakah objek yang mengenai adalah Objek Poin
        if (other.gameObject.CompareTag("Poin"))
        {
            // Hancurkan objek ini
            Destroy(other.gameObject);
            // Tambah skor
            AddScore(10);
        }
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;

        // Update skor di Scene2Manager
        Scene2Manager.Instance.AddScore(scoreToAdd);

        scoreText.text = "Score: " + score;
    }
    /*private Vector3 start_Position;
    private Vector3 start_Rotation;
    private void Start()
    {
        start_Position = transform.position;
        start_Rotation = transform.eulerAngles;     //Ini adalah code untuk spawn player

    }*/
    /*void SpawnRocket()
    {
        transform.position = start_Position;
        transform.eulerAngles = start_Rotation;
    }*/
    void StartSuccessSequence()
    {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        
        // Simpan skor ke file JSON
        Scene2Manager.Instance.SaveScoreToJson(Scene1Manager.Instance.namaInput.text);

        Invoke("NextLevel", levelLoadDelay);
        successParticle.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(audioClip[1]);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        PlayerPrefs.SetInt("PlayerScore", score);
        saveScore = PlayerPrefs.GetInt("PlayerScore",0);
        yourscoreText.text = "Your Score : " + saveScore.ToString(); 
        /*// Simpan skor ke file JSON
        Scene2Manager.Instance.SaveScoreToJson(Scene1Manager.Instance.namaInput.text);*/

        /*Invoke("ReloadLevel", levelLoadDelay);*/
        crashParticle.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(audioClip[0]);
        if (canvasPopUp != null)
        {
            canvasPopUp.gameObject.SetActive(true);
        }
    }
    void NextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextsceneindex = currentSceneIndex + 1;
        if (nextsceneindex == SceneManager.sceneCountInBuildSettings) 
        {
            nextsceneindex = 0;
        }
        
        /*SceneManager.LoadScene(currentSceneIndex + 1, LoadSceneMode.Single);*/    //load scene mode single agar scene sebelumnya diclose dan memuat scene baru / selanjutnya
        SceneManager.LoadScene(nextsceneindex);
    }
    public void ReloadLevel()
    {
        Scene2Manager.Instance.ResetScore();
        /* SceneManager.LoadScene("Sandbox", LoadSceneMode.Single);*/ //Code untuk mengulang scene tetapi tidak tumpang tindih dengan scene lain karena menggunakan single
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    public void BackToMenu() 
    {
        Scene2Manager.Instance.ResetScore();
        SceneManager.LoadScene(0);
    }
}
