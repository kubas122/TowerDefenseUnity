using UnityEngine;

public class TowerManager : MonoBehaviour
{
    public GameObject iceTowerPrefab;
    public GameObject sniperTowerPrefab;
    public GameObject towerPrefab;

    public int iceTowerCost = 50;
    public int sniperTowerCost = 100;
    public int towerCost = 75;

    private GameManager gameManager;
    private GameObject towerToPlace;
    private bool isPlacing = false;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (isPlacing && towerToPlace != null)
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            towerToPlace.transform.position = mousePosition;

            if (Input.GetMouseButtonDown(0))
            {
                PlaceTower();
            }
        }
    }

    public void BuyIceTower()
    {
        if (gameManager.gold >= iceTowerCost)
        {
            gameManager.SpendGold(iceTowerCost);
            StartPlacingTower(iceTowerPrefab);
        }
        else
        {
            Debug.Log("Not enough gold for Ice Tower");
        }
    }

    public void BuySniperTower()
    {
        if (gameManager.gold >= sniperTowerCost)
        {
            gameManager.SpendGold(sniperTowerCost);
            StartPlacingTower(sniperTowerPrefab);
        }
        else
        {
            Debug.Log("Not enough gold for Sniper Tower");
        }
    }

    public void BuyTower()
    {
        if (gameManager.gold >= towerCost)
        {
            gameManager.SpendGold(towerCost);
            StartPlacingTower(towerPrefab);
        }
        else
        {
            Debug.Log("Not enough gold for Tower");
        }
    }

    void StartPlacingTower(GameObject towerPrefab)
    {
        towerToPlace = Instantiate(towerPrefab);
        isPlacing = true;
    }

    void PlaceTower() // not implemented
    {
        isPlacing = false;
        towerToPlace = null;
    }
}
