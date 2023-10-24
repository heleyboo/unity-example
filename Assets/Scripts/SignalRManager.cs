using Microsoft.AspNetCore.SignalR.Client;
using UnityEngine;


namespace Tuong
{
    public class SignalRManager : MonoBehaviour
    {
        private HubConnection hubConnection;

        void Start()
        {
            string hubUrl = "https://localhost:7168/chatHub";

            hubConnection = new HubConnectionBuilder()
                .WithUrl(hubUrl)
                .Build();

            hubConnection.On<string, string>("ReceiveMessage", (user, message) => {
                Debug.Log($"{user} says: {message}");
            });

            hubConnection.StartAsync().ContinueWith(task => {
                if (task.IsFaulted)
                {
                    Debug.LogError("Connection Error: " + task.Exception.GetBaseException());
                }
                else if (task.IsCompleted)
                {
                    Debug.Log("Connected");
                }
                else
                {
                    Debug.Log("Connected to signal R");
                }
            });
        }

        void Update()
        {
            // Your game logic here
        }

        void OnDestroy()
        {
            hubConnection.StopAsync();
        }
    }
}