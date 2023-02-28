using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dimensions : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TMP_Dropdown chosenRoom;
    public TMP_Dropdown chosenLevel;
    public TMP_InputField width;
    public TMP_InputField lenght;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangedRoom()
    {
        var room = chosenRoom.options[chosenRoom.value].text;
        title.text = "Dimensions de " + room.ToLower();
    }

    public void SetDimensions()
    {
        var layer = LayerMask.NameToLayer(chosenLevel.options[chosenLevel.value].text);
        var tag = chosenRoom.options[chosenRoom.value].text;
        var rooms = GameObject.FindGameObjectsWithTag(tag);
        foreach(var room in rooms)
        {
            if(room.layer == layer)
            {
                float w = float.Parse(width.text);
                float l = float.Parse(lenght.text);
                if(l > 0 && w > 0)
                {
                    room.transform.localScale = new Vector3(w/10, 1, l/10);
                }
                
            }
        }
    }
}
