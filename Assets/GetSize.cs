using UnityEngine;
using System.Collections;

public class GetSize : MonoBehaviour {

	[ContextMenu("Get bounding size of all child renderers")]
	void GetSizeOfChildren() {
        Renderer[] children = GetComponentsInChildren<Renderer>();
        Bounds b = children[0].bounds;
        for (int i = 1; i < children.Length; ++i)
            b.Encapsulate(children[i].bounds);
        Debug.Log("X bounds: " + b.size.x);
        Debug.Log("Y bounds: " + b.size.y);
        Debug.Log("Z bounds: " + b.size.z);
	}
	
}
