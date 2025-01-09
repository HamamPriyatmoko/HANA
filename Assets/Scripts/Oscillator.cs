using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Oscillator : MonoBehaviour
{
    Vector3 StartPosition;
    [SerializeField] Vector3 movementVector; //Seberapa jauh akan melangkah bisa 10,0,0 atau 20,0,0
    [SerializeField] float movementFactor;

    [SerializeField] float period = 2f;
    // Start is called before the first frame update
    void Start()
    {
        StartPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (period == 0) { return; }
        float cycle = Time.time / period; //Terus berkembang

        const float tau = Mathf.PI * 2; //Memberikan nilai full lingkaran yaitu 6.28
        float rawSinWave = Mathf.Sin(cycle * tau); //Memberikan hasil nilai radian antara -1 and 1

        movementFactor = (rawSinWave + 1)/2; // Agar memberikan nilai antara 0 dan 1

        Vector3 offset = movementVector * movementFactor;
        transform.position = StartPosition + offset;
    }
}
