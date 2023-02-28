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
                chosenRoom.RefreshShownValue();
            }
            else
            {
                dropRoomChosen.RefreshShownValue();
            }
        }
    }

    public void ChangeRoom()
    {
        /*var layer = LayerMask.NameToLayer(chosenLevel.options[chosenLevel.value].text);
        var tag = chosenRoom.options[chosenRoom.value].text;
        GameObject level = GameObject.FindGameObjectWithTag(tag);
        GameObject disappearedLevel;
        switch (layer)
        {
            case 6:
                disappearedLevel = GameObject.FindGameObjectWithTag("Étage 2");
                break;
            case 7:
                disappearedLevel = GameObject.FindGameObjectWithTag("RDC");
                break;
            default:
                disappearedLevel = null;
                break;
        }
        level.SetActive(true);
        disappearedLevel.SetActive(false);*/
    }

    public void ChangedLevel()
    {
        var level = chosenLevel.options[chosenLevel.value].text;
        switch (level)
        {
            case "Étage 2":
                title.text = "Pièces du deuxième étage";
                break;
            case "RDC":
                title.text = "Pièces du rez-de-chaussée";
                break;
            case "Sous-sol":
                title.text = "Pièces du sous-sol";
                break;
        }
    }

    public void SetHeight()
    {

    }
}
