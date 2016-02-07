using UnityEngine;
using System.Collections;

public class explosion : MonoBehaviour {
    private float timer = 0f;
    public float timeTilDeath = 1f;
	// Use this for initialization
	void Start () {
        AudioSource aud = GetComponent<AudioSource>();
        aud.pitch *= (Random.value + 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > timeTilDeath)
            Destroy(this.gameObject);
	}
}
