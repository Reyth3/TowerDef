using UnityEngine;
using System.Collections;

public class CameraMovement : MonoBehaviour {

    private Camera cam;
    private float minZoom, maxZoom, movementSpeed;
    private RectOffset bounds;
	// Use this for initialization
	void Start () {
        cam = GetComponent<Camera>();
        minZoom = cam.orthographicSize * 0.5f;
        maxZoom = cam.orthographicSize * 2.25f;
        movementSpeed = 17f;
        bounds = new RectOffset(-20, 20, 20, -20);
	}
	
	// Update is called once per frame
	void Update () {
        var v = Input.GetAxis("Vertical");
        var h = Input.GetAxis("Horizontal");
        var s = Input.GetAxis("Mouse ScrollWheel");
        if (v != 0f ) transform.position += new Vector3(0f, v * movementSpeed * Time.deltaTime);
        if (h != 0f) transform.position += new Vector3(h * movementSpeed * Time.deltaTime, 0f);
        if (s != 0f) cam.orthographicSize = Mathf.Clamp(cam.orthographicSize -= s * movementSpeed * 18f * Time.deltaTime, minZoom, maxZoom);
        var pos = transform.position;
        transform.position = new Vector3(Mathf.Clamp(pos.x, bounds.left, bounds.right), Mathf.Clamp(pos.y, bounds.bottom, bounds.top), pos.z);
    }
}
