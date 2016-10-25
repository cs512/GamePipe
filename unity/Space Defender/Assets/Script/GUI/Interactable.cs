using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Interactable : MonoBehaviour {
    public LayerMask touchInputMask;

    [System.Serializable]
    public class Action
    {
        public Color color;
        public Sprite sprite;
        public string title;
    }
    public Action[] options;
    // Use this for initialization

#if UNITY_EDITOR
    void OnMouseDown() {
        if (Input.GetMouseButtonDown(0)) {
            GetComponentsInChildren<CircleSpwaner>()[0].SpawnMenu(this);
        }
    }
#endif
    void Update() {
        int nbTouches = Input.touchCount;

        if (nbTouches > 0) {
            for (int i = 0; i < nbTouches; i++) {
                Touch touch = Input.GetTouch(i);

                if (touch.phase == TouchPhase.Began) {
                    Ray screenRay = Camera.main.ScreenPointToRay(touch.position);
                    RaycastHit hit;
                    if (Physics.Raycast(screenRay, out hit)) {
                        if (hit.collider.gameObject.name == "Quad") {
                            GetComponentsInChildren<CircleSpwaner>()[0].SpawnMenu(this);
                        }
                    }
                }

            }
        }
    }
}
