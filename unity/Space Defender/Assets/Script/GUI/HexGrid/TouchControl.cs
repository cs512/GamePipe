using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class TouchControl : MonoBehaviour {

    public HexGrid hexGrid;

    Touch? dragTouch = null;
    int layerMask = 1 << 8;
    bool hasMove;

    void Awake() {
        
    }

    void Update() {
        if (Application.platform != RuntimePlatform.Android) {
            if ( Input.GetMouseButton(0) && (Input.mousePosition.y < 0.92f * Screen.height)) {
                HandleTap(Input.mousePosition);
            }
        } else {
            foreach (Touch touch in Input.touches) {
                if (touch.phase == TouchPhase.Began && dragTouch == null) {
                    dragTouch = touch;
                    hasMove = false;
                    return;
                }
                if (touch.phase == TouchPhase.Moved && touch.fingerId == dragTouch.Value.fingerId) {
                    hasMove = true;
                    GameObject cam = GameObject.Find("Main Camera");
                    cam.transform.position += new Vector3(-touch.deltaPosition.x, 0, -touch.deltaPosition.y);
                    return;
                }
                if (touch.phase == TouchPhase.Ended && touch.fingerId == dragTouch.Value.fingerId) {
                    if (!hasMove && (touch.position.y < 0.92f * Screen.height)) {
                        HandleTap(touch.position);
                    }
                    dragTouch = null;
                    return;
                }
            }
        }
        
    }

    void HandleTap(Vector2 position) {
        Ray inputRay = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit, Mathf.Infinity, layerMask)) {
            Vector3 hitPoint = hit.point;
            hexGrid.ColorCell(hit.point, new Color(1, 0, 0, 0.5f));
            print(hexGrid.GetGridGlobalPosition(hitPoint));
        }
    }

}
