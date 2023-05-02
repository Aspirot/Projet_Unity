using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class Navmeshbaker : MonoBehaviour
{
    public List<GameObject> surfaces;
    // Start is called before the first frame update
    void Start()
    {
        surfaces.AddRange(new List<GameObject>(GameObject.FindGameObjectsWithTag("Entrée")));
        surfaces.AddRange(new List<GameObject>(GameObject.FindGameObjectsWithTag("Salon")));
        surfaces.AddRange(new List<GameObject>(GameObject.FindGameObjectsWithTag("Cuisine")));
        surfaces.AddRange(new List<GameObject>(GameObject.FindGameObjectsWithTag("Salle à manger")));
        surfaces.AddRange(new List<GameObject>(GameObject.FindGameObjectsWithTag("Salle de bain")));
        surfaces.AddRange(new List<GameObject>(GameObject.FindGameObjectsWithTag("Chambre")));
        surfaces.AddRange(new List<GameObject>(GameObject.FindGameObjectsWithTag("Couloir")));
        surfaces.AddRange(new List<GameObject>(GameObject.FindGameObjectsWithTag("Salle d'eau")));
        surfaces.AddRange(new List<GameObject>(GameObject.FindGameObjectsWithTag("Escalier")));
        surfaces.AddRange(new List<GameObject>(GameObject.FindGameObjectsWithTag("Chambre 1")));
        surfaces.AddRange(new List<GameObject>(GameObject.FindGameObjectsWithTag("Chambre 2")));
        surfaces.AddRange(new List<GameObject>(GameObject.FindGameObjectsWithTag("Chambre 3")));
        surfaces.AddRange(new List<GameObject>(GameObject.FindGameObjectsWithTag("Chambre 4")));
        surfaces.AddRange(new List<GameObject>(GameObject.FindGameObjectsWithTag("Chambre 5")));
        surfaces.AddRange(new List<GameObject>(GameObject.FindGameObjectsWithTag("Doormat")));

        for (int i = 0; i < surfaces.Count; i++)
        {
            surfaces[i].GetComponent<NavMeshSurface>().BuildNavMesh();
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
