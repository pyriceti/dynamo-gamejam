using UnityEngine;

public class camera_controler : MonoBehaviour {
    public float dampTime = 0.15f;
    private Vector3 velocity = Vector3.zero;
    public Transform Target;
    public Vector3 Offset = new Vector3(1, 1, 0f);

    private new Camera camera;

    private void Start() {
        camera = GetComponent<Camera>();
    }

    void Update() {
        Vector3 point = camera.WorldToViewportPoint(Target.position);
        Vector3 delta = Target.position - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z)) + Offset;
        Vector3 destination = transform.position + delta;
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);
    }

    public void SetXOffset(float xOffset) {
        Offset = new Vector3(xOffset, 1, 0f);
    }
}