using UnityEngine;

public class Tile : MonoBehaviour
{
    // TODO: later
    // [SerializeField] Tower towerPrefab; // not GameObject, to be able to access Tower method

    [SerializeField] bool isPlaceable;
    public bool IsPlaceable { get { return isPlaceable; } } // getter method rather than making var public, or creating method like GetIsPlaceable

    GridManager gridManager;
    Pathfinder pathfinder;
    Vector2Int coordinates = new Vector2Int();

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

    // TODO: later
    // private void OnMouseDown() // built-in
    // {
    //     if (gridManager.GetNode(coordinates).isWalkable && !pathfinder.willBlockPath(coordinates))
    //     {
    //         bool isSuccessful = towerPrefab.CreateTower(towerPrefab, transform.position);

    //         if (isSuccessful)
    //         {
    //             gridManager.BlockNode(coordinates);
    //             pathfinder.NotifyReceivers(); // everytime tower is placed, it sends message to recalculate the path that enemy should follow
    //         }
    //     }
    // }
}
