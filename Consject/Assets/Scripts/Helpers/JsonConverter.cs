using System.Collections.Generic;
using UnityEngine;

public class JsonConverter
{
    private readonly IList<string> roomTags = new List<string>()
    {
        "Entrée","Salon", "Cuisine", "Salle à manger", "Salle de bain", "Chambre", "Couloir", "Salle d'eau", "Escalier", "Chambre 1", "Chambre 2", "Chambre 3", "Chambre 4", "Chambre 5"
    };

    private readonly IList<string> wallTags = new List<string>()
    {
        "Mur Droite", "Mur Gauche", "Mur Haut", "Mur Bas"
    };

    public House EncodeHouse(string houseName)
    {
        House encodedHouse = new House();
        encodedHouse.name = houseName;
        encodedHouse.levels = new List<Level>();
        var rdc = GameObject.FindGameObjectWithTag("RDC");
        if (rdc != null)
        {
            var level = EncodeLevel(rdc);
            if (level.name != null)
                encodedHouse.levels.Add(level);
        }
        return encodedHouse;
    }

    public Level EncodeLevel(GameObject level)
    {
        Level encodedLevel = new Level();
        encodedLevel.name = level.tag;
        switch (encodedLevel.name)
        {
            case "RDC":
                encodedLevel.height = Rooms.RdcHeight; 
                break;
            case "Étage 2":
                encodedLevel.height = Rooms.SecondFloorHeight;
                break;
            default:
                encodedLevel.height = 4;
                break;
        }

        encodedLevel.rooms = new List<Room>();
        encodedLevel.furnitures = new List<Furniture>();
        foreach (var obj in level.GetComponentsInChildren<Transform>())
        {
            if (roomTags.Contains(obj.tag))
            {
                var room = EncodeRoom(obj.gameObject);
                if(room.name != null)
                    encodedLevel.rooms.Add(room);
            }
            if(obj.tag == "Furniture")
            {
                var furn = EncodeFurniture(obj.gameObject);
                if(furn.type != null)
                    encodedLevel.furnitures.Add(EncodeFurniture(obj.gameObject));
            }
        }
        return encodedLevel;
    }

    public GameObject DecodeLevel(string json)
    {
        return null;
    }

    public Room EncodeRoom(GameObject room)
    {
        Room encodedRoom = new Room();
        encodedRoom.name = room.name.Replace("(Clone)", "");
        encodedRoom.xposition = room.transform.position.x;
        encodedRoom.zposition = room.transform.position.z;
        encodedRoom.size = room.transform.localScale.x;
        encodedRoom.walls = new List<Wall>();
        foreach (var wall in room.GetComponentsInChildren<Transform>())
        {
            if (wallTags.Contains(wall.tag))
            {
                var wall_ = EncodeWall(wall.gameObject);
                if(wall_.type != null)
                    encodedRoom.walls.Add(wall_);
            }
        }

        return encodedRoom;
    }

    public GameObject DecodeRoom(string json)
    {
        return null;
    }

    public Furniture EncodeFurniture(GameObject furniture)
    {
        Furniture encodedFurniture= new Furniture();
        encodedFurniture.type = furniture.name.Replace("(Clone)","");
        encodedFurniture.xposition = furniture.transform.position.x;
        encodedFurniture.zposition = furniture.transform.position.z;
        encodedFurniture.yposition = furniture.transform.position.y;

        return encodedFurniture;
    }

    public Wall EncodeWall(GameObject wall)
    {
        Wall encodedWall = new Wall();
        if (wall.name.Contains("Door"))
            encodedWall.type = "Door";
        else
            encodedWall.type = "Normal";
        encodedWall.side = wall.tag;
        return encodedWall;
    }

    public GameObject DecodeWall(string json)
    {
        return null;
    }
}
