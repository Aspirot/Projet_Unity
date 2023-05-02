using UnityEngine;
using UnityEngine.UI;

public class SetPlayerSpawn : MonoBehaviour
{
    public Vector3 spawn;
    // Start is called before the first frame update
    void Start()
    {
        spawn= new Vector3(0,1,0);
    }

    private void Update()
    {
        if(transform.position.y < -20)
        {
            gameObject.SetActive(false);
            transform.position = spawn;
            gameObject.SetActive(true);
        }
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
