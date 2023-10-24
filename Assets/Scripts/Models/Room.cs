using System;
using System.Collections.Generic;

namespace Tuong.Models
{
    [Serializable]
    public class Room
    {
        public int id { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        
        public string? board { get; set; }
    
        public List<int>? boardNumber { get; set; }
    
        public int? firstPlayerId { get; set; }
    
        public int? secondPlayerId { get; set; }
    }
}