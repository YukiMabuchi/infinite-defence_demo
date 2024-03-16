using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 75;
    public int Cost { get { return cost; } }

    [SerializeField] float buildDelay = 1f;

    [SerializeField] bool doesAim = true;
    public bool DoesAim { get { return doesAim; } }


    [SerializeField] LayerMask towerUIlayer;

    [SerializeField] float range = 20f; // TODO: 10 for 1 block, but considering the current pos of the tower, 0.5f has to be the basis
    public float Range { get { return range; } }

    [SerializeField] GameObject rangeIndicator;
    public GameObject RangeIndicator { get { return rangeIndicator; } }

    TowerUpgrader upgrader;
    public TowerUpgrader Upgrader { get { return upgrader; } }

    Tile targetTile;
    bool skipBuildDelay = false;

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
            yield return new WaitForSeconds(skipBuildDelay ? 0 : buildDelay);
        }
    }

    public void SetTile(Tile tile)
    {
        targetTile = tile;
    }

    public void SetRange(float _range)
    {
        range = _range;
    }

    bool IsOnLayer(GameObject obj, LayerMask layerMask)
    {
        return layerMask == (layerMask | (1 << obj.layer));
    }

    public void ManageRangeIndicator(bool state)
    {
        rangeIndicator.SetActive(state);
    }

    public void SkipBuildDelay(bool state)
    {
        skipBuildDelay = state;
    }

    // if the GameObject has collider, you can use this
    private void OnMouseDown()
    {
        if (BattleManager.instance.IsGameOver || BattleManager.instance.IsGamePaused) return;

        // hide the previous tower's indicator first if any
        if (TowerManager.instance.SelectedTower)
        {
            TowerManager.instance.UnSelectTower();
        }
        TowerManager.instance.SelectTower(this);
        UIManager.instance.OpenTowerUpgradePanel(upgrader.AllowUpgrade);
    }

    private void OnDestroy()
    {
        if (targetTile)
        {
            targetTile.SetIsPlaceable(true);
        }
    }
}
