using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    private Transform cam;
    private Rigidbody rb;
    private float mouseSensitivy = 10.0f;
    private float mouseLerpSpeed = 10.0f;

    void Awake() {
        cam = Camera.main.transform;
        rb = GetComponent<Rigidbody>();
    }

    
    // Use this for initialization
    void Start() {
        if (!isLocalPlayer) {
            Destroy(transform.Find("MainCamera"));
            Destroy(this);
        }
    }

    float curVertLook;
    float curHorizLook;

    // Update is called once per frame
    void Update() {

        float targetHoriz = Input.GetAxisRaw("Mouse X") * mouseSensitivy;
        curVertLook -= Input.GetAxisRaw("Mouse Y") * mouseSensitivy;
        curVertLook = Mathf.Clamp(curVertLook, -90.0f, 90.0f);

        Quaternion targetVert = Quaternion.Euler(curVertLook, 0.0f, 0.0f);
        cam.localRotation = Quaternion.Lerp(cam.localRotation, targetVert, mouseLerpSpeed * Time.deltaTime);

        curHorizLook = Mathf.Lerp(curHorizLook, targetHoriz, mouseLerpSpeed * Time.deltaTime);

        transform.Rotate(0.0f, curHorizLook, 0.0f);

        if (Input.GetButtonDown("Jump")) {
            // jump here
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            //Application.
        }

    }

    void FixedUpdate() {

        float inputX = Input.GetAxisRaw("Horizontal");
        float inputY = Input.GetAxisRaw("Vertical");

        Vector2 input = new Vector2(inputX, inputY);
        if (input.sqrMagnitude > 1.0f) {
            input.Normalize();
        }
        Vector3 xzforward = Vector3.Cross(Vector3.up, -cam.right).normalized;
        rb.velocity = (input.x * cam.right + input.y * xzforward) * 5.0f;
    }
}
