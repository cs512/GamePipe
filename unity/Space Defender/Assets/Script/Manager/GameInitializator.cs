using UnityEngine;
using System.Collections;

public class GameInitializator : MonoBehaviour {

    // Use this for initialization
    void Start() {
        Toolbox tb = Toolbox.Instance;
        tb.RegisterComponent<LevelManager>();
    }
    // Update is called once per frame
    void Update() {

    }
}
