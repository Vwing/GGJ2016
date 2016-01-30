using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    private Transform cam;
    private Rigidbody rb;
    private float mouseSensitivy = 8.0f;
    private float mouseLerpSpeed = 20.0f;
    private float jumpSpeed = 8.0f;
    private float moveSpeed = 5.0f;

    private bool grounded = false;
    private bool hasLanded = false;

    // Use this for initialization
    void Start() {
        cam = transform.Find("Main Camera");
        rb = GetComponent<Rigidbody>();

        if (!isLocalPlayer) {
            rb.useGravity = false;
            Destroy(cam.GetComponent<Camera>());
            Destroy(cam.GetComponent<AudioListener>());
            Destroy(this);
        }
    }

    float curVertLook;
    float curHorizLook;
    float timeSinceJump = 10f;

    // Update is called once per frame
    void Update() {

        float targetHoriz = Input.GetAxisRaw("Mouse X") * mouseSensitivy;
        curVertLook -= Input.GetAxisRaw("Mouse Y") * mouseSensitivy;
        curVertLook = Mathf.Clamp(curVertLook, -90.0f, 90.0f);

        Quaternion targetVert = Quaternion.Euler(curVertLook, 0.0f, 0.0f);
        cam.localRotation = Quaternion.Lerp(cam.localRotation, targetVert, mouseLerpSpeed * Time.deltaTime);

        curHorizLook = Mathf.Lerp(curHorizLook, targetHoriz, mouseLerpSpeed * Time.deltaTime);

        transform.Rotate(0.0f, curHorizLook, 0.0f);

        if (Input.GetKeyDown(KeyCode.Escape)) {
            // should figure out how to just disconnect instead
            Application.Quit();
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

        timeSinceJump += Time.deltaTime;
        if (Input.GetButtonDown("Jump")) {
            timeSinceJump = 0.0f;
        }

        float newY = rb.velocity.y;
        if (timeSinceJump < 0.25f && grounded && hasLanded) {
            newY = jumpSpeed;
            grounded = false;
            hasLanded = false;
        }

        rb.velocity = (input.x * cam.right + input.y * xzforward) * moveSpeed + newY * Vector3.up;

        // check to see if player is grounded
        Vector3 castStart = transform.position + new Vector3(0.0f, 0.5f, 0.0f);
        RaycastHit info;
        grounded = Physics.SphereCast(castStart, 0.45f, Vector3.down, out info, 0.5f);

    }

    void OnCollisionEnter(Collision c) {
        hasLanded = true;
    }
}
