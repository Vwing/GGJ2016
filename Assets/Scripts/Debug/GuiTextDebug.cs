using UnityEngine;
using System.Collections;

public class GuiTextDebug : MonoBehaviour
{
    //public string output = "";
    //public string stack = "";
    public float scrollSpeed = 10f;
    public bool DummyLogging = false;

    private TextMesh txt;
    private Vector3 startPos;
    private string myText;
    private float debugTimer = 0f;
    void Awake()
    {
        txt = GetComponent<TextMesh>();
        startPos = transform.position;
        myText = "";
        if(DummyLogging)
            StartCoroutine("DebugLogging");
    }
    IEnumerator DebugLogging()
    {
        while (true)
        {
            Debug.Log("Debugging");
            yield return new WaitForSeconds(1f);
        }
    }
    void Update()
    {
        float ax = Input.GetAxisRaw("Mouse Y");
        if (ax > 0)
            transform.Translate(Vector3.up * scrollSpeed * Time.deltaTime);
        else if (ax < 0)
            transform.Translate(Vector3.down * scrollSpeed * Time.deltaTime);
        if (Input.GetMouseButtonUp(0) || ax != 0)
            debugTimer = 0f;
        if (!isActive && Input.GetMouseButton(0))
            ActivateTextAfterTimer();
        if (isActive && Input.GetMouseButton(0))
            DeactivateTextAfterTimer();
    }
    bool isActive = false;
    void ActivateTextAfterTimer()
    {
        debugTimer += Time.deltaTime;
        if (debugTimer > 1f)
            ToggleText(true);
    }
    void DeactivateTextAfterTimer()
    {
        debugTimer += Time.deltaTime;
        if (debugTimer > 1f)
            ToggleText(false);
    }
    void ToggleText(bool on)
    {
        isActive = !isActive;
        debugTimer = 0f;
        if (on)
        {
            transform.localPosition = startPos;
            txt.text = myText;
        }
        else
        {
            //myText = txt.text;
            txt.text = "";
        }
    }
    void OnEnable()
    {
        Application.logMessageReceived += HandleLog;
    }
    void OnDisable()
    {
        Application.logMessageReceived -= HandleLog;
    }
    void HandleLog(string logString, string stackTrace, LogType type)
    {
        myText += logString + "\n";
    }
}