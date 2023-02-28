using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelScripts : MonoBehaviour
{

    public Transform levelsContent;
    public TMP_Dropdown dropLevelChosen;
    public TMP_Dropdown chosenLevel;

    public void AddLevel()
    {
        if (dropLevelChosen.options.Count > 0)
        {
            var text = dropLevelChosen.options[dropLevelChosen.value].text; 
            if (text != null)
            {
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
        Debug.Log("Fuck");
    }
}
