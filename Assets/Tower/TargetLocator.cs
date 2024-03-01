using UnityEngine;

[RequireComponent(typeof(Tower))]
public class TargetLocator : MonoBehaviour
{
    [SerializeField] Transform weapon;
    [SerializeField] ParticleSystem projectileParticles;
    [SerializeField] float range = 20f; // 10 for 1 block

    Tower tower;
    Transform target;
    GridManager gridManager;

    private float rangeIndicatorSize = 65f; // 65 is the current RangeIndicator scale for 1 tile around the tower

    private void Start()
    {
        tower = GetComponent<Tower>();
        gridManager = FindObjectOfType<GridManager>();

        UpgradeRangeIndicator(range);
    }

    void Update()
    {
        FindClosestTarget();
        AimWeapon();
    }

    void FindClosestTarget()
    {
        Enemy[] enemies = FindObjectsOfType<Enemy>();
        Transform closestTarget = null;
        float maxDistance = Mathf.Infinity;

        // enemyの種類が多いと、guard statementとしてこのboundaryを超えたenemyを計算するとかしないとloopがデカくなる
        foreach (Enemy enemy in enemies)
        {
            float targetDistance = Vector3.Distance(transform.position, enemy.transform.position);
            if (targetDistance < maxDistance)
            {
                closestTarget = enemy.transform;
                maxDistance = targetDistance;
            }
        }
        target = closestTarget;
    }

    void AimWeapon()
    {
        float targetDistance = Vector3.Distance(transform.position, target.position);
        weapon.LookAt(target);

        if (targetDistance < range)
        {
            Attack(true);
        }
        else
        {
            Attack(false);
        }
    }

    void Attack(bool isActive)
    {
        // 撃つか撃たないかかparticle systemをいじる
        // particleが出る時、tower自身とぶつかって消えるから
        // 1. Project settingsのphysicsでlayerの設定
        // 2. particle systemのcollisionでCollide withでlayerを設定しtower除外
        var emissionModule = projectileParticles.emission;
        emissionModule.enabled = isActive;
    }

    public void UpgradeRangeIndicator(float range)
    {
        GameObject rangeIndicator = tower.RangeIndicator;
        float newScale = range / gridManager.UnityGridSize * rangeIndicatorSize;
        rangeIndicator.transform.localScale = new Vector3(newScale, newScale, newScale);
    }

    public void UpgradeFireRate(float newFireRange)
    {
        var emissionModule = projectileParticles.emission;
        emissionModule.rateOverTime = newFireRange;
    }

    public void UpgradeRange(float newRange)
    {
        range = newRange;
        UpgradeRangeIndicator(newRange);
    }
}
