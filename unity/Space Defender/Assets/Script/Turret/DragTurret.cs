using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class DragTurret : MonoBehaviour, Killer {

    private Vector3 screenPoint;
    private Vector3 offset;

    public int fireInterval = 100;

    void OnMouseDown() {
        screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag() {
        Vector3 cursorPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(cursorPoint) + offset;
        transform.position = cursorPosition;
    }

    int Killer.GetFireInterval() {
        return this.fireInterval;
    }

    int Killer.GetID() {
        return this.GetInstanceID();
    }

    public abstract void ShotSpawn();

    public abstract void Attack(Dictionary<int, Victim> victims);
}
