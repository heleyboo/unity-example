using System;
using Tuong.Models;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Tuong
{
    public class GameManager: MonoBehaviour
    {
        [SerializeField] protected Transform template;
        [SerializeField] protected Transform container;

        public Button backButton;
        
        
        private void Start()
        {
            Debug.Log("=================");
            Debug.Log(GameHolder.Room?.boardNumber);
            InitBoard();
            backButton.onClick.AddListener(() =>
            {
                SceneManager.LoadScene("RoomScene");
            });
        }

        private void InitBoard()
        {
            for (int i = 0; i < GameHolder.Room.boardNumber.Count; i++)
            {
                int number = GameHolder.Room.boardNumber[i];
                Transform entryTransform = Instantiate(template, container);
                entryTransform.gameObject.SetActive(true);
            
                entryTransform.Find("Text").GetComponent<Text>().text = number.ToString();
            }
        }
    }
}