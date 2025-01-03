using System.Collections;
using System.Collections.Generic;
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


    public void SavePlayerName()
    {
        //  MainManager.Instance.SavePlayerNameData();
    }

    public void LoadPlayerName()
    {
        // MainManager.Instance.LoadScoreData();
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(1);

    }
}
