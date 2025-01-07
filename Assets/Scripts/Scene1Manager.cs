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

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public class PlayerName 
    {
        public string name;
    }

    public void SavePlayerName()
    {
        // Save Nama Player ke Json
        PlayerName data = new PlayerName();
        data.name = namaInput.text;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.dataPath + "/filePlayerName.json", json);
    }

    public void LoadPlayerName()
    {
        // Mendapatkan nama Player dari file json ke NamaInput.Text
        string path = Application.dataPath + "/filePlayerName.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            PlayerName data = JsonUtility.FromJson<PlayerName>(json);
            namaInput.text = data.name;
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);

    }
}
