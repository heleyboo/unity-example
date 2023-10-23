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
    [SerializeField] protected Transform container;
    

    public HighScore()
    {
        PopulateList();
    }
    private void Awake()
    {
        entryContainer = transform.Find("HighScoreEntryContainer");
      
        for (int i = 0; i < LeaderBoardEntries.Count; i++)
        {
            LeaderBoardEntry item = LeaderBoardEntries[i];
            Transform entryTransform = Instantiate(template, container);
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
