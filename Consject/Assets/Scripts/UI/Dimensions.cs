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

    }

    public void SetFromWhichSideOfWhatRoom()
    {
        var layer = LayerMask.NameToLayer(chosenLevel.options[chosenLevel.value].text);
        var tag = chosenRoom.options[chosenRoom.value].text;
        var rooms = GameObject.FindGameObjectsWithTag(tag);
        foreach (var room in rooms)
        {
            if (room.layer == layer)
            {
                var scale = GetRoomMeters(choseFromRoom.options[choseFromRoom.value].text);
                var position = GetRoomPosition(choseFromRoom.options[choseFromRoom.value].text);
                switch (chosenFromSide.options[chosenFromSide.value].text)
                {
                    case "Droite":
                        room.transform.position = new Vector3(scale.x * 10 + position.x, position.y, position.z);
                        break;
                    case "Gauche":
                        room.transform.position = new Vector3(- scale.x * 10 + position.x, position.y, position.z);
                        break;
                    case "Haut":
                        room.transform.position = new Vector3(position.x, position.y, scale.z * 10 + position.z);
                        break;
                    case "Bas":
                        room.transform.position = new Vector3(position.x, position.y, - scale.z * 10 + position.z);
                        break;
                }
            }
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
                if (child.tag.ToString() != "Untagged" && child.tag.ToString() != chosenLevel.options[chosenLevel.value].text && child.tag.ToString() != chosenRoom.options[chosenRoom.value].text)
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
        //destroy current room and change it to be a new present of size in dropdown
        if (chosenRoomSize.options[chosenRoomSize.value].text != "Choisir la grosseur désirée")
        {

        }
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
