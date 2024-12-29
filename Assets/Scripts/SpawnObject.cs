using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour
{
    
    /*public float minaxisX = 0f;
    public float maxaxisX = 0f;
    public float minaxisY = 0f;
    public float maxaxisY = 0f;
    public float axisZ = 0f;*/

    public GameObject obstacle;

    private void Start()
    {
        InvokeRepeating("ObjectSpawn", 1f, 3.5f);
        
    }

    void ObjectSpawn()
    {
        /*Quaternion objekRotation = Quaternion.Euler(0, 0, Random.Range(-2,2));*/

       /* Vector3 spawnPos = new Vector3(Random.Range(minaxisX, -7f), Random.Range(minaxisY, 8.8f), axisZ);*/
        Instantiate(obstacle, gameObject.transform.position, obstacle.transform.rotation);
    }
}
