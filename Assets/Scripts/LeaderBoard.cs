using System.Collections;
using System.Collections.Generic;
using Dan.Main;
using UnityEngine;

public class LeaderBoard : MonoBehaviour
{

    private string publicLeaderboardkey =
        "85b2ae2783cd8399b0e7e39574af6df7d1ddd603f81bb6ffbad98150cf83ed0b";
    public void GetLeaderBoard()
    {
        LeaderboardCreator.GetLeaderboard(publicLeaderboardkey, ((msg) =>
        {

        }));
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
