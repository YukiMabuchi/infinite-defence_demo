using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 75;
    public int Cost { get { return cost; } }

    [SerializeField] float buildDelay = 1f;

    [SerializeField] LayerMask towerUIlayer;

    [SerializeField] GameObject rangeIndicator;
    public GameObject RangeIndicator { get { return rangeIndicator; } }

    TowerUpgrader upgrader;
    public TowerUpgrader Upgrader { get { return upgrader; } }

    Tile targetTile;

    private void Awake()
    {
        upgrader = GetComponent<TowerUpgrader>();
    }


    private void Start()
    {
        StartCoroutine(Build());
    }

    public bool CreateTower(Tower tower, Vector3 position)
    {
        if (Bank.instance.IsAffordable(cost))
        {
            Instantiate(tower, position, Quaternion.identity);
            Bank.instance.Withdraw(cost);
            return true;
        }

        return false;
    }

    /// <summary>
    /// enable each children and grandchildren gradually
    /// </summary>
    /// <returns></returns>
    IEnumerator Build()
    {
        // child in transform
        foreach (Transform child in transform)
        {
            if (IsOnLayer(child.gameObject, towerUIlayer)) continue;
            child.gameObject.SetActive(false);
            foreach (Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(false);
            }
        }

        foreach (Transform child in transform)
        {
            if (IsOnLayer(child.gameObject, towerUIlayer)) continue;
            child.gameObject.SetActive(true);
            foreach (Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(true);
            }
            yield return new WaitForSeconds(buildDelay);
        }
    }

    public void SetTile(Tile tile)
    {
        targetTile = tile;
    }

    bool IsOnLayer(GameObject obj, LayerMask layerMask)
    {
        return layerMask == (layerMask | (1 << obj.layer));
    }

    public void ManageRangeIndicator(bool state)
    {
        rangeIndicator.SetActive(state);
    }

    // if the GameObject has collider, you can use this
    private void OnMouseDown()
    {
        // hide the previous tower's indicator first if any
        if (TowerManager.instance.SelectedTower)
        {
            TowerManager.instance.UnSelectTower();
        }
        TowerManager.instance.SelectTower(this);
        UIManager.instance.OpenTowerPanel();
    }

    private void OnDestroy()
    {
        if (targetTile)
        {
            targetTile.SetIsPlaceable(true);
        }
    }
}
