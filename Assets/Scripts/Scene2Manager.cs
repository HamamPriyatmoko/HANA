using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene2Manager : MonoBehaviour
{
    public static Scene2Manager Instance;
    public int playerScore = 0;
    // Start is called before the first frame update
    private void Awake()
    {
        // Jika instance sudah ada, hancurkan objek baru
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Tetapkan instance dan jangan hancurkan objek ini saat berpindah scene
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    // Tambahkan metode untuk mengupdate skor
    public void AddScore(int score)
    {
        playerScore += score;
    }
}
