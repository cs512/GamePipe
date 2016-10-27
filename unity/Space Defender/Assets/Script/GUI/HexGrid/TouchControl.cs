using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class TouchControl : MonoBehaviour {

    public SpawnMenu spawnMenu;
    public HexGrid hexGrid;

    Touch? dragTouch = null;
    int layerMask = 1 << 8;
    bool hasMove;
    bool inMenu = false;
    float zoomFactor = 1.1f;

    void Awake() {

    }

    void Update() {
        if (spawnMenu.MenuShowing) {
            if (Application.platform != RuntimePlatform.Android) {
                if (Input.GetMouseButton(0) && (Input.mousePosition.y < 0.92f * Screen.height)) {
                    HandleTurretMenu(Input.mousePosition);
                }
            } else {
                foreach (Touch touch in Input.touches) {
                    HandleTurretMenu(touch.position);
                }
            }
        } else {
            if (Application.platform != RuntimePlatform.Android) {
                if (Input.GetMouseButton(0) && (Input.mousePosition.y < 0.92f * Screen.height)) {
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
                        cam.transform.position += new Vector3(-touch.deltaPosition.x * zoomFactor, 0, -touch.deltaPosition.y * zoomFactor);
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
        
        
    }

    void HandleTap(Vector2 position) {
        Ray inputRay = Camera.main.ScreenPointToRay(position);
        RaycastHit hit;
        if (Physics.Raycast(inputRay, out hit, Mathf.Infinity, layerMask)) {
            Vector3 hitPoint = hit.point;
            hexGrid.ColorCell(hit.point, new Color(1, 0, 0, 0.5f));
            Vector3 menuPosition = hexGrid.GetGridGlobalPosition(hitPoint);
            spawnMenu.transform.position = menuPosition;
            spawnMenu.ShowMenu();
        }
    }

    void HandleTurretMenu(Vector2 position) {
        var pointer = new PointerEventData(EventSystem.current);
        // convert to a 2D position
        pointer.position = position;
        var raycastResults = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointer, raycastResults);
        if (raycastResults.Count > 0) {
            // Do anything to the hit objects. Here, I simply disable the first one.
            foreach (RaycastResult rr in raycastResults) {
                if (rr.gameObject.tag == "Menu Button")
                    return;
            }
        }
        spawnMenu.DestroyMenu();
    }

}
