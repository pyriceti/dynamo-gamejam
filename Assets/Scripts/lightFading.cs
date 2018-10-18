using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class lightFading : MonoBehaviour
{
    public float duration = 1.0F;
    public Light lt;

    //light intensity factor
    public float intensityFactor = 0.5f;

    public Rigidbody orb;

    public Material orbMat;

    void Start()
    {
        lt = GetComponent<Light>();
    }
    void Update()
    {
        //float phi = Time.time / duration * 2 * Mathf.PI;
        //float amplitude = Mathf.Cos(phi) * intensityFactor + intensityFactor;
        //lt.intensity = amplitude;
        if (orb.IsSleeping() && lt.intensity >= 0f)
        {
            lt.intensity = lt.intensity - intensityFactor;
           
            if(lt.intensity <= 1f)
            {

                UnityEditor.SerializedObject halo = new SerializedObject(orb.GetComponent("Halo"));
                if (halo.FindProperty("m_Size").floatValue >= 0.0f)
                {
                    halo.FindProperty("m_Size").floatValue = halo.FindProperty("m_Size").floatValue - 0.001f;
                    halo.FindProperty("m_Enabled").boolValue = true;
                    halo.FindProperty("m_Color").colorValue = Color.white;
                    halo.ApplyModifiedProperties();
                }
                else
                {
                    halo.FindProperty("m_Enabled").boolValue = false;
                    halo.ApplyModifiedProperties();
                }

            }

        }

        else if(!orb.IsSleeping() && lt.intensity < 5f)
        {

            lt.intensity = lt.intensity + intensityFactor;

            
            UnityEditor.SerializedObject halo = new SerializedObject(orb.GetComponent("Halo"));
            if (halo.FindProperty("m_Size").floatValue < 0.1f)
            {
                halo.FindProperty("m_Size").floatValue = halo.FindProperty("m_Size").floatValue + 0.001f;
                halo.FindProperty("m_Enabled").boolValue = true;
                halo.FindProperty("m_Color").colorValue = Color.white;
                halo.ApplyModifiedProperties();
            }
            
        }
        
    }
}