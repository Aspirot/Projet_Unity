using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CoinMouvement : MonoBehaviour
{

    private NavMeshAgent coin;
    public int maxDistance = 20;
    public int secondsToWait = 2;

    // Start is called before the first frame update
    void Start()
    {
        coin = GetComponent<NavMeshAgent>();
        Wander();
       
    }

    // Update is called once per frame
    void Update()
    {
        if(!coin.isStopped) {
            if (!coin.pathPending && coin.remainingDistance < 1f)
            {
                coin.isStopped = true;
                StartCoroutine(WaitTime());
            }
        }
        
    }

    public void Wander()
    {
        Vector3 randomPosition =  FindNewPosition();
        Debug.Log(randomPosition);
        coin.SetDestination(randomPosition);    
    }

    public Vector3 FindNewPosition()
    {
        Vector3 randomPosition = UnityEngine.Random.insideUnitSphere * maxDistance;
        randomPosition.y = 0f;
        NavMeshHit hit;
        NavMesh.SamplePosition(transform.position + randomPosition, out hit, maxDistance, NavMesh.AllAreas);
        return hit.position;
    }

    private IEnumerator WaitTime()
    {
        yield return new WaitForSecondsRealtime(secondsToWait);
        coin.isStopped = false;
        Wander();
    }

    public void startWander()
    {
        coin = GetComponent<NavMeshAgent>();
        Wander();
    }

}
