using UnityEngine;

public class ProjectileSkill : MonoBehaviour
{
    [SerializeField] bool useSlowDown = false;
    public bool UseSlowDown { get { return useSlowDown; } }
    [SerializeField] float slowDownPercent = 0f;
    [SerializeField] float slowDownDuration = 0f;

    public void SlowDown(Enemy enemy)
    {
        if (!useSlowDown) return;

        EnemyMover enemyMover = enemy.GetComponent<EnemyMover>();
        StartCoroutine(enemyMover.DowngradeEnemeySpeed(slowDownPercent, slowDownDuration)); // TODO: seconds

    }
}
