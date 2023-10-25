using System;
using Microsoft.AspNetCore.SignalR.Client;
using UnityEngine;


namespace Tuong
{
    public class SignalRManager : MonoBehaviour
    {
        private HubConnection hubConnection;

        async void Start()
        {
            string hubUrl = "https://localhost:7168/gameHub";

            hubConnection = new HubConnectionBuilder()
                .WithUrl(hubUrl)
                .Build();

            hubConnection.On<string>("PlayerJoined", (connectionId) => {
                Debug.Log($"{connectionId} joined");
            });
            
            try
            {
                await hubConnection.StartAsync();
                Debug.Log("Connected to SignalR server");

                // Join the specified room
                await hubConnection.InvokeAsync("JoinRoom", GameHolder.Room.code, GameHolder.Room.name);
                Debug.Log("Joined room: " + GameHolder.Room.code);
            }
            catch (Exception ex)
            {
                Debug.LogError("SignalR connection error: " + ex.Message);
            }

            // hubConnection.StartAsync().ContinueWith(task => {
            //     if (task.IsFaulted)
            //     {
            //         Debug.LogError("Connection Error: " + task.Exception.GetBaseException());
            //     }
            //     else if (task.IsCompleted)
            //     {
            //         Debug.Log("Connected");
            //         hubConnection.On<string>("PlayerJoined", (connectionId) => {
            //             Debug.Log($"{connectionId} joined");
            //         });
            //     }
            //     else
            //     {
            //         Debug.Log("Connected to signal R");
            //     }
            // });
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