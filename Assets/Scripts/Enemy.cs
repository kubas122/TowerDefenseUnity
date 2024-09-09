using UnityEngine;

public class Enemy : MonoBehaviour
{
    public delegate void DeathDelegate();
    public event DeathDelegate OnDeath;

    public float health = 100f;            // Enemy HP
    public float speed = 2f;               // Enemy Speed
    private float originalSpeed;           // Basic Speed

    public int goldReward = 10;            // Gold for kill
    public bool isBoss = false;            // is Boss?

    public Transform[] waypoints;          // Waypoint
    private int waypointIndex = 0;
    private HeartManager heartManager;
    private GameManager gameManager;


    private float boostCooldown = 5f;      // cooldwon on boss skill
    private float boostMultiplier = 0.25f; // Boss boost
    private float boostTimer = 0f;         // timer

    void Start()
    {
        originalSpeed = speed;
        heartManager = FindObjectOfType<HeartManager>();
        gameManager = FindObjectOfType<GameManager>();

        // Automatic Waypoint
        GameObject path = GameObject.Find("Path");
        if (path != null)
        {
            waypoints = new Transform[path.transform.childCount];
            for (int i = 0; i < path.transform.childCount; i++)
            {
                waypoints[i] = path.transform.GetChild(i);
            }
        }
        else
        {
            Debug.LogError("Path object not found.");
        }
    }

    void Update()
    {
        Move();

        if (isBoss) // if boss give boss boost
        {
            HandleBossBoost();
        }
    }

    void Move() // move through waypoints
    {
        if (waypointIndex < waypoints.Length)
        {
            transform.position = Vector2.MoveTowards(transform.position,
                waypoints[waypointIndex].position,
                speed * Time.deltaTime);

            if (transform.position == waypoints[waypointIndex].position)
            {
                waypointIndex++;
            }
        }
        else
        {
            ReachEnd();
        }
    }

    void ReachEnd()
    {
        heartManager.LoseHeart();
        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {

        if (isBoss && OnDeath != null) // if boss
        {
            OnDeath.Invoke();  // boss death event
        }

        gameManager.AddGold(goldReward); // give gold for killing
        Destroy(gameObject);
    }

    public void Slow(float slowPercent, float duration) // Slow enemy
    {
        if (isBoss)  // If boss ignore slow
        {
            return;
        }

        speed = originalSpeed * (1f - slowPercent);
        Invoke("ResetSpeed", duration);
    }

    void ResetSpeed()
    {
        speed = originalSpeed;
    }


    void HandleBossBoost() // Boss boost
    {
        boostTimer += Time.deltaTime;

        if (boostTimer >= boostCooldown)
        {
            speed += originalSpeed * boostMultiplier;
            Invoke("ResetSpeed", 3f);
            boostTimer = 0f;
        }
    }
}
