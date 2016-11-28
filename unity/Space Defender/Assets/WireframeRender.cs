using UnityEngine;
using System.Collections;

public class WireframeRender : MonoBehaviour {

	// Use this for initialization
	void Start () {
        GL.wireframe = true;
    }
	
	// Update is called once per frame
	void Update () {
        GL.wireframe = true;
    }

    void OnPreRender()
    {
        GL.wireframe = true;
    }

    void OnPostRender()
    {
        GL.wireframe = false;
    }
}
