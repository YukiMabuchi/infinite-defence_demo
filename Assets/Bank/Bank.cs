using UnityEngine;
using TMPro;

// EnemyMover and EnemyHealth will call the method of Enemy to deposit or withdraw gold 
public class Bank : MonoBehaviour
{
    [SerializeField] int startingBalance = 200;
    [SerializeField] int currentBalance;
    public int CurrentBalance { get { return currentBalance; } } // can access, but cannot set the val. Better than making currentBalance public

    [SerializeField] TextMeshProUGUI displayBalance;
    BattleManager battleManager;

    private void Awake()
    {
        battleManager = FindObjectOfType<BattleManager>();
        currentBalance = startingBalance;
        UpdateDisplay();
    }

    public void Deposit(int amount)
    {
        currentBalance += Mathf.Abs(amount); // Mathf.Abs => negative will be removed. -10 = 10
        UpdateDisplay();
    }
    public void Withdraw(int amount)
    {
        currentBalance -= Mathf.Abs(amount); // Mathf.Abs => negative will be removed. -10 = 10
        UpdateDisplay();

        // game over
        if (currentBalance < 0)
        {
            if (battleManager != null)
            {
                UpdateDisplay(0);
                battleManager.GameOver();
            }
        }
    }

    void UpdateDisplay()
    {
        displayBalance.text = "Gold: " + currentBalance;
    }
    void UpdateDisplay(int balance)
    {
        currentBalance = balance;
        displayBalance.text = "Gold: " + currentBalance;
    }
}
