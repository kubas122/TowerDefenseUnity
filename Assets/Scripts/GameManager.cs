using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int gold = 1000; // gold on start
    public TMP_Text goldText;  // gold text

    void Start()
    {
        UpdateGoldUI();
    }

    public void AddGold(int amount)
    {
        gold += amount;
        UpdateGoldUI();
    }

    public void SpendGold(int amount)
    {
        if (gold >= amount)
        {
            gold -= amount;
            UpdateGoldUI();
        }
        else
        {
            Debug.Log("Not enough gold!");
        }
    }

    void UpdateGoldUI()
    {
        goldText.text = "Gold: " + gold;
    }
}
