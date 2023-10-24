using System.Collections;
using System.Collections.Generic;
using System.Text.Json;
using Tuong.Models;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tuong
{
    public class RoomManager: MonoBehaviour
    {
        private List<Room> Rooms { get; set; }
        
        [SerializeField] protected Transform template;
        [SerializeField] protected Transform container;
        public InputField roomNameInput;
        public Button createRoomButton;

        private string apiUrl = "https://localhost:7168/rooms"; // Replace with your API endpoint URL

        void Start()
        {
            Rooms = new List<Room>();
            createRoomButton.onClick.AddListener(CreateRoomClicked);
            StartCoroutine(GetDataFromAPI());
        }

        private void CreateRoomClicked()
        {
            Debug.Log(roomNameInput.text);
            StartCoroutine(CreateRoom());
        }
        
        IEnumerator CreateRoom()
        {
            var room = new CreateRoomRequest() { RoomName = roomNameInput.text };
            string jsonData = JsonSerializer.Serialize<CreateRoomRequest>(room);
            
            // Create a POST request
            UnityWebRequest webRequest = new UnityWebRequest(apiUrl, "POST");
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");

            // Send the request
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string responseText = webRequest.downloadHandler.text;

                Room? roomRes = JsonSerializer.Deserialize<Room>(responseText);

                GameHolder.Room = roomRes;
                
                SceneManager.LoadScene("GameScene");
            }

        }

        IEnumerator GetDataFromAPI()
        {
            UnityWebRequest webRequest = UnityWebRequest.Get(apiUrl);

            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                string responseText = webRequest.downloadHandler.text;
                Rooms = JsonSerializer.Deserialize<List<Room>>(responseText);
                Debug.Log("Response: " + Rooms.Count);
                ShowListRooms();
                // Parse and process the response data here
            }
            else
            {
                Debug.LogError("API Request Error: " + webRequest.error);
            }
        }

        private void ShowListRooms()
        {
            for (int i = 0; i < Rooms.Count; i++)
            {
                Room item = Rooms[i];
                Transform entryTransform = Instantiate(template, container);
                entryTransform.gameObject.SetActive(true);
            
                entryTransform.Find("RoomName").GetComponent<Text>().text = item.code + " - " + item.name;
                entryTransform.Find("JoinButton").GetComponent<Button>().onClick.AddListener(() => JoinGame(item));
            }
        }

        private void JoinGame(Room room)
        {
            GameHolder.Room = room;
            SceneManager.LoadScene("GameScene");
        }
    }
}