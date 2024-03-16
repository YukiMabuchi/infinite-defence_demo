using UnityEngine;

// manage how enemy gets affected by tower skills
public class EnemyStatus : MonoBehaviour
{
    bool isSlowedDown;
    public bool IsSlowedDown { get { return isSlowedDown; } }

    // TODO: better type to check status name
    public void UpdateEnemeyStatus(string status, bool state)
    {
        if (status == "slow")
        {
            isSlowedDown = state;
        }
    }
}
