using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 75;
    public int Cost { get { return cost; } }

    [SerializeField] float buildDelay = 1f;

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
            child.gameObject.SetActive(false);
            foreach (Transform grandchild in child)
            {
                grandchild.gameObject.SetActive(false);
            }
        }

        foreach (Transform child in transform)
        {
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

    // if the GameObject has collider, you can use this
    private void OnMouseDown()
    {
        UIManager.instance.OpenTowerPanel(this);
    }

    private void OnDestroy()
    {
        if (targetTile)
        {
            targetTile.SetIsPlaceable(true);
        }
    }
}
