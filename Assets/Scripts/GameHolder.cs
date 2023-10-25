using Tuong.Models;
using UnityEngine;

namespace Tuong
{
    public class GameHolder: MonoBehaviour
    {
        public static Room Room { get; set; }
        public static GameUser GameUser { get; set; }
    }
}