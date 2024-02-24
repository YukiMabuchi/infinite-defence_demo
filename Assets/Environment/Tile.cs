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

    private void OnMouseDown() // built-in
    {
        if (gridManager.GetNode(coordinates).isWalkable && !pathfinder.willBlockPath(coordinates))
        {
            bool isSuccessful = towerPrefab.CreateTower(towerPrefab, transform.position);

            if (isSuccessful)
            {
                gridManager.BlockNode(coordinates);
                pathfinder.NotifyReceivers(); // everytime tower is placed, it sends message to recalculate the path that enemy should follow
            }
        }
    }
}
