using System.Collections.Generic;
using System.Linq;
using Unity.AI.Navigation;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class SetUpLevel : MonoBehaviour
{
    private readonly IList<string> roomTags = new List<string>()
    {
        "Entrée","Salon", "Cuisine", "Salle à manger", "Salle de bain", "Chambre", "Couloir", "Salle d'eau", "Escalier", "Chambre 1", "Chambre 2", "Chambre 3", "Chambre 4", "Chambre 5"
    };

    private readonly IList<string> wallTags = new List<string>()
    {
        "Mur Droite", "Mur Gauche", "Mur Haut", "Mur Bas"
    };

    public GameObject SmallCeiling;
    public GameObject MediumCeiling;
    public GameObject BigCeiling;
    public List<GameObject> surfaces;
    

    public void SetUp()
    {
        var rdc = GameObject.FindGameObjectWithTag("RDC");
        var secondFloor = GameObject.FindGameObjectWithTag("Étage 2");
        List<GameObject> walls = null;
        List<GameObject> rooms = null;
        if (rdc != null)
        {
            walls = GetAllLevelWall("RDC");
            foreach(var wall in walls)
            {
                var doorParts = wall.GetComponentsInChildren<Transform>();
                if (doorParts.Length == 1)
                {
                    wall.transform.position = new Vector3(wall.transform.position.x, (Rooms.RdcHeight / 2), wall.transform.position.z);
                    wall.transform.localScale = new Vector3(wall.transform.localScale.x, Rooms.RdcHeight * 4 / 3, wall.transform.localScale.z);
                }
                else
                {
                    foreach (var doorPart in doorParts)
                    {
                        if (doorPart.tag != "Doormat" && doorPart.tag != "Doortop" && !doorPart.name.ToLower().Contains("wall"))
                        {
                            doorPart.transform.position = new Vector3(doorPart.position.x, (Rooms.RdcHeight /2), doorPart.position.z);
                            doorPart.transform.localScale = new Vector3(doorPart.localScale.x, Rooms.RdcHeight, doorPart.localScale.z);
                        }
                        if(doorPart.tag == "Doortop")
                        {
                            doorPart.transform.position = new Vector3(doorPart.position.x,(Rooms.RdcHeight / 2) + 1, doorPart.position.z);
                            doorPart.transform.localScale = new Vector3(doorPart.localScale.x, Rooms.RdcHeight - 2, doorPart.localScale.z);
                        }
                    }
                }
            }
            walls.Clear();
            rooms = GetAllLevelRoom("RDC");
            foreach(var room in rooms)
                CreateCeilingForRoom(room);


            if (secondFloor != null)
            {
                walls = GetAllLevelWall("Étage 2");
                foreach (var wall in walls)
                {
                    var doorParts = wall.GetComponentsInChildren<Transform>();
                    if (doorParts.Length == 1)
                    {
                        wall.transform.position = new Vector3(wall.transform.position.x, (Rooms.SecondFloorHeight / 2), wall.transform.position.z);
                        wall.transform.localScale = new Vector3(wall.transform.localScale.x, Rooms.SecondFloorHeight * 4 / 3, wall.transform.localScale.z);
                    }
                    else
                    {
                        foreach (var doorPart in doorParts)
                        {
                            if (doorPart.tag != "Doormat" && doorPart.tag != "Doortop" && !doorPart.name.ToLower().Contains("wall"))
                            {
                                doorPart.transform.position = new Vector3(doorPart.position.x, (Rooms.SecondFloorHeight / 2), doorPart.position.z);
                                doorPart.transform.localScale = new Vector3(doorPart.localScale.x, Rooms.SecondFloorHeight, doorPart.localScale.z);
                            }
                            if (doorPart.tag == "Doortop")
                            {
                                doorPart.transform.position = new Vector3(doorPart.position.x, (Rooms.SecondFloorHeight / 2) + 1, doorPart.position.z);
                                doorPart.transform.localScale = new Vector3(doorPart.localScale.x, Rooms.SecondFloorHeight - 2, doorPart.localScale.z);
                            }
                        }
                    }
                }
                walls.Clear();

                rooms = GetAllLevelRoom("Étage 2");
                foreach (var room in rooms)
                    CreateCeilingForRoom(room);

                rdc.transform.position = new Vector3(0, 0, 0);
                secondFloor.transform.position = new Vector3(0, Rooms.RdcHeight);
            }
            else
                rdc.transform.position = new Vector3(0, 0, 0);
        }
        else
        {
            walls = GetAllLevelWall("Étage 2");
            foreach (var wall in walls)
            {
                var doorParts = wall.GetComponentsInChildren<Transform>();
                if (doorParts.Length == 1)
                {
                    wall.transform.position = new Vector3(wall.transform.position.x, (Rooms.SecondFloorHeight / 2), wall.transform.position.z);
                    wall.transform.localScale = new Vector3(wall.transform.localScale.x, Rooms.SecondFloorHeight * 4 / 3, wall.transform.localScale.z);
                }
                else
                {
                    foreach (var doorPart in doorParts)
                    {
                        if (doorPart.tag != "Doormat" && doorPart.tag != "Doortop" && !doorPart.name.ToLower().Contains("wall"))
                        {
                            doorPart.transform.position = new Vector3(doorPart.position.x, (Rooms.SecondFloorHeight / 2), doorPart.position.z);
                            doorPart.transform.localScale = new Vector3(doorPart.localScale.x, Rooms.SecondFloorHeight, doorPart.localScale.z);
                        }
                        if (doorPart.tag == "Doortop")
                        {
                            doorPart.transform.position = new Vector3(doorPart.position.x, (Rooms.SecondFloorHeight / 2) + 1, doorPart.position.z);
                            doorPart.transform.localScale = new Vector3(doorPart.localScale.x, Rooms.SecondFloorHeight - 2, doorPart.localScale.z);
                        }
                    }
                }
            }
            rooms = GetAllLevelRoom("Étage 2");
            foreach (var room in rooms)
                CreateCeilingForRoom(room);
            secondFloor.transform.position = new Vector3(0, 0, 0);
        }

        
        surfaces.AddRange(new List<GameObject>(GameObject.FindGameObjectsWithTag("RDC")));
        

        for (int i = 0; i< surfaces.Count; i++)
        {
            surfaces[i].GetComponent<NavMeshSurface>().BuildNavMesh();
        }

    }


    private List<GameObject> GetAllLevelWall(string level)
    {
        var floor = GameObject.FindGameObjectWithTag(level);
        var walls = new List<GameObject>();
        foreach(var room in floor.GetComponentsInChildren<Transform>())
        {
            if (roomTags.Contains(room.tag))
            {
                foreach(var wall in room.GetComponentsInChildren<Transform>())
                {
                    if (wallTags.Contains(wall.tag))
                    {
                        walls.Add(wall.gameObject);
                    }
                }
            }
        }
        return walls;
    }

    private List<GameObject> GetAllLevelRoom(string level)
    {
        var floor = GameObject.FindGameObjectWithTag(level);
        var rooms = new List<GameObject>();
        foreach (var room in floor.GetComponentsInChildren<Transform>())
        {
            if (roomTags.Contains(room.tag))
            {
                rooms.Add(room.gameObject);
            }
        }
        return rooms;
    }

    private void CreateCeilingForRoom(GameObject room)
    {
        GameObject toInstanciate = null;
        var height = 0F;
        var children = room.GetComponentsInChildren<Transform>();
        if(children.Length > 1) {
            GameObject ceil = null;
            foreach(var child in children)
            {
                if (child.tag == "Ceiling")
                    ceil = child.gameObject;
            }
            if(ceil != null)
            {
                Destroy(ceil);
            }
        }

        switch (room.transform.localScale.x)
        {
            case 0.5F:
                toInstanciate = SmallCeiling;
                break;
            case 0.75F:
                toInstanciate= MediumCeiling;
                break;
            case 1:
                toInstanciate = BigCeiling;
                break;
        }
        switch (room.layer)
        {
            case 6:
                height = Rooms.RdcHeight;
                break; 
            case 7:
                height = Rooms.SecondFloorHeight;
                break;
        }
        if(height != 0 && toInstanciate != null)
        {
            var ceiling = Instantiate(toInstanciate, new Vector3(room.transform.position.x, room.transform.position.y + height, room.transform.position.z), Quaternion.identity);
            ceiling.layer = room.layer;
            ceiling.tag = "Ceiling";
            ceiling.transform.Rotate(180, 0, 0);
            ceiling.transform.parent = room.transform;
        }
    }
}
