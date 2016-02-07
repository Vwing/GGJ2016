using UnityEngine;
using System.Collections;

public class OnZombieDeath : MonoBehaviour {
    public GameObject zombieSplatter;
	void OnDestroy()
    {
        GameObject.Instantiate(zombieSplatter, transform.position, transform.rotation);
    }
}
