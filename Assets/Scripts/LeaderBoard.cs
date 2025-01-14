using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class LeaderBoard : MonoBehaviour
{
    public TMP_Text leaderboardNameDisplay; // Kolom untuk nama pemain
    public TMP_Text leaderboardScoreDisplay; // Kolom untuk skor pemain

    private string jsonFilePath;

    void Start()
    {
        // File JSON akan berada di folder Assets
        jsonFilePath = Application.dataPath + "/leaderboard.json";
        GetLeaderBoardFromJson();
    }

    public void GetLeaderBoardFromJson()
    {
        // Periksa apakah file JSON ada
        if (File.Exists(jsonFilePath))
        {
            // Baca isi file JSON
            string jsonContent = File.ReadAllText(jsonFilePath);

            // Parse isi file ke dalam format leaderboard
            LeaderboardData leaderboardData = JsonUtility.FromJson<LeaderboardData>(jsonContent);

            // Reset tampilan leaderboard
            leaderboardNameDisplay.text = "";
            leaderboardScoreDisplay.text = "";

            // Tampilkan maksimal 10 data leaderboard
            if (leaderboardData.leaderboards.Count > 0)
            {
                // Urutkan leaderboard berdasarkan skor (descending)
                leaderboardData.leaderboards.Sort((a, b) => b.score.CompareTo(a.score));

                // Iterasi maksimal 10 data
                for (int i = 0; i < Mathf.Min(10, leaderboardData.leaderboards.Count); i++)
                {
                    var entry = leaderboardData.leaderboards[i];
                    leaderboardNameDisplay.text += $"{entry.playerName}\n";
                    leaderboardScoreDisplay.text += $"{entry.score}\n";
                }
            }
            else
            {
                leaderboardNameDisplay.text = "No data available.";
                leaderboardScoreDisplay.text = "";
            }
        }
        else
        {
            Debug.LogWarning($"Leaderboard JSON file not found at {jsonFilePath}");
            leaderboardNameDisplay.text = "No data available.";
            leaderboardScoreDisplay.text = "";
        }
    }

    public void AddEntryToJson(string playerName, int score)
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
        leaderboardData.leaderboards.Add(new LeaderboardEntry(playerName, score));

        // Serialize ke JSON
        string updatedJsonContent = JsonUtility.ToJson(leaderboardData, true);

        // Tulis kembali ke file JSON
        File.WriteAllText(jsonFilePath, updatedJsonContent);

        Debug.Log($"Added new entry: {playerName} with score {score}");
        GetLeaderBoardFromJson(); // Refresh leaderboard
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
