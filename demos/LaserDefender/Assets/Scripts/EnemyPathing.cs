using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPathing : MonoBehaviour
{
    [SerializeField] float waypointPause = 1f;
    [SerializeField] bool loopPathing = false;
    [SerializeField] int maxLoops = -1;

    WaveConfig waveConfig;
    float moveSpeed;
    List<Transform> waypoints;

    int waypointIndex = 0;
    bool canMove = true;
    int loopIndex = 0;

    public void SetWaveConfig(WaveConfig waveConfig)
    {
        this.waveConfig = waveConfig;
    }

    void Start()
    {
        waypoints = waveConfig.GetWaveWaypoints();
        if (waypoints.Count < 1)
        {
            Destroy(gameObject);
        }
        transform.position = waypoints[0].transform.position;
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (!canMove)
        {
            return;
        }
        if (waypointIndex >= waypoints.Count)
        {
            Destroy(gameObject);
            return;
        }
        var target = waypoints[waypointIndex];
        if (transform.position != target.transform.position)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                target.transform.position,
                waveConfig.GetMoveSpeed() * Time.deltaTime);
            return;
        }
        ++waypointIndex;
        if (waypointIndex >= waypoints.Count && loopPathing
            && (loopIndex < maxLoops || maxLoops < 0))
        {
            ++loopIndex;
            waypointIndex = 0;
        }
        canMove = false;
        StartCoroutine(ReleaseMovementLock(waypointPause));
    }

    IEnumerator ReleaseMovementLock(float delay)
    {
        yield return new WaitForSeconds(delay);
        canMove = true;
    }
}
