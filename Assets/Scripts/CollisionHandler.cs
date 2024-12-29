using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public float levelLoadDelay = 1f;

    [SerializeField] AudioClip[] audioClip;
    [SerializeField] ParticleSystem successParticle;
    [SerializeField] ParticleSystem crashParticle;
    AudioSource audioSource;

    bool isTransitioning = false;
    bool collisionDisable = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        isTransitioning = false;

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
        // todo add sfx upon succes
        // todo add particle effect upon succes
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        Invoke("NextLevel", levelLoadDelay);
        successParticle.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(audioClip[1]);

    }
    void StartCrashSequence()
    {
        // todo add sfx upon crash
        // todo add particle effect upon crash
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
        crashParticle.Play();
        audioSource.Stop();
        audioSource.PlayOneShot(audioClip[0]);
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
    void ReloadLevel()
    {
        /* SceneManager.LoadScene("Sandbox", LoadSceneMode.Single);*/ //Code untuk mengulang scene tetapi tidak tumpang tindih dengan scene lain karena menggunakan single
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    

}
