using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] WaveConfigSO waveConfig;
    Transform[] waypoints;
    int waypointIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        waypoints = waveConfig.GetWaypoints();
        transform.position = waveConfig.GetStartingWaypoint().position;
    }

    // Update is called once per frame
    void Update()
    {
        FollowPath();
    }
    void FollowPath()
    {
        if (waypointIndex < waypoints.Length)
        {
            Vector3 targetPosition = waypoints[waypointIndex].position;
            float moveDelta = waveConfig.GetEnemyMoveSpeed() * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveDelta);
            if (transform.position == targetPosition)
            {
                waypointIndex++;
            }
        }

        else
        {             
            Destroy(gameObject);
        }
    }
}
