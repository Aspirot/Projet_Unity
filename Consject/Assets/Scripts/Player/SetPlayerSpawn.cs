using UnityEngine;

public class SetPlayerSpawn : MonoBehaviour
{
    public Vector3 spawn;
    // Start is called before the first frame update
    void Start()
    {
        spawn= new Vector3(0,1,0);
    }

    public void SetSpawnToEntry()
    {
        var defaultSpawnTag = "Entrée";
        var entry = GameObject.FindGameObjectWithTag(defaultSpawnTag);
        if (entry != null)
            spawn = entry.transform.position;
        else
            spawn = new Vector3(0, 0, 0);
        transform.position = spawn;
    }
}
