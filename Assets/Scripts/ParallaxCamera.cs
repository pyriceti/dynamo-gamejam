using System;
using UnityEngine;

[ExecuteInEditMode]
public class ParallaxCamera : MonoBehaviour {
    public delegate void ParallaxCameraDelegate(float deltaMovement);

    public ParallaxCameraDelegate OnCameraTranslate;
    private float oldPosition;

    private void Start() {
        oldPosition = transform.position.x;
    }

    private void Update() {
        if (Math.Abs(transform.position.x - oldPosition) < float.Epsilon) return;

        if (OnCameraTranslate != null) {
            float delta = oldPosition - transform.position.x;
            OnCameraTranslate(delta);
        }

        oldPosition = transform.position.x;
    }
}