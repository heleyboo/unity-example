using System.Collections.Generic;

namespace Tuong.Models
{
    public class Chapter: IStory
    {
        public string Name { get; set; }
        public string Description { get; set; }
        private List<Difficulty> Difficulties { get; set; }

        public Chapter()
        {
            Difficulties = new List<Difficulty>();
            for (int i = 0; i < 5; i++)
            {
                Difficulties.Add(new Difficulty() { Name = Faker.Name.FullName(), Description = Faker.Lorem.Sentence()});
            }
        }
    }
}