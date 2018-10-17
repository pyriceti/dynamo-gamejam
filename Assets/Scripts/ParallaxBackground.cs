using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ParallaxBackground : MonoBehaviour {
    public ParallaxCamera ParallaxCamera;

    private readonly List<ParallaxLayer> parallaxLayers = new List<ParallaxLayer>();

    private void Start() {
        if (ParallaxCamera == null)
            ParallaxCamera = Camera.main.GetComponent<ParallaxCamera>();
        if (ParallaxCamera != null)
            ParallaxCamera.OnCameraTranslate += Move;
        SetLayers();
    }

    private void SetLayers() {
        parallaxLayers.Clear();
        for (var i = 0; i < transform.childCount; i++) {
            ParallaxLayer layer = transform.GetChild(i).GetComponent<ParallaxLayer>();

            if (layer == null) continue;

            // layer.name = "Layer-" + i;
            parallaxLayers.Add(layer);
        }
    }

    private void Move(float delta) {
        foreach (ParallaxLayer layer in parallaxLayers) {
            layer.Move(delta);
        }
    }
}