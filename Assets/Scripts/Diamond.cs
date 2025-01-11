using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        // Periksa apakah objek yang mengenai adalah Player
        if (other.gameObject.CompareTag("Player"))
        {
            // Hancurkan objek ini
            Destroy(gameObject);
        }
    }
}
