using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball_behaviour : MonoBehaviour {

    public Light point_light;
    public SphereCollider safe_zone;

    public float time_to_fade = 10f;

    public float max_radius;
    public float min_radius;

    float time_count; 

    // Use this for initialization
    void Start () {

        point_light.range = max_radius;
        time_count = 0;

        safe_zone.radius = max_radius * 5;

        IsFading = true; 

    }

    bool IsFading; 
    // Update is called once per frame
    void Update () {

        if (point_light.range > min_radius)
        {
            time_count += Time.deltaTime;

            float advancement = time_count / time_to_fade;
            
            point_light.range = max_radius - (max_radius - min_radius) * advancement;

            safe_zone.radius = point_light.range * 5 - 10;
        }
        else
        {
            IsFading = false; 
        }
    }
}
