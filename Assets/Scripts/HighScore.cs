using System.Collections.Generic;
using System.Linq;
using Tuong;
using UnityEngine;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    private Transform entryContainer;

    private Transform entryTemplate;

    private List<LeaderBoardEntry> LeaderBoardEntries;

    public HighScore()
    {
        PopulateList();
    }
    private void Awake()
    {
        entryContainer = transform.Find("HighScoreEntryContainer");
        entryTemplate = entryContainer.transform.Find("HighScoreEntryTemplate");
        
        entryTemplate.gameObject.SetActive(false);
        
        

        float templateHeight = 20f;
        
        for (int i = 0; i < LeaderBoardEntries.Count; i++)
        {
            LeaderBoardEntry item = LeaderBoardEntries[i];
            Transform entryTransform = Instantiate(entryTemplate, entryContainer);
            RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
            entryRectTransform.anchoredPosition = new Vector2(0, (-templateHeight * i) + 30);
            entryTransform.gameObject.SetActive(true);

            entryTransform.Find("PosText").GetComponent<Text>().text = (i + 1).ToString();
            entryTransform.Find("NameText").GetComponent<Text>().text = item.Name;
            entryTransform.Find("ScoreText").GetComponent<Text>().text = item.Score.ToString();
        }
    }

    private void PopulateList()
    {
        List<LeaderBoardEntry> list = new List<LeaderBoardEntry>();
        list.Add(new LeaderBoardEntry() { Name = "Hoan", Score = 12453 });
        list.Add(new LeaderBoardEntry() { Name = "Tuong", Score = 45332 });
        list.Add(new LeaderBoardEntry() { Name = "Sanh", Score = 46456 });
        list.Add(new LeaderBoardEntry() { Name = "Thuat", Score = 23234 });
        list.Add(new LeaderBoardEntry() { Name = "Thang", Score = 465667 });

        LeaderBoardEntries = list.OrderByDescending(l => l.Score).ToList();
    }
}
