using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace Tuong
{
    
    
    public class FlipImageController: MonoBehaviour
    {
        [SerializeField] protected Transform template;
        [SerializeField] protected Transform container;
        [SerializeField] protected Text txtScore;
        [SerializeField] protected Text txtTurnCount;
        [SerializeField] protected Text txtCorrectPairCount;
        [SerializeField] protected Transform newGamePanel;
        [SerializeField] protected Text txtNewGame;
        [SerializeField] protected Button btnNewGame;
        public List<int> Board { get; set; }
        
        public List<Transform> Items { get; set; }
        
        const int TURNS = 20;

        private int firstSelected = -1;
        private int secondSelected = -1;
        private bool canSelect = true;
        private int turns = TURNS;
        private int correctPairs = 0;
        private int score = 0;
        
        // Init board
        public FlipImageController()
        {
            NewGame();
        }

        private void NewGame()
        {
            Board = new List<int>();

            Items = new List<Transform>();
            
            List<int> numbers = Enumerable.Range(0, 10).ToList();

            Random random = new Random();

            // Add each number twice to the list
            foreach (int number in numbers)
            {
                Board.Add(number);
                Board.Add(number);
            }

            // Shuffle the list to randomize positions
            for (int i = Board.Count - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                int temp = Board[i];
                Board[i] = Board[j];
                Board[j] = temp;
            }

            firstSelected = -1;
            secondSelected = -1;
            canSelect = true;
            turns = TURNS;
            correctPairs = 0;
            score = 0;
            InitBoard();
            newGamePanel.gameObject.SetActive(false);
        }

        public void Awake()
        {
            NewGame();
        }

        private void ShowPanelNewGame(bool isWin = true)
        {
            newGamePanel.gameObject.SetActive(true);
            if (isWin)
            {
                txtNewGame.text = "You win!";
            }
            else
            {
                txtNewGame.text = "You lose!";
            }
            btnNewGame.onClick.AddListener(() => NewGame());
        }

        public void InitBoard()
        {
            foreach (Transform child in container) {
                Destroy(child.gameObject);
            }
            for (int i = 0; i < Board.Count; i++)
            {
                Transform entryTransform = Instantiate(template, container);
                entryTransform.gameObject.SetActive(true);
                Items.Add(entryTransform);
                
                int index = i;

                entryTransform.GetComponent<Button>().onClick.AddListener(() => HandleClick(index));
                
                
                // Assuming you have a reference to the sprite you want to set
                var imageObj = entryTransform.Find("Image").GetComponent<Image>();
                // imageObj.sprite = Resources.Load<Sprite>(String.Format("Sprites/{0}", Board[i]));
                imageObj.enabled = true;
            }
        }

        private void Update()
        {
            txtScore.text = score.ToString();
            txtTurnCount.text = turns.ToString();
            txtCorrectPairCount.text = correctPairs.ToString();
            
            if (correctPairs == 10 && turns >= 0)
            {
                ShowPanelNewGame();
            }
            if (turns == 0 && correctPairs < 10)
            {
                ShowPanelNewGame(false);
            }
        }

        public void HandleClick(int i)
        {
            if (!canSelect)
            {
                return;
            }
            Debug.Log("Button clicked");
            var entryTransform = Items[i];
            var imageObj = entryTransform.Find("Image").GetComponent<Image>();
            imageObj.sprite = Resources.Load<Sprite>(String.Format("Sprites/{0}", Board[i]));
            imageObj.enabled = true;
            
            if (firstSelected == -1)
            {
                firstSelected = i;
            }
            else
            {
                secondSelected = i;
                if (Board[firstSelected] == Board[secondSelected])
                {
                    Debug.Log("Matched");
                    firstSelected = -1;
                    secondSelected = -1;
                    UpdateCorrectStatistic();
                }
                else
                {
                    Debug.Log("Not matched");
                    UpdateWrongStatistic();
                    canSelect = false;
                    Invoke("FlipSelectedImages", 0.3f);
                }
            }
        }

        public void UpdateWrongStatistic()
        {
            turns -= 1;
        }
        
        public void UpdateCorrectStatistic()
        {
            score += 1000;
            correctPairs += 1;
            if (correctPairs == 10 && turns >= 0)
            {
                score += turns * 100;
            }
        }
        

        public void FlipSelectedImages()
        {
            FlipImage(firstSelected);
            FlipImage(secondSelected);
            firstSelected = -1;
            secondSelected = -1;
            canSelect = true;
        }

        public void FlipImage(int index)
        {
            var entryTransform = Items[index];
            var imageObj = entryTransform.Find("Image").GetComponent<Image>();
            imageObj.sprite = Resources.Load<Sprite>("Sprites/100");
        }

        public void FlipAll()
        {
            foreach (var entryTransform in Items)
            {
                var imageObj = entryTransform.Find("Image").GetComponent<Image>();
                imageObj.sprite = Resources.Load<Sprite>("Sprites/100");
            }
        }
        
    }
}