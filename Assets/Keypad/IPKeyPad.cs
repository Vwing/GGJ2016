using UnityEngine;
using System.Collections;

public class IPKeyPad : MonoBehaviour {
    Transform cam;
    public string theirIP = "";
    public string myIP = "";
    TextMesh theirIPtxt;
    private AudioSource aud;

	void Start () {
        cam = Camera.main.transform;
        aud = GetComponent<AudioSource>();
        myIP = UtilitiesToAdd.GetIP();
        TextMesh myIPtxt = transform.Find("myIP").GetComponent<TextMesh>();
        theirIPtxt = transform.Find("theirIP").GetComponent<TextMesh>();

        theirIP = PlayerPrefs.GetString("theirIP");
        theirIPtxt.text = "Their IP: " + theirIP;
        myIPtxt.text = "My IP: " + myIP;
	}
	
	// Update is called once per frame
	void Update () {
        UpdateLookingAt();
	}

    void AddStr(string str)
    {
        aud.Play();
        if (str == "back")
        {
            if (theirIP.Length > 0)
                theirIP = theirIP.Remove(theirIP.Length - 1, 1);
        }
        else if (str == "dot")
            theirIP += ".";
        else
            theirIP += str;
        theirIPtxt.text = "Their IP: " + theirIP;
        PlayerPrefs.SetString("theirIP", theirIP);
    }
    IPButton lastLookedAt = null;
    void UpdateLookingAt()
    {
        RaycastHit hit;
        if (!Physics.Raycast(cam.position, cam.forward, out hit))
        {
            if (lastLookedAt)
            {
                lastLookedAt.SetNormal();
            }
        }
        else if (hit.transform.name == "Button")
        {
            IPButton lookedAt = hit.transform.GetComponent<IPButton>();
            if(lastLookedAt && lookedAt != lastLookedAt)
                lastLookedAt.SetNormal();
            lookedAt.SetEmissive();
            if (Input.GetMouseButtonDown(0))
                AddStr(lookedAt.str);
            lastLookedAt = lookedAt;
        }
    }
}
