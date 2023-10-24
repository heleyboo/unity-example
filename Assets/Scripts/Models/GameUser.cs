using System;
using JetBrains.Annotations;
using UnityEngine.Device;

namespace Tuong.Models
{
    [Serializable]
    public class GameUser
    {
        public int id { get; set; }
    
        public string? username { get; set; }
    
        public string deviceId { get; set; }
        
        public static GameUser CreateFromDeviceId([CanBeNull] string username)
        {
            return new GameUser() { deviceId = SystemInfo.deviceUniqueIdentifier, username = username};
        }
    }
}