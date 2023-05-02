using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CoinInteraction : MonoBehaviour
{
    public int points;
    public TMP_Text score;

    public float interactionDistance = 2f;

    private Camera mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        points= 0;
        score.text= "Score: " + points;

        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;


            if (Physics.Raycast(mainCamera.transform.position, mainCamera.transform.forward, out hit, interactionDistance))
            {
                if (hit.collider.CompareTag("Coin"))
                {
                    points += 10;
                    score.text = "Score: " + points;
                    hit.collider.GetComponent<CoinSpawn>().CoinFound();

                }
            }
        }
        
    }
}
