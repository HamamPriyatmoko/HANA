using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene1Manager : MonoBehaviour
{
    public TMP_InputField namaInput;

    [System.Serializable]
    public class PlayerName
    {
        public string name;

        public PlayerName(string name)
        {
            this.name = name;
        }
    }

    [System.Serializable]
    public class Leaderboard
    {
        public List<PlayerName> leaderboards = new List<PlayerName>();
    }

    private string GetFilePath()
    {
        // Menggunakan Application.dataPath
        return Application.dataPath + "/filePlayerName.json";
    }

    public void SavePlayerName()
    {
        string namaplayer = namaInput.text;

        // Validasi input kosong
        if (string.IsNullOrWhiteSpace(namaplayer))
        {
            Debug.LogWarning("Nama tidak boleh kosong!");
            return;
        }

        // Lokasi file
        string path = GetFilePath();
        Leaderboard data = new Leaderboard();

        // Jika file JSON sudah ada, baca data lama
        if (File.Exists(path))
        {
            string existingJson = File.ReadAllText(path);
            data = JsonUtility.FromJson<Leaderboard>(existingJson);
        }

        // Tambahkan nama baru ke leaderboard
        data.leaderboards.Add(new PlayerName(namaplayer));

        // Simpan ke file JSON
        string json = JsonUtility.ToJson(data, true); // Format JSON agar lebih mudah dibaca
        File.WriteAllText(path, json);

        Debug.Log("Data berhasil disimpan ke: " + path);
        Debug.Log("Isi JSON: " + json);
    }

    public void LoadPlayerName()
    {
        string path = GetFilePath();

        // Cek apakah file JSON ada
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            // Baca data leaderboard dari file
            Leaderboard data = JsonUtility.FromJson<Leaderboard>(json);

            // Jika leaderboard memiliki data, ambil nama terakhir
            if (data.leaderboards.Count > 0)
            {
                namaInput.text = data.leaderboards[data.leaderboards.Count - 1].name;
                Debug.Log("Nama terakhir berhasil dimuat: " + namaInput.text);
            }
            else
            {
                Debug.Log("Leaderboard kosong!");
            }
        }
        else
        {
            Debug.LogWarning("File JSON tidak ditemukan!");
        }
    }

    public void PlayGame()
    {
        if (!string.IsNullOrWhiteSpace(namaInput.text))
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            Debug.LogWarning("Tolong isi namanya terlebih dahulu!");
        }
    }
}
