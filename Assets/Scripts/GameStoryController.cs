using System;
using System.Collections.Generic;
using Tuong.Models;
using UnityEngine;
using UnityEngine.UI;

namespace Tuong
{
    public class GameStoryController: MonoBehaviour
    {
        [SerializeField] protected Transform template;
        [SerializeField] protected Transform content;
        protected void Awake()
        {
            InitData();
            for (int i = 0; i < GameStoryConfig.Categories.Count; i++)
            {
                IStory category = GameStoryConfig.Categories[i];
                Transform entryTransform = Instantiate(template, content);
                entryTransform.gameObject.SetActive(true);
            
                entryTransform.Find("Name").GetComponent<Text>().text = category.Name;
            }
        }

        public void InitData()
        {
            GameStoryConfig.Categories = new List<IStory>();

            for (int i = 0; i < 5; i++)
            {
                GameStoryConfig.Categories.Add(new Category() { Name = Faker.Name.FullName(), Description = Faker.Lorem.Sentence()});
            }
        }

    }
}