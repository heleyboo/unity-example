using System;
using System.Collections;
using System.Text.Json;
using JetBrains.Annotations;
using Tuong.Models;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tuong
{
    public class AccountManager: MonoBehaviour
    {
        public InputField accountNameInput;
        public Button createAccountButton;

        private readonly string apiUrl = "https://localhost:7168/game-users";

        private void Awake()
        {
            StartCoroutine(CreateAccount(null));
        }

        private void Start()
        {
            // Attach the button's click event to the CreateAccount method
            createAccountButton.onClick.AddListener(UpdateUsername);
        }
        
        private void UpdateUsername()
        {
            // Get the account name from the input field
            string accountName = accountNameInput.text;

            StartCoroutine(UpdateUsername(accountName));
            Debug.Log("Clicked");
        }
        
        IEnumerator CreateAccount([CanBeNull] string accountName)
        {
            var gameUser = GameUser.CreateFromDeviceId(accountName);
            string jsonData = JsonSerializer.Serialize<GameUser>(gameUser);
            
            Debug.Log(jsonData);

            // Create a POST request
            UnityWebRequest webRequest = new UnityWebRequest(apiUrl, "POST");
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");

            // Send the request
            yield return webRequest.SendWebRequest();

            CheckToLoadScene(webRequest);
        }
        
        IEnumerator UpdateUsername(string username)
        {
            var gameUserUpdate = new UpdateGameUser() { username = username };
            string jsonData = JsonSerializer.Serialize<UpdateGameUser>(gameUserUpdate);
            
            // Create a POST request
            UnityWebRequest webRequest = new UnityWebRequest(apiUrl + "/" + SystemInfo.deviceUniqueIdentifier, "POST");
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            webRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");

            // Send the request
            yield return webRequest.SendWebRequest();
            
            CheckToLoadScene(webRequest);
        }

        private void CheckToLoadScene(UnityWebRequest webRequest)
        {
            if (webRequest.isNetworkError || webRequest.isHttpError)
            {
                Debug.LogError("API Request Error: " + webRequest.error);
            }
            else
            {
                // Request was successful, handle the response data here
                string responseText = webRequest.downloadHandler.text;

                GameUser? gameUserRes = JsonSerializer.Deserialize<GameUser>(responseText);

                if (gameUserRes != null && !String.IsNullOrEmpty(gameUserRes.username))
                {
                    SceneManager.LoadScene("RoomScene");
                }
            }
        }

    }
}