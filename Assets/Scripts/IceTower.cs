using UnityEngine;

public class IceTower : MonoBehaviour
{
    public float range = 5f;  // Tower Range
    public float slowAmount = 0.5f;  // Slow rate
    public float slowDuration = 2f;  // Slow duration
    public LineRenderer lineRenderer;  // Draw laser

    private Transform target;  // Target

    void Update()
    {
        UpdateTarget();

        if (target == null)
        {
            if (lineRenderer.enabled)
            {
                lineRenderer.enabled = false;  // Hide laser if no target
            }
            return;
        }

        LockOnTarget();
        ShootLaser();
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }
    }

    void LockOnTarget() // Can add tower movemnt if aiming
    {

    }

    void ShootLaser()
    {
        if (!lineRenderer.enabled)
        {
            lineRenderer.enabled = true;
        }

        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, target.position);

        // Slow target
        Enemy enemy = target.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.Slow(slowAmount, slowDuration);
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
