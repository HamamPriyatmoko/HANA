using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.UIElements;

public class SpinObstacle : MonoBehaviour
{
    // Update is called once per frame
    /*Rigidbody rb;*/
    public float xrandom = 1;
    public float yrandom = 1;
    public float zrandom = 0;
    float speedRandom = 0f;
    float speedRotation = 50f;

    [SerializeField] ParticleSystem crashParticle;
    Vector3 meteorMovement;
    
    private void Start()
    {
        /*rb = GetComponent<Rigidbody>();*/
        meteorMovement = new Vector3(xrandom, yrandom, zrandom);
    
        speedRandom = Random.Range(2, 5);
    }
    private void Update()
    {
        /*rb.AddForce(new Vector3(7, 0, 0));*/
        transform.Translate(meteorMovement * speedRandom * Time.deltaTime, Space.World);

        transform.Rotate(Vector3.forward * speedRotation * Time.deltaTime);
    }
    /*private void OnBecameInvisible()
    {
        *//*enabled = true;*//*
        Destroy(gameObject);
    }*/

    /*private void OnTriggerExit(Collider other)
    {
        Destroy(gameObject);
        Debug.Log("Sudah dihancurkan");
    }*/

    private void OnCollisionEnter(Collision collision)
    {
         Destroy(gameObject);        
    }
}
