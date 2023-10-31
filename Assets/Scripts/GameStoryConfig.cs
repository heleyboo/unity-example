using System.Collections.Generic;
using Tuong.Models;
using UnityEngine;

namespace Tuong
{
    public class GameStoryConfig: MonoBehaviour
    {
        public static List<IStory> Categories { get; set; }

        public static Category SelectedCategory { get; set; }
        
        public static Chapter SelectedChapter { get; set; }

        public GameStoryConfig()
        {
            Categories = new List<IStory>();

            for (int i = 0; i < 5; i++)
            {
                Categories.Add(new Category() { Name = Faker.Name.FullName(), Description = Faker.Lorem.Sentence()});
            }
        }
    }
}