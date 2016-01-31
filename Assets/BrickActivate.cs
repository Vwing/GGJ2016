using UnityEngine;
using System.Collections;

public class BrickActivate : MonoBehaviour 
{
    public bool activated = false;
    private ParticleSystem.EmissionModule ps;
    private AudioSource aud;
    //private Renderer statRend;
    private float shininess;
    public GameObject statueHead;
	// Use this for initialization
	void Start () {
        ps = GetComponent<ParticleSystem>().emission;
        aud = GetComponent<AudioSource>();
        //statRend = statueHead.GetComponent<Renderer>();
	}
	
	void OnTriggerEnter(Collider other)
    {
        activated = true;
        ps.enabled = true;
        aud.enabled = true;
    }
    void OnTriggerExit(Collider other)
    {
        activated = false;
        ps.enabled = false;
        aud.enabled = false;
    }
    //void lerpShiny()
    //{

    //}
}
