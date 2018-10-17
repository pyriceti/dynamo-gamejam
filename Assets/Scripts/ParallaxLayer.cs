using UnityEngine;

[ExecuteInEditMode]
public class ParallaxLayer : MonoBehaviour {
    public float ParallaxFactor;

    public void Move(float delta) {
        Vector3 newPos = transform.localPosition;
        newPos.x -= delta * ParallaxFactor;
        transform.localPosition = newPos;
    }
}