using System.Collections.Generic;
using System.Linq;
using Faker;
using Tuong;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    private Transform entryContainer;

    private Transform entryTemplate;

    private List<LeaderBoardEntry> LeaderBoardEntries;
    
    [SerializeField] protected Transform template;
    [SerializeField] protected Transform scrollview;
    

    public HighScore()
    {
        PopulateList();
    }
    private void Awake()
    {
        entryContainer = transform.Find("HighScoreEntryContainer");
        // entryTemplate = entryContainer.transform.Find("HighScoreEntryTemplate");
        //
        // entryTemplate.gameObject.SetActive(false);
        
        

        float templateHeight = 66.5f;
        
        for (int i = 0; i < LeaderBoardEntries.Count; i++)
        {
            LeaderBoardEntry item = LeaderBoardEntries[i];
            Transform entryTransform = Instantiate(template, scrollview);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, (-templateHeight * i) + 90);
            entryTransform.gameObject.SetActive(true);
            
            entryTransform.Find("Name").GetComponent<Text>().text = item.Name;
            entryTransform.Find("Score").GetComponent<Text>().text = item.Score.ToString();
        }
    }

    private void PopulateList()
    {
        List<LeaderBoardEntry> list = new List<LeaderBoardEntry>();
        for (int i = 0; i < 100; i++)
        {
            list.Add(new LeaderBoardEntry() { Name = Name.FullName(NameFormats.WithPrefix), Score = Faker.RandomNumber.Next() });
        }

        LeaderBoardEntries = list.OrderByDescending(l => l.Score).ToList();
    }
}
