using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelScripts : MonoBehaviour
{

    public TMP_Dropdown dropLevelChosen;
    public TMP_Dropdown chosenLevel;
    public GameObject levelBase;
    public GameObject RoomSelection;
    public GameObject DimensionSelection;

    public void AddLevel()
    {
        if (dropLevelChosen.options.Count > 0)
        {
            var text = dropLevelChosen.options[dropLevelChosen.value].text; 
            if (text != null)
            {
                var level = Instantiate(levelBase, new Vector3(0,0,0), Quaternion.identity);
                level.tag= text;
                level.layer = LayerMask.NameToLayer(text);
                dropLevelChosen.options.RemoveAt(dropLevelChosen.value);
                dropLevelChosen.RefreshShownValue();
                chosenLevel.options.Add(new TMP_Dropdown.OptionData(text));
                if(chosenLevel.options.Count == 1 ) {
                    chosenLevel.value = 0;
                    chosenLevel.value = 1;
                    chosenLevel.value = 0;
                }
                chosenLevel.RefreshShownValue();
            }
            else
            {
                dropLevelChosen.RefreshShownValue();
            }
        }
    }

    public void ChangeLevel()
    {
        var layer = LayerMask.NameToLayer(chosenLevel.options[chosenLevel.value].text);
        var tag = chosenLevel.options[chosenLevel.value].text;
        GameObject.FindGameObjectWithTag(tag).transform.position = new Vector3(0,0,0);
        switch (layer)
        {
            case 6:
                var obj = GameObject.FindGameObjectWithTag("Étage 2");
                if (obj != null)
                {
                    obj.transform.position = new Vector3(50, 0, 0);
                }
                break;
            case 7:
                var obj2 = GameObject.FindGameObjectWithTag("RDC");
                if (obj2 != null)
                {
                    obj2.transform.position = new Vector3(50, 0, 0);
                }
                break;
        }
    }

    public void RemoveLevel()
    {
        if (chosenLevel.options.Count > 0)
        {
            var text = chosenLevel.options[chosenLevel.value].text;
            if (text != null)
            {
                var level = GameObject.FindGameObjectWithTag(text);
                Destroy(level);
                chosenLevel.options.RemoveAt(chosenLevel.value);
                if(chosenLevel.options.Count > 0)
                {
                    chosenLevel.value = 0;
                    chosenLevel.value = 1;
                    chosenLevel.value = 0;
                }
                chosenLevel.RefreshShownValue();
                dropLevelChosen.options.Add(new TMP_Dropdown.OptionData(text));
                if (dropLevelChosen.options.Count == 1)
                {
                    dropLevelChosen.value = 0;
                    dropLevelChosen.value = 1;
                    dropLevelChosen.value = 0;
                }
                dropLevelChosen.RefreshShownValue();

            }
            else
            {
                chosenLevel.RefreshShownValue();
            }
        }
        if(chosenLevel.options.Count == 0)
        {
            RoomSelection.SetActive(false);
            DimensionSelection.SetActive(false);
        }
    }
}
