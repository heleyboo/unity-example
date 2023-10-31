using System.Collections.Generic;
using Tuong.Models;
using UnityEngine;

namespace Tuong
{
    public class GameStoryConfig: MonoBehaviour
    {
        public static List<IStory> StoryItems { get; set; }

        public static IStory SelectedCategory { get; set; }
        
        public static IStory SelectedChapter { get; set; }
    }
}