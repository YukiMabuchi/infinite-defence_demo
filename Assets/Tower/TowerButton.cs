using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TowerButton : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] Transform indicator;
    [SerializeField] LayerMask tileLayer;

    [SerializeField] Tower towerToPlace;
    Tower towerDisplay; // dummy tower to tell users where to put it

    GridManager gridManager;

    private void Start()
    {
        gridManager = FindObjectOfType<GridManager>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        StartTowerPlacement();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 location = GetIndicatorPosition();
        indicator.position = location;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // set prfab's build delay to default (without it, it will affect the build)
        towerToPlace.SkipBuildDelay(false);

        // destroy the dummy tower
        Destroy(indicator.gameObject);

        // TODO: find better way, maybe onDrop in Tile (I could not figure out why it didn't work, the event was not sent to tile)
        Vector2Int coordinates = gridManager.GetCoordinatesFromPosition(indicator.transform.position);
        Tile[] tiles = FindObjectsOfType<Tile>();
        Tile targetTile = Array.Find(tiles, tile => tile.Coordinates == coordinates);
        if (targetTile)
        {
            targetTile.PlaceTower();
        }
    }

    public void StartTowerPlacement()
    {
        // unset prfab's build delay
        towerToPlace.SkipBuildDelay(true);

        // instantiate one tower to use it as dummy tower to enable user to see where to put it
        towerDisplay = Instantiate(towerToPlace);
        towerDisplay.enabled = false;
        TargetLocator targetLocator = towerDisplay.GetComponent<TargetLocator>();
        targetLocator.Attack(false);
        targetLocator.enabled = false; // disaling TargetLocator script, so prevents shooting
        towerDisplay.GetComponent<Collider>().enabled = false; // prevent detecting its own collider in update

        // put the dummy tower to indicator to show it while dragging
        indicator = towerDisplay.transform;
    }

    public Vector3 GetIndicatorPosition()
    {
        Vector3 location = Vector3.zero;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(ray.origin, ray.direction * 200f, Color.red);

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 200f, tileLayer))
        {
            location = hit.point;
        }

        location.y = 0f;

        return location;
    }
}