using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UIElements;

public class LoadHouse : MonoBehaviour
{

    private const string URL_HOUSE = "https://unity-ba03.restdb.io/rest/house";

    public TMP_Dropdown HousesDb;

    public GameObject levelBase;
    public GameObject bigRoom;
    public GameObject mediumRoom;
    public GameObject smallRoom;

    public GameObject bath;
    public GameObject bed;
    public GameObject fridge;
    public GameObject chair;
    public GameObject kitchen;
    public GameObject armchair;
    public GameObject table;
    public GameObject pcchair;
    public GameObject pot2;
    public GameObject shelf;
    public GameObject closet;
    public GameObject sink;
    public GameObject washer;
    public GameObject carpet;
    public GameObject carpetb;

    public GameObject smallWall;
    public GameObject mediumWall;
    public GameObject bigWall;
    public GameObject smallDoorWall;
    public GameObject mediumDoorWall;
    public GameObject bigDoorWall;

    private void Start()
    {
        
    }

    public void LoadHousesFromDb()
    {
        var request = new UnityWebRequest(URL_HOUSE, "GET");
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("content-type", "application/json");
        request.SetRequestHeader("x-apikey", "09e0e81a6ae0004556ea1fc9e2ba4372e3ac2");

        StartCoroutine(WaitForRequestGetAll(request));
    }

    public void LoadHouseFromLocal()
    {
        string path = Application.persistentDataPath + "/House.json";
        using StreamReader reader = new StreamReader(path);

        var jsonData = reader.ReadToEnd();
        var house = JsonConvert.DeserializeObject<House>(jsonData);
        if (!string.IsNullOrEmpty(house.name))
        {
            DecodeHouse(house);
        }
    }
    
    public void LoadHouseFromDb()
    {
        string parameters = "?q={\"name\":\"" + HousesDb.options[HousesDb.value].text + "\" }";
        var request = new UnityWebRequest(URL_HOUSE + parameters, "GET");
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("content-type", "application/json");
        request.SetRequestHeader("x-apikey", "09e0e81a6ae0004556ea1fc9e2ba4372e3ac2");

        StartCoroutine(WaitForRequestGetOne(request));
    }

    private IEnumerator WaitForRequestGetAll(UnityWebRequest request)
    {
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Network error");
        }
        else
        {
            if (request.downloadHandler.text != null)
            {
                List<House> houses = JsonConvert.DeserializeObject<List<House>>(request.downloadHandler.text);
                if (houses.Any())
                {
                    Debug.Log("Houses found");
                    HousesDb.options = new List<TMP_Dropdown.OptionData>
                    {
                        new TMP_Dropdown.OptionData("Choisir une maison")
                    };
                    foreach (House house in houses)
                    {
                        HousesDb.options.Add(new TMP_Dropdown.OptionData(house.name));
                    }
                    
                }
                else
                    Debug.Log("No houses found");
            }
            else
            {
                Debug.Log("Error");
            }
        }
    }
    private IEnumerator WaitForRequestGetOne(UnityWebRequest request)
    {
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Network error");
        }
        else
        {
            if (request.downloadHandler.text != null)
            {
                var json = request.downloadHandler.text.Substring(1, request.downloadHandler.text.Length - 2);
                Debug.Log(json);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    House house = JsonConvert.DeserializeObject<House>(json);
                    if (!string.IsNullOrEmpty(house.name))
                    {
                        DecodeHouse(house);
                    }
                }
                else
                    Debug.Log("No house with name found");
            }
            else
            {
                Debug.Log("Error");
            }
        }
    }

    public void DecodeHouse(House house)
    {
        var rdc = GameObject.FindGameObjectWithTag("RDC");
        if (rdc != null)
        {
            Destroy(rdc);
        }
        foreach (var level in house.levels)
        {
            DecodeLevel(level);
        }
    }

    public void DecodeLevel(Level level)
    {
        switch (level.name)
        {
            case "RDC":
                Rooms.RdcHeight = level.height;
                break;
            case "Étage 2":
                Rooms.SecondFloorHeight = level.height;
                break;
            default:
                break;
        }
        var levelObj = Instantiate(levelBase, new Vector3(0, 0, 0), Quaternion.identity);
        levelObj.tag = level.name;
        levelObj.layer = LayerMask.NameToLayer(level.name);
        foreach(var room in  level.rooms)
        {
            DecodeRoom(room, levelObj);
        }
        foreach (var furniture in level.furnitures)
        {
            DecodeFurniture(furniture, levelObj);
        }
    }
    public void DecodeRoom(Room room, GameObject parent)
    {
        GameObject roomSize = null;
        switch (room.size)
        {
            case 0.5F:
                roomSize = smallRoom;
                break;
            case 0.75F:
                roomSize = mediumRoom;
                break;
            case 1:
                roomSize = bigRoom;
                break;
        }
        var newRoom = Instantiate(roomSize, new Vector3(room.xposition, 0, room.zposition), Quaternion.identity);
        newRoom.layer = parent.layer;
        newRoom.tag = parent.tag;
        newRoom.transform.parent = parent.transform;

        foreach(var wall in room.walls)
        {
            DecodeWall(wall, newRoom);
        }
    }

    public void DecodeFurniture(Furniture furniture, GameObject parent)
    {
        GameObject pref = null;
        switch (furniture.type)
        {
            case "Bath":
                pref = bath;
                break;
            case "Bed":
                pref = bed;
                break;
            case "Fridge":
                pref = fridge;
                break;
            case "Chair":
                pref = chair;
                break;
            case "Kitchen":
                pref = kitchen;
                break;
            case "ArmChair":
                pref = armchair;
                break;
            case "Table":
                pref = table;
                break;
            case "Pc Chair":
                pref = pcchair;
                break;
            case "Pot2":
                pref = pot2;
                break;
            case "Shelf":
                pref = shelf;
                break;
            case "Closet":
                pref = closet;
                break;
            case "Sink":
                pref = sink;
                break;
            case "Washer":
                pref = washer;
                break;
            case "Carpet":
                pref = carpet;
                break;
            case "CarpetB":
                pref = carpetb;
                break;
        }

        var newObj = Instantiate(pref, new Vector3(furniture.xposition, furniture.yposition, furniture.zposition), Quaternion.identity);
        newObj.tag = "Furniture";
        newObj.layer = parent.layer;
        newObj.transform.parent = parent.transform;
    }

    public void DecodeWall(Wall wall, GameObject parent)
    {
        var layer = parent.layer;
        
        if (parent != null)
        {
            GameObject wallToSet = null;
            var edge = 0.0F;
            switch (parent.transform.localScale.x)
            {
                case 0.5F:
                    if (wall.type == "Normal")
                        wallToSet = smallWall;
                    else wallToSet = smallDoorWall;
                    edge = 2.5F;
                    break;
                case 0.75F:
                    if (wall.type == "Normal")
                        wallToSet = mediumWall;
                    else wallToSet = mediumDoorWall;
                    edge = 3.75F;
                    break;
                case 1:
                    if (wall.type == "Normal")
                        wallToSet = bigWall;
                    else wallToSet = bigDoorWall;
                    edge = 5F;
                    break;
            }
            //TODO - get appropriate x and z depending on wall side
            var x = 0.0F;
            var z = 0.0F;
            var rotate = false;
            switch (wall.side)
            {
                case "Mur Droite":
                    rotate = true;
                    x = edge;
                    break;
                case "Mur Gauche":
                    rotate = true;
                    x = -edge;
                    break;
                case "Mur Haut":
                    z = edge;
                    break;
                case "Mur Bas":
                    z = -edge;
                    break;
            }


            GameObject newWall = null;

            switch (wall.type)
            {
                case "Normal":
                    newWall = Instantiate(wallToSet, new Vector3(parent.transform.position.x + x, parent.transform.position.y + 2, parent.transform.position.z + z), Quaternion.identity);
                    break;
                case "Door":
                    newWall = Instantiate(wallToSet, new Vector3(parent.transform.position.x + x, parent.transform.position.y, parent.transform.position.z + z), Quaternion.identity);
                    break;
            }
            if (rotate)
            {
                newWall.transform.Rotate(new Vector3(0, 90, 0));
            }
            newWall.tag = wall.side;
            newWall.layer = layer;
            newWall.transform.parent = parent.transform;
        }
    }
}
