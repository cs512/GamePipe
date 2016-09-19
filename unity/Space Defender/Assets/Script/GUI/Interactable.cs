using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Interactable : MonoBehaviour {
    public LayerMask touchInputMask;

    private List<GameObject> touchList = new List<GameObject>();
    private GameObject[] touchesOld;
    private RaycastHit hit;

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
            CircleSpwaner.ins.SpawnMenu(this);
        }
    }
#else
    void Update()
    {
        if (Input.touchCount > 0) {

            touchesOld = new GameObject[touchList.Count];

            touchList.CopyTo(touchesOld);
            touchList.Clear();

            foreach (Touch touch in Input.touches) {
                Ray ray = GetComponent<Camera>().ScreenPointToRay(touch.position);
                if (Physics.Raycast(ray, out hit, touchInputMask)) {

                    GameObject recipient = hit.transform.gameObject;
                    touchList.Add(recipient);

                    if (touch.phase == TouchPhase.Began) {
                        CircleSpwaner.ins.SpawnMenu(this);
                    }
                }
            }
            foreach (GameObject g in touchesOld) {
                if (!touchList.Contains(g)) {
                    g.SendMessage("OnTouchExit", hit.point, SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }
#endif
}
