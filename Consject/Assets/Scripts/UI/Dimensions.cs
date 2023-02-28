using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dimensions : MonoBehaviour
{
    public TextMeshProUGUI title;
    public TMP_Dropdown chosenRoom;
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
}
