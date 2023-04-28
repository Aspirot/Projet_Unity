using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class SaveHouse : MonoBehaviour
{
    private const string URL_HOUSE = "https://unity-ba03.restdb.io/rest/house";

    public TMP_InputField namefield;

    public void SaveLocal()
    {
        if (namefield.text != null && namefield.text != "")
        {
            var house = new JsonConverter().EncodeHouse(namefield.text);
            var jsonData = JsonConvert.SerializeObject(house);
            string path = Application.persistentDataPath + $"/{namefield.text}.json";
            Debug.Log(path);
            using StreamWriter writer = new StreamWriter(path);
            writer.Write(jsonData);
        }
    }

    public void SaveCurrentHouse()
    {   
        if(namefield.text != null && namefield.text != "")
        {
            var request = new UnityWebRequest(URL_HOUSE, "POST");
            var house = new JsonConverter().EncodeHouse(namefield.text);
            var jsonData = JsonConvert.SerializeObject(house);
            var bytes = new System.Text.UTF8Encoding().GetBytes(jsonData);

            request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bytes);
            request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
            request.SetRequestHeader("x-apikey", "09e0e81a6ae0004556ea1fc9e2ba4372e3ac2");
            request.SetRequestHeader("content-type", "application/json");

            StartCoroutine(WaitForRequestHousePOST(request));
        }
    }

    private IEnumerator WaitForRequestHousePOST(UnityWebRequest request)
    {
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Network error");
        }
        else
        {
            Debug.Log("Request reponseCode : " + request.responseCode);
        }
    }

    private IEnumerator WaitForRequestGET(UnityWebRequest request)
    {
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError)
        {
            Debug.Log("Network error");
        }
        else
        {
            if (request.downloadHandler.text != null)
            {
                var json = request.downloadHandler.text.Substring(1, request.downloadHandler.text.Length - 2);
                Debug.Log(json);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    House house = JsonConvert.DeserializeObject<House>(json);
                    if (!string.IsNullOrEmpty(house.name))
                    {
                        Debug.Log("Hello");
                    }
                }
                else
                    Debug.Log("Credentials invalid");
            }
            else
            {
                SceneManager.LoadScene("TD07_1");
            }
        }
    }
}
public struct House
{
    public string _id;
    public string name;
    public List<Level> levels;
}
public struct Level
{
    public string name;
    public float height;
    public List<Room> rooms;
    public List<Furniture> furnitures;
}
public struct Room
{
    public string name;
    public float size;
    public float xposition;
    public float zposition;
    public List<Wall> walls;
}
public struct Furniture
{
    public string type;
    public float xposition;
    public float yposition;
    public float zposition;
}
public struct Wall
{
    public string type;
    public string side;
}