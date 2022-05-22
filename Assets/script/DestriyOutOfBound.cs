using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestriyOutOfBound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DestroyObject();
    }
    void DestroyObject()
    {
        // game object will destroy in 7 seconds after loading the object
        Destroy(gameObject,7);
    }
}
