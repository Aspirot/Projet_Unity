using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AddObject : MonoBehaviour
{
    public TMP_Dropdown chosenLevel;

    public void AddPrefab(GameObject obj)
    {
        var y = 0F;
        switch (obj.name)
        {
            case "Bath":
                y = 2.3F;
                break;
            case "Bed":
                y = 0.75F;
                break;
            case "Fridge":
                y = 1.12F;
                break;
            case "Chair":
                y = 0.6F;
                break;
            case "Kitchen":
                y = 1.435F;
                break;
            case "ArmChair":
                y = 1.03F;
                break;
            case "Table":
                y = 0.415F;
                break;
            case "Pc Chair":
                y = 0.559F;
                break;
            case "Pot2":
                y = 0.25F;
                break;
            case "Shelf":
                y = 1F;
                break;
            case "Closet":
                y = 0.65F;
                break;
            case "Sink":
                y = 1.196F;
                break;
            case "Washer":
                y = 0.559F;
                break;
        }

        var newObj = Instantiate(obj, new Vector3(0F,y,0F), Quaternion.identity);
        var layer = LayerMask.NameToLayer(chosenLevel.options[chosenLevel.value].text);
        newObj.layer = layer;
        newObj.transform.parent = GameObject.FindGameObjectWithTag(chosenLevel.options[chosenLevel.value].text).transform;
    }

    public void Rotate()
    {
        if (Dragable.SelectedObject != null)
        {
            Dragable.SelectedObject.transform.Rotate(0, 270, 0);
        }
    }
}
