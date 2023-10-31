using System.Collections.Generic;

namespace Tuong.Models
{
    public class Category: IStory
    {
        public string Name { get; set; }
        public string Description { get; set; }
        private List<Chapter> Chapters { get; set; }
        
        public Category()
        {
            Chapters = new List<Chapter>();
            for (int i = 0; i < 5; i++)
            {
                Chapters.Add(new Chapter() { Name = Faker.Name.FullName(), Description = Faker.Lorem.Sentence()});
            }
        }
    }
}