using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class Scene2Manager : MonoBehaviour
{
    public static Scene2Manager Instance;
    public int playerScore = 0;

    private string jsonFilePath;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Tentukan lokasi file JSON
        jsonFilePath = Application.dataPath + "/leaderboard.json";
    }

    public void AddScore(int score)
    {
        playerScore += score;
    }

    public void ResetScore()
    {
        playerScore = 0;
    }

    public void SaveScoreToJson(string playerName)
    {
        // Buat leaderboard baru jika file tidak ada
        LeaderboardData leaderboardData = new LeaderboardData();

        if (File.Exists(jsonFilePath))
        {
            // Baca file JSON yang sudah ada
            string jsonContent = File.ReadAllText(jsonFilePath);
            leaderboardData = JsonUtility.FromJson<LeaderboardData>(jsonContent);
        }

        // Tambahkan entri baru
        leaderboardData.leaderboards.Add(new LeaderboardEntry(playerName, playerScore));

        // Serialize ke JSON
        string json = JsonUtility.ToJson(leaderboardData, true);

        // Tulis ke file JSON
        File.WriteAllText(jsonFilePath, json);
        Debug.Log($"Score saved to JSON: {json}");
    }

    public void LoadLeaderboardFromJson()
    {
        // Cek apakah file JSON ada
        if (File.Exists(jsonFilePath))
        {
            string jsonContent = File.ReadAllText(jsonFilePath);

            // Deserialize JSON ke dalam objek
            LeaderboardData leaderboardData = JsonUtility.FromJson<LeaderboardData>(jsonContent);

            // Tampilkan data leaderboard di konsol untuk debugging
            foreach (var entry in leaderboardData.leaderboards)
            {
                Debug.Log($"Player: {entry.playerName}, Score: {entry.score}");
            }
        }
        else
        {
            Debug.LogWarning("Leaderboard JSON file not found!");
        }
    }

    [System.Serializable]
    public class LeaderboardEntry
    {
        public string playerName;
        public int score;

        public LeaderboardEntry(string playerName, int score)
        {
            this.playerName = playerName;
            this.score = score;
        }
    }

    [System.Serializable]
    public class LeaderboardData
    {
        public List<LeaderboardEntry> leaderboards = new List<LeaderboardEntry>();
    }
}
