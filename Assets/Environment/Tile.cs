using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } } // getter method rather than making var public, or creating method like GetIsPlaceable

    GridManager gridManager;
    Pathfinder[] pathfinders;

    Vector2Int coordinates = new Vector2Int();
    public Vector2Int Coordinates { get { return coordinates; } }

    string initialTileColor = "#006F07"; // TODO: consider using SerializedField to get the material

    // IMPORTANT: need to setup script execution order. GridManager > Pathfinder > Tile
    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinders = FindObjectsOfType<Pathfinder>();
    }

    void Start()
    {
        if (gridManager != null)
        {
            coordinates = gridManager.GetCoordinatesFromPosition(transform.position);

            if (!isPlaceable)
            {
                gridManager.BlockNode(coordinates);
            }
        }
    }

    public void SetIsPlaceable(bool state)
    {
        isPlaceable = state;
    }

    bool CheckPlaceable()
    {
        Node node = gridManager.GetNode(coordinates);
        return isPlaceable && node != null && node.isWalkable && !pathfinders[0].willBlockPath(coordinates); // pathfinders[0] can be any one of the pathfinders, just using the method
    }

    public void PlaceTower(Tower towerPrefab)
    {
        if (CheckPlaceable())
        {
            TowerManager.instance.UnSelectTower(); // TODO: clicking the button on TowerUpgradePanel will bubble the click event and comes here, so ... if node is safe to place the tower, it's gonna unselect first so cannot do upgrade and such
            bool isSuccessful = towerPrefab.CreateTower(towerPrefab, transform.position);

            if (isSuccessful)
            {
                towerPrefab.SetTile(this);
                gridManager.BlockNode(coordinates);

                // everytime tower is placed, it sends message to recalculate the path that enemy should follow
                foreach (Pathfinder _pathfinder in pathfinders)
                {
                    _pathfinder.NotifyReceivers();
                }
            }
        }

        Color color;
        if (ColorUtility.TryParseHtmlString(initialTileColor, out color))
        {
            PaintTile(color);
        }
    }

    void PaintTile(Color color)
    {
        MeshRenderer meshRenderer = GetComponentInChildren<MeshRenderer>();
        if (meshRenderer != null)
        {
            foreach (Material material in meshRenderer.materials)
            {
                material.color = color;
            }
        }
    }

    private void OnMouseEnter()
    {
        if (TowerManager.instance.PlacingTower == null)
        {
            return;
        }

        Color color = CheckPlaceable() ? Color.blue : Color.red;
        PaintTile(color);
    }

    private void OnMouseExit()
    {
        if (TowerManager.instance.PlacingTower == null)
        {
            return;
        }

        Color color;
        if (ColorUtility.TryParseHtmlString(initialTileColor, out color))
        {
            PaintTile(color);
        }
    }
}
