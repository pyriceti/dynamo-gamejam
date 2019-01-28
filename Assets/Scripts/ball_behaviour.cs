using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ball_behaviour : MonoBehaviour
{
    public Light point_light;
    public SphereCollider safe_zone;

    public float time_to_fade = 10f;

    public float max_radius;
    public float min_radius;

    float time_count;

    bool IsFading;

    private void Start()
    {
        if (point_light)
            point_light.range = max_radius;
        
        time_count = 0;

        if (safe_zone)
            safe_zone.radius = max_radius * 5;

        IsFading = true;
    }

    private void Update()
    {
        if (!point_light) return;
        
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