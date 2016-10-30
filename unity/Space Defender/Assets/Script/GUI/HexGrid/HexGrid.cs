using UnityEngine;
using UnityEngine.UI;
using System;

public class HexGrid : MonoBehaviour {

    public int width = 6;
    public int height = 6;

    public Color defaultColor = Color.white;
    public Color buildableColor = Color.green;
    public Color hasBuildingColor = Color.cyan;

    public HexCell cellPrefab;
    public Text cellLabelPrefab;

    public HexCell[] cells;

    HexMesh hexMesh;

    void Awake() {
        hexMesh = GetComponentInChildren<HexMesh>();

        cells = new HexCell[height * width];

        for (int z = 0, i = 0; z < height; z++) {
            for (int x = 0; x < width; x++) {
                CreateCell(x, z, i++);
            }
        }
    }

    void Start() {
        hexMesh.Triangulate(cells);
    }

    public void ColorCell(Vector3 position, Color color) {
        position = transform.InverseTransformPoint(position);
        HexCoordinates coordinates = HexCoordinates.FromPosition(position);
        //print(coordinates.X + ":" + coordinates.Y + ":" + coordinates.Z);
        int index = coordinates.X + coordinates.Z * width + coordinates.Z / 2;
        HexCell cell = cells[index];
        cell.color = color;
        hexMesh.Triangulate(cells);
    }

    private void ColorCell(HexCell cell, Color color) {
        //print(color);
        cell.color = color;
        hexMesh.Triangulate(cells);
    }

    public bool IsBuildable(Vector3 position) {
        position = transform.InverseTransformPoint(position);
        HexCoordinates coordinates = HexCoordinates.FromPosition(position);
        int index = coordinates.X + coordinates.Z * width + coordinates.Z / 2;
        HexCell cell = cells[index];
        if (cell.HasTurret) {
            return false;
        } else {
            foreach (HexDirection hd in Enum.GetValues(typeof(HexDirection))) {
                if (cell.GetNeighbor(hd) && cell.GetNeighbor(hd).HasTurret) {
                    return true;
                }
            }
            return false;
        }
    }

    public GameObject IsUpgradeable(Vector3 position) {
        position = transform.InverseTransformPoint(position);
        HexCoordinates coordinates = HexCoordinates.FromPosition(position);
        int index = coordinates.X + coordinates.Z * width + coordinates.Z / 2;
        HexCell cell = cells[index];
        return cell.Turret;
    }

    public void SetBuilding(Vector3 position, GameObject turret) {
        this.SetBuildingStatue(position, true, turret);
    }

    public void DeleteBuilding(Vector3 position) {
        this.SetBuildingStatue(position, false, null);
    }

    private HexCell SetBuildingStatue(Vector3 position, bool flag, GameObject turret) {
        position = transform.InverseTransformPoint(position);
        HexCoordinates coordinates = HexCoordinates.FromPosition(position);
        int index = coordinates.X + coordinates.Z * width + coordinates.Z / 2;
        HexCell cell = cells[index];
        cell.HasTurret = flag;
        cell.Turret = turret;
        this.SetColor(cell);
        foreach (HexDirection hd in Enum.GetValues(typeof(HexDirection))) {
            if (cell.GetNeighbor(hd))
                this.SetColor(cell.GetNeighbor(hd));
        }
        return cell;
    }

    private void SetColor(HexCell cell) {
        if (cell.HasTurret) {
            this.ColorCell(cell, this.hasBuildingColor);
            return;
        }
        foreach (HexDirection hd in Enum.GetValues(typeof(HexDirection))) {
            if (cell.GetNeighbor(hd) && cell.GetNeighbor(hd).HasTurret) {
                this.ColorCell(cell, this.buildableColor);
                return;
            }
        }
        this.ColorCell(cell, this.defaultColor);
    }

    public Vector3 GetGridGlobalPosition(Vector3 position) {
        position = transform.InverseTransformPoint(position);
        HexCoordinates coordinates = HexCoordinates.FromPosition(position);
        int index = coordinates.X + coordinates.Z * width + coordinates.Z / 2;
        return cells[index].transform.position;
    }
    public int GetTurretCount() {
        int count = 0;
        for (int i = 0; i < cells.Length; i++) {
            if (cells[i].HasTurret)
                count++;
        }
        return count; 
    }
    void CreateCell(int x, int z, int i) {
        Vector3 position;
        position.x = (x + z * 0.5f - z / 2) * (HexMetrics.innerRadius * 2f);
        position.y = 0f;
        position.z = z * (HexMetrics.outerRadius * 1.5f);

        HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = position;
        cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);
        cell.color = defaultColor;

        if (x > 0) {
            cell.SetNeighbor(HexDirection.W, cells[i - 1]);
        }
        if (z > 0) {
            if ((z & 1) == 0) {
                cell.SetNeighbor(HexDirection.SE, cells[i - width]);
                if (x > 0) {
                    cell.SetNeighbor(HexDirection.SW, cells[i - width - 1]);
                }
            } else {
                cell.SetNeighbor(HexDirection.SW, cells[i - width]);
                if (x < width - 1) {
                    cell.SetNeighbor(HexDirection.SE, cells[i - width + 1]);
                }
            }
        }
    }
}