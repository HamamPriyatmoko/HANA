using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class Movement : MonoBehaviour
{
    // Parameters - for tunning, typically set in the editor
    // Cache - e.g references for readibility or speed
    // State - private instance (member) variables

    
    public float rotationThrust = 100f;
    public float mainThrust = 100f;
    [SerializeField] AudioClip audioClip;
    //untuk score
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI nameText;
    public int score;
    /*public bool isGameOver = false;*/

    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] ParticleSystem leftThrustParticle;
    [SerializeField] ParticleSystem rightThrustParticle;

    Rigidbody rb;
    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();

        //kondisi saat pertama kali mulai skor 0
        nameText.text = "Name: " + Scene1Manager.Instance.namaInput.text;
        score = 0;
        scoreText.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
        // Perbarui teks skor di UI
        scoreText.text = "Score: " + score;
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.W))
        {
            StartTrusthing();
            
        }
        else
        {
            StopTrusthing();
        }
    }
    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            LeftRotation();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RightRotation();
        }
        else
        {
            StopRotation();
        }
    }
    void StartTrusthing()
    {
        rb.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
        if (!audioSource.isPlaying)
        {
            /*audioSource.Play();*/ //Ini berfungsi jika hanya mempunyai/menghandle satu audio clip saja
            audioSource.PlayOneShot(audioClip);
        }
        if (!mainEngineParticle.isPlaying)
        {
            mainEngineParticle.Play();
        }
    }
    void StopTrusthing()
    {
        audioSource.Stop();
        mainEngineParticle.Stop();
    }
    void RightRotation()
    {
        ApplyRotation(-rotationThrust);
        if (!leftThrustParticle.isPlaying)
        {
            leftThrustParticle.Play();
        }
    }
    void LeftRotation()
    {
        ApplyRotation(rotationThrust);
        if (!rightThrustParticle.isPlaying)
        {
            rightThrustParticle.Play();
        }
    }
    void StopRotation()
    {
        rightThrustParticle.Stop();
        leftThrustParticle.Stop();
    }
    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationThisFrame);
        rb.freezeRotation = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        // Periksa apakah objek yang mengenai adalah Objek Poin
        if (other.gameObject.CompareTag("Poin"))
        {
            // Hancurkan objek ini
            Destroy(other.gameObject);
            // Tambah skor
            score += 10;

        }
    }

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    // Variabel untuk skor
    // public int score = 0;
    // public Text scoreText; // Referensi ke UI Text untuk menampilkan skor

    //public void AddPoints(int points)
    //{
    //    // Tambahkan poin ke skor
    //    score += points;

    //    // Perbarui tampilan skor
    //    if (scoreText != null)
    //    {
    //        scoreText.text = "Score: " + score;
    //    }
    //}


}
