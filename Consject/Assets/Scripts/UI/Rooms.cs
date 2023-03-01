using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class Rooms : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TMP_Dropdown dropRoomChosen;
    public TMP_Dropdown chosenLevel;
    public TMP_Dropdown chosenRoom;
    public TMP_InputField height;
    public GameObject roomBase;
    public TMP_Dropdown choseFromRoom;
    public GameObject DimensionSelection;

    private List<TMP_Dropdown.OptionData> rdcChoices = new List<TMP_Dropdown.OptionData>()
    {
        new TMP_Dropdown.OptionData("Entrée"),
        new TMP_Dropdown.OptionData("Salon"),
        new TMP_Dropdown.OptionData("Cuisine"),
        new TMP_Dropdown.OptionData("Salle à manger"),
        new TMP_Dropdown.OptionData("Salle de bain"),
        new TMP_Dropdown.OptionData("Chambre"),
        new TMP_Dropdown.OptionData("Chambre"),
        new TMP_Dropdown.OptionData("Couloir"),
        new TMP_Dropdown.OptionData("Salle d'eau"),
        new TMP_Dropdown.OptionData("Escalier")
    };
    private List<TMP_Dropdown.OptionData> secondChoices = new List<TMP_Dropdown.OptionData>()
    {
        new TMP_Dropdown.OptionData("Chambre 1"),
        new TMP_Dropdown.OptionData("Chambre 2"),
        new TMP_Dropdown.OptionData("Chambre 3"),
        new TMP_Dropdown.OptionData("Chambre 4"),
        new TMP_Dropdown.OptionData("Chambre 5"),
        new TMP_Dropdown.OptionData("Couloir"),
        new TMP_Dropdown.OptionData("Salle de bain")
    };
    private List<TMP_Dropdown.OptionData> underChoices;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddRoom()
    {
        if (dropRoomChosen.options.Count > 0)
        {
            var text = dropRoomChosen.options[dropRoomChosen.value].text;
            if (text != null)
            {
                var room = Instantiate(roomBase, new Vector3(0, 0, 0), Quaternion.identity);
                room.tag = text;
                room.layer = LayerMask.NameToLayer(chosenLevel.options[chosenLevel.value].text);
                room.transform.parent = GameObject.FindGameObjectWithTag(chosenLevel.options[chosenLevel.value].text).transform;
                dropRoomChosen.options.RemoveAt(dropRoomChosen.value);
                dropRoomChosen.RefreshShownValue();
                chosenRoom.options.Add(new TMP_Dropdown.OptionData(text));
                if (chosenRoom.options.Count == 1)
                {
                    chosenRoom.value = 0;
                    chosenRoom.value = 1;
                    chosenRoom.value = 0;
                }
                else
                {
                    SetFromWhichRoomDropdown();
                }
                chosenRoom.RefreshShownValue();
                
            }
            else
            {
                dropRoomChosen.RefreshShownValue();
            }
        }
    }

    public void ChangedLevel()
    {
        var level = chosenLevel.options[chosenLevel.value].text;
        switch (level)
        {
            case "Étage 2":
                title.text = "Pièces du deuxième étage";
                chosenRoom.options = new List<TMP_Dropdown.OptionData>();
                dropRoomChosen.options = secondChoices;
                break;
            case "RDC":
                title.text = "Pièces du rez-de-chaussée";
                chosenRoom.options = new List<TMP_Dropdown.OptionData>();
                dropRoomChosen.options = rdcChoices;
                break;
            case "Sous-sol":
                title.text = "Pièces du sous-sol";
                chosenRoom.options = new List<TMP_Dropdown.OptionData>();
                dropRoomChosen.options = underChoices;
                break;
        }
        SetDropDownRoomChoices();
        if(chosenRoom.options.Count > 0)
        {
            DimensionSelection.SetActive(true);
        }
    }

    public void SetHeight()
    {

    }

    public void RemoveRoom()
    {
        if (chosenRoom.options.Count > 0)
        {
            var text = chosenRoom.options[chosenRoom.value].text;
            if (text != null)
            {
                var rooms = GameObject.FindGameObjectsWithTag(text);
                foreach(var room in rooms)
                {
                    var layer = LayerMask.NameToLayer(chosenLevel.options[chosenLevel.value].text);
                    if (room.layer == layer)
                    {
                        Destroy(room);
                        chosenRoom.options.RemoveAt(chosenRoom.value);
                        if (chosenRoom.options.Count > 0)
                        {
                            chosenRoom.value = 0;
                            chosenRoom.value = 1;
                            chosenRoom.value = 0;
                        }
                        chosenRoom.RefreshShownValue();
                        dropRoomChosen.options.Add(new TMP_Dropdown.OptionData(text));
                        if (dropRoomChosen.options.Count == 1)
                        {
                            dropRoomChosen.value = 0;
                            dropRoomChosen.value = 1;
                            dropRoomChosen.value = 0;
                        }
                        dropRoomChosen.RefreshShownValue();
                    }
                }
            }
            else
            {
                chosenRoom.RefreshShownValue();
            }
        }
        if (chosenRoom.options.Count == 0)
        {
            DimensionSelection.SetActive(false);
        }
    }

    private void SetDropDownRoomChoices()
    {
        var parent = GameObject.FindGameObjectWithTag(chosenLevel.options[chosenLevel.value].text);
        if (parent != null)
        {
            foreach(var child in parent.GetComponentsInChildren<Transform>())
            {
                if(child.tag.ToString() != "Untagged" && child.tag.ToString() != chosenLevel.options[chosenLevel.value].text)
                {
                    chosenRoom.options.Add(new TMP_Dropdown.OptionData(child.tag));
                    if (chosenRoom.options.Count == 1)
                    {
                        chosenRoom.value = 0;
                        chosenRoom.value = 1;
                        chosenRoom.value = 0;
                    }
                }
            }
            chosenRoom.RefreshShownValue();
            dropRoomChosen.RefreshShownValue();
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
}
