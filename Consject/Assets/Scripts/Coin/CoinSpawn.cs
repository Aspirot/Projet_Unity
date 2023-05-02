using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CoinSpawn : MonoBehaviour
{

    public int maxDistance;
    public GameObject coin;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Vector3 FindRandomStart()
    {
        Vector3 randomPosition = Random.insideUnitSphere * maxDistance;
        randomPosition.y = 0.5f;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomPosition, out hit, maxDistance, NavMesh.AllAreas);
        return hit.position;
    }

    public void spawn()
    {
        Instantiate(coin, FindRandomStart(), Quaternion.identity);
    }

    public void CoinFound()
    {
        Destroy(gameObject);
        var c = Instantiate(coin, FindRandomStart(), Quaternion.identity);
        //c.GetComponent<CoinMouvement>().startWander();
        
    }
}
