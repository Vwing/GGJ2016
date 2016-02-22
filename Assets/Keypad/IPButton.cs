using UnityEngine;
using System.Collections;

public class IPButton : MonoBehaviour 
{
    public bool beingLookedAt;
    private bool lastBeingLookedAt;
    public string str;
    private Renderer mat;
    private Color emiss;

	void Start () 
    {
        str = transform.parent.name;
        mat = GetComponent<Renderer>();
        emiss = new Color(0.235f, 0.057f, 0.057f, 1f);
	}

    public void SetEmissive()
    {
        mat.material.SetColor("_EmissionColor", emiss);
    }

    public void SetNormal()
    {
        mat.material.SetColor("_EmissionColor", Color.black);
    }
}
