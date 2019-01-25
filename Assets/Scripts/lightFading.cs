using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class lightFading : MonoBehaviour
{
    public float duration = 10f;
    Light lt;

    //light intensity factor
    public float intensityFactor = 0.5f;
    public float rangeMax = 8f;
    public float rangeMin= 4f;



    void Start()
    {
        lt = GetComponent<Light>();
    }
    void Update()
    {

        
    }
}