using UnityEngine;

public class Tile : MonoBehaviour
{
    // TODO: adujst to be able to take several towers
    [SerializeField] Tower towerPrefab; // not GameObject, to be able to access Tower method

    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } } // getter method rather than making var public, or creating method like GetIsPlaceable

    GridManager gridManager;
    Pathfinder pathfinder;
    Vector2Int coordinates = new Vector2Int();
    public Vector2Int Coordinates { get { return coordinates; } }

    // IMPORTANT: need to setup script execution order. GridManager > Pathfinder > Tile
    private void Awake()
    {
        gridManager = FindObjectOfType<GridManager>();
        pathfinder = FindObjectOfType<Pathfinder>();
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

    public void PlaceTower()
    {
        Node node = gridManager.GetNode(coordinates);
        if (node != null && node.isWalkable && !pathfinder.willBlockPath(coordinates))
        {
            TowerManager.instance.UnSelectTower(); // TODO: clicking the button on TowerUpgradePanel will bubble the click event and comes here, so ... if node is safe to place the tower, it's gonna unselect first so cannot do upgrade and such
            bool isSuccessful = towerPrefab.CreateTower(towerPrefab, transform.position);

            if (isSuccessful)
            {
                towerPrefab.SetTile(this);
                gridManager.BlockNode(coordinates);
                pathfinder.NotifyReceivers(); // everytime tower is placed, it sends message to recalculate the path that enemy should follow
            }
        }
    }
}
