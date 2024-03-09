using System.Collections;
using UnityEngine;

// TODO
public class EnemySkill : MonoBehaviour
{
    Enemy enemy;

    [SerializeField] float skillBetween = 5f;

    private void Start()
    {
        enemy = GetComponent<Enemy>();
    }

    public IEnumerator AttackFromLongDistance()
    {
        yield return new WaitForSeconds(skillBetween);
        enemy.GiveDamage();
    }
}
