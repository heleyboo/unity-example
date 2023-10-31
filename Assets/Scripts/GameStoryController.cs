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
        [SerializeField] protected Transform contentCategory;
        [SerializeField] protected Button backButton;
        
        private bool OnCategory { get; set; }
        private bool OnChapter { get; set; }
        private bool OnDifficulty { get; set; }
        
        private List<IStory> StoryItems { get; set; }
        
        protected void Awake()
        {
            InitData();
            RenderCategories();
            backButton.onClick.AddListener(BackButtonClicked);
        }

        public void BackButtonClicked()
        {
            Debug.Log("OnDifficulty: " + OnDifficulty);
            Debug.Log("OnChapter: " + OnChapter);
            if (OnDifficulty)
            {
                RenderChapters(GameStoryConfig.SelectedCategory);
            } else if (OnChapter)
            {
                RenderCategories();
            }
        }

        public void RenderList()
        {
            foreach (Transform child in contentCategory) {
                Destroy(child.gameObject);
            }
            for (int i = 0; i < StoryItems.Count; i++)
            {
                IStory story = StoryItems[i];
                Transform entryTransform = Instantiate(template, contentCategory);
                entryTransform.gameObject.SetActive(true);

                if (story is Category)
                {
                    entryTransform.Find("Name").GetComponent<Text>().text = string.Format("Category: {0}", story.Name);
                    entryTransform.GetComponent<Button>().onClick.AddListener(() => RenderChapters(story));
                }
                
                if (story is Chapter)
                {
                    entryTransform.Find("Name").GetComponent<Text>().text = string.Format("Chapter: {0}", story.Name);
                    entryTransform.GetComponent<Button>().onClick.AddListener(() => RenderDifficulty(story));
                }
                
                if (story is Difficulty)
                {
                    entryTransform.Find("Name").GetComponent<Text>().text = string.Format("Difficulty: {0}", story.Name);
                }
            }
        }
        
        public void RenderCategories()
        {
            StoryItems = GameStoryConfig.StoryItems;
            OnChapter = false;
            OnCategory = true;
            OnDifficulty = false;
            RenderList();
            backButton.gameObject.SetActive(false);
        }

        public void RenderChapters(IStory story)
        {
            Category category = (Category) story;
            GameStoryConfig.SelectedCategory = story;
            StoryItems = category.Chapters;
            OnChapter = true;
            OnCategory = false;
            OnDifficulty = false;
            RenderList();
            backButton.gameObject.SetActive(true);
        }
        
        public void RenderDifficulty(IStory story)
        {
            Chapter chapter = (Chapter) story;
            GameStoryConfig.SelectedChapter = story;
            StoryItems = chapter.Difficulties;
            OnChapter = false;
            OnCategory = false;
            OnDifficulty = true;
            RenderList();
            backButton.gameObject.SetActive(true);
        }

        public void InitData()
        {
            GameStoryConfig.StoryItems = new List<IStory>();

            for (int i = 0; i < 5; i++)
            {
                GameStoryConfig.StoryItems.Add(new Category() { Name = Faker.Name.FullName(), Description = Faker.Lorem.Sentence()});
            }
        }

    }
}