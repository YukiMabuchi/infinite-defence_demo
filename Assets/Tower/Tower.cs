using System.Collections;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] int cost = 75;
    [SerializeField] float buildDelay = 1f;

    private void Start()
    {
        StartCoroutine(Build());
    }

    public bool CreateTower(Tower tower, Vector3 position)
    {
        Bank bank = FindObjectOfType<Bank>();
        if (!bank) return false;

        if (bank.CurrentBalance >= cost)
        {
            Instantiate(tower, position, Quaternion.identity);
            bank.Withdraw(cost);
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
}
