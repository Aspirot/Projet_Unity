using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dimensions : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TMP_Dropdown chosenRoom;
    public TMP_Dropdown chosenLevel;
    public TMP_Dropdown chosenFromSide;
    public TMP_Dropdown choseFromRoom;
    public TMP_Dropdown chosenRoomSize;
    public TMP_Dropdown wallSide;
    public TMP_Dropdown wallType;

    public GameObject smallWall;
    public GameObject mediumWall;
    public GameObject bigWall;
    public GameObject smallDoorWall;
    public GameObject mediumDoorWall;
    public GameObject bigDoorWall;

    public GameObject smallRoom;
    public GameObject mediumRoom;
    public GameObject bigRoom;

    private readonly IList<string> roomTags = new List<string>()
    {
        "Entrée","Salon", "Cuisine", "Salle à manger", "Salle de bain", "Chambre", "Couloir", "Salle d'eau", "Escalier", "Chambre 1", "Chambre 2", "Chambre 3", "Chambre 4", "Chambre 5"
    };

    // Start is called before the first frame update
    void Start()
    {
        choseFromRoom.options = new List<TMP_Dropdown.OptionData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangedRoom()
    {
        var room = chosenRoom.options[chosenRoom.value].text;
        title.text = "Dimensions de " + room.ToLower();
        SetFromWhichRoomDropdown();
    }

    public void ChangeWall()
    {
        var layer = LayerMask.NameToLayer(chosenLevel.options[chosenLevel.value].text);
        var type = wallType.options[wallType.value].text;
        var roomTag = chosenRoom.options[chosenRoom.value].text;
        var rooms = GameObject.FindGameObjectsWithTag(roomTag);
        var wallTag = "Mur " + wallSide.options[wallSide.value].text;
        GameObject roomToWall = null;
        //Destroy ancient wall
        foreach (var room in rooms)
        {
            if (room.layer == layer)
            {
                roomToWall = room;
                foreach (var child in room.GetComponentsInChildren<Transform>())
                {
                    if (child.tag == wallTag)
                    {
                        Destroy(child.gameObject);
                    }
                }
            }
        }
        if(roomToWall != null)
        {
            var roomScale = GetRoomMeters(roomToWall.tag);
            GameObject wallToSet = null;
            var edge = 0.0F;
            switch (roomScale.x)
            {
                case 0.5F:
                    if(type == "Normal")
                        wallToSet = smallWall;
                    else wallToSet= smallDoorWall;
                    edge = 2.5F;
                    break;
                case 0.75F:
                    if (type == "Normal")
                        wallToSet = mediumWall;
                    else wallToSet = mediumDoorWall;
                    edge = 3.75F;
                    break; 
                case 1:
                    if (type == "Normal")
                        wallToSet = bigWall;
                    else wallToSet = bigDoorWall;
                    edge = 5F;
                    break;
            }
            //TODO - get appropriate x and z depending on wall side
            var x = 0.0F;
            var z = 0.0F;
            var rotate = false;
            switch (wallTag)
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

            switch (type)
            {
                case "Normal":
                    newWall = Instantiate(wallToSet, new Vector3(roomToWall.transform.position.x + x, roomToWall.transform.position.y + 2, roomToWall.transform.position.z + z), Quaternion.identity);
                    break;
                case "Porte":
                    newWall = Instantiate(wallToSet, new Vector3(roomToWall.transform.position.x + x, roomToWall.transform.position.y, roomToWall.transform.position.z + z), Quaternion.identity);
                    break;
                case "Aucun":
                    return;
            }
            if(rotate)
            {
                newWall.transform.Rotate(new Vector3(0, 90, 0));
            }
            newWall.tag = wallTag;
            newWall.layer = layer;
            newWall.transform.parent = roomToWall.transform;
        }
    }

    public void Rotate()
    {
        var tag = chosenRoom.options[chosenRoom.value].text;
        var room = GetRoomFromTag(tag);
        room.transform.Rotate(new Vector3(0, 270, 0));
    }

    public void SetFromWhichSideOfWhatRoom()
    {
        var tag = chosenRoom.options[chosenRoom.value].text;
        var room = GetRoomFromTag(tag);
        var moveBy = 0F;
        switch (GetRoomMeters(choseFromRoom.options[choseFromRoom.value].text).x)
        {
            case 0.5F:
                moveBy = 2.5F;
                break;
            case 0.75F:
                moveBy = 3.75F;
                break;
            case 1:
                moveBy = 5F;
                break;
        }
        switch (GetRoomMeters(chosenRoom.options[chosenRoom.value].text).x)
        {
            case 0.5F:
                moveBy += 2.5F;
                break;
            case 0.75F:
                moveBy += 3.75F;
                break;
            case 1:
                moveBy += 5F;
                break;
        }

        var position = GetRoomPosition(choseFromRoom.options[choseFromRoom.value].text);
        switch (chosenFromSide.options[chosenFromSide.value].text)
        {
            case "Droite":
                room.transform.position = new Vector3(moveBy + position.x, position.y, position.z);
                break;
            case "Gauche":
                room.transform.position = new Vector3(-moveBy + position.x, position.y, position.z);
                break;
            case "Haut":
                room.transform.position = new Vector3(position.x, position.y, moveBy + position.z);
                break;
            case "Bas":
                room.transform.position = new Vector3(position.x, position.y, -moveBy + position.z);
                break;
        }
    }

    private void SetFromWhichRoomDropdown()
    {
        var parent = GameObject.FindGameObjectWithTag(chosenLevel.options[chosenLevel.value].text);
        choseFromRoom.options = new List<TMP_Dropdown.OptionData>();
        if (parent != null)
        {
            foreach (var child in parent.GetComponentsInChildren<Transform>())
            {
                if (roomTags.Contains(child.tag) && child.tag.ToString() != chosenRoom.options[chosenRoom.value].text)
                {
                    choseFromRoom.options.Add(new TMP_Dropdown.OptionData(child.tag));
                    if (choseFromRoom.options.Count == 1)
                    {
                        choseFromRoom.value = 0;
                        choseFromRoom.value = 1;
                        choseFromRoom.value = 0;
                    }
                }
            }
            choseFromRoom.RefreshShownValue();
        }
    }

    public void SetRoomSize()
    {
        GameObject roomSize = null;
        switch (chosenRoomSize.options[chosenRoomSize.value].text)
        {
            case "Choisir la grosseur désirée":
                return;
            case "Petit":
                roomSize = smallRoom;
                break;
            case "Moyen":
                roomSize = mediumRoom;
                break;
            case "Grand":
                roomSize = bigRoom;
                break;
        }
        var roomToRemove = GetRoomFromTag(chosenRoom.options[chosenRoom.value].text);
        var tag = roomToRemove.tag;
        var layer = roomToRemove.layer;
        var position = roomToRemove.transform.position;
        Destroy(roomToRemove);
        var newRoom = Instantiate(roomSize, new Vector3(position.x, 0, position.z), Quaternion.identity);
        newRoom.layer = layer;
        newRoom.tag = tag;
        newRoom.transform.parent = GameObject.FindGameObjectWithTag(chosenLevel.options[chosenLevel.value].text).transform;
    }

    private GameObject GetRoomFromTag(string roomName)
    {
        var layer = LayerMask.NameToLayer(chosenLevel.options[chosenLevel.value].text);
        var rooms = GameObject.FindGameObjectsWithTag(roomName);
        foreach (var room in rooms)
        {
            if (room.layer == layer)
            {
                return room;
            }
        }
        return null;
    }

    private Vector3 GetRoomMeters(string roomName)
    {
        var layer = LayerMask.NameToLayer(chosenLevel.options[chosenLevel.value].text);
        var rooms = GameObject.FindGameObjectsWithTag(roomName);
        Vector3 scale = new Vector3(0,0,0);
        foreach (var room in rooms)
        {
            if (room.layer == layer)
            {
                scale = room.transform.localScale;
            }
        }
        return scale;
    }

    private Vector3 GetRoomPosition(string roomName)
    {
        var layer = LayerMask.NameToLayer(chosenLevel.options[chosenLevel.value].text);
        var rooms = GameObject.FindGameObjectsWithTag(roomName);
        Vector3 position = new Vector3(0, 0, 0);
        foreach (var room in rooms)
        {
            if (room.layer == layer)
            {
                position = room.transform.position;
            }
        }
        return position;
    }
}
