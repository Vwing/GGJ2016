using UnityEngine;
using System.Collections;

public class DebugPosition : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine("PositionPost");
	}
	
	IEnumerator PositionPost()
    {
        while (true)
        {
            Debug.Log("Position: " + transform.position);
            yield return new WaitForSeconds(.4f);
        }
    }
}
