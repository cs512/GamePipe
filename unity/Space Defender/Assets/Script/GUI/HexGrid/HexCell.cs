using UnityEngine;

public class HexCell : MonoBehaviour {

    public HexCoordinates coordinates;
    public Color color;
    public Color inner = new Color(0, 0, 0, 0);
    public bool HasTurret {
        get {
            return hasTurret;
        }
        set {
            hasTurret = value;
        }
    }
    public GameObject Turret {
        get {
            return turret;
        }
        set {
            turret = value;
        }
    }

    bool hasTurret = false;
    GameObject turret;
    [SerializeField]
    HexCell[] neighbors;

    public HexCell GetNeighbor(HexDirection direction) {
        return neighbors[(int)direction];
    }

    public void SetNeighbor(HexDirection direction, HexCell cell) {
        neighbors[(int)direction] = cell;
        cell.neighbors[(int)direction.Opposite()] = this;
    }

    public GameObject GetTurretObject() {
        if (hasTurret) {
            return turret;
        } else {
            return null;
        }
    }

}