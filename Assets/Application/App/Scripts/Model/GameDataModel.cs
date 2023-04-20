using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yourvrexperience.Utils;

namespace Innovamat.IdentificationNumber
{
    /* *********************************************
		Class GameDataModel
		
		-SOLID Principle ("S" Single Responsibility principle):
		
			Class responsible of managing the game data

		-Dessign Pattern Singleton
		-Dessign Pattern MVC ("M" Model)
	*/
    [CreateAssetMenu(menuName = "Game/GameDataModel")]
	public class GameDataModel : ScriptableObject
    {
        public const int MaxAllowedRange = 100000000;
        public const int MaxNumbersToFind = 3;
        public const int MaxOptionsAvailable = 7;
        public const int MaxDelayTime = 5;

	    private static GameDataModel _instance;
        public static GameDataModel Instance
        {
            get { return _instance; }
        }

        [Tooltip("Total of numbers to guess (max 3)")]
		[SerializeField] private int totalNumberToFind = 1;
        [Tooltip("Total of options to show as possible answers (max 7)")]
		[SerializeField] private int totalOptionsToShow = 3;
        [Tooltip("Maximum random number generated (max 100000000)")]
        [SerializeField] private int maximumValueToFind = 1000;
        [Tooltip("Showing animation time (max 10 seconds)")]
        [SerializeField] private float delayToShow = 2;
        [Tooltip("Stay quiet before next transtition time (max 5 seconds)")]
        [SerializeField] private float delayToStay = 2;
        [Tooltip("Fadeout animation time (max 10 seconds)")]
        [SerializeField] private float delayToDisappear = 2;
        [Tooltip("Language to use (available 'es' (spanish) and 'en' (english))")]
        [SerializeField] private string codeLanguage = "es"; // "es" (spanish), "en" (english)

        private int _scoreSuccess = 0;
        private int _scoreMistakes = 0;
        private int _gamesPlayed = 0;

        private List<int> _numbers;

        private List<int> _responses;

        public int ScoreSuccess
        {
            get { return _scoreSuccess; }
            set { _scoreSuccess = value; }
        }        
        public int ScoreMistakes
        {
            get { return _scoreMistakes; }
            set { _scoreMistakes = value; }
        }
        public List<int> Numbers
        {
            get { return _numbers; }
        }
        public List<int> Responses
        {
            get { return _responses; }
        }
        public float DelayToShow
        {
            get { 
                if (delayToShow > MaxDelayTime)
                {
                    return MaxDelayTime;
                }
                else
                {
                    return delayToShow; 
                }                
            }
        }
        public float DelayToStay
        {
            get { 
                if (delayToStay > MaxDelayTime)
                {
                    return MaxDelayTime;
                }
                else
                {
                    return delayToStay; 
                }                
            }
        }
        public float DelayToDisappear
        {
            get { 
                if (delayToDisappear > MaxDelayTime)
                {
                    return MaxDelayTime;
                }
                else
                {
                    return delayToDisappear; 
                }                
            }
        }
        public string CodeLanguage
        {
            get { return codeLanguage; }
        }

        public void Initialize()
        {
            _instance = this;
            _scoreSuccess = 0;
            _scoreMistakes = 0;
        }

        private int GenerateRandomNumber()
        {
            if (maximumValueToFind > MaxAllowedRange)
            {
                maximumValueToFind = MaxAllowedRange;
            }
            return UnityEngine.Random.Range(0, maximumValueToFind);
        }

        public void CreateNumberToFind(int totalNumbers = -1)
        {
            if (totalNumbers == -1)
            {
                totalNumbers = totalNumberToFind;
            }
            if (totalNumbers > MaxNumbersToFind)
            {
                totalNumbers = MaxNumbersToFind;
            }
            _numbers = new List<int>();
            while (_numbers.Count < totalNumbers)
            {
                int newNumber = GenerateRandomNumber();
                if (!_numbers.Contains(newNumber))
                {
                    _numbers.Add(newNumber);
                }
            }      
            _gamesPlayed++;
        }

        public void GenerateAnswers(int totalAnswers = -1)
        {
            if (totalAnswers == -1)
            {
                totalAnswers = totalOptionsToShow;
            }
            if (totalAnswers > MaxOptionsAvailable)
            {
                totalAnswers = MaxOptionsAvailable;
            }
            _responses = new List<int>();
            int numberToGenerate = totalAnswers - _numbers.Count;
            while (_responses.Count < numberToGenerate)
            {
                int newFalseOption = GenerateRandomNumber();
                if (!_numbers.Contains(newFalseOption) && (!_responses.Contains(newFalseOption)))
                {
                    _responses.Add(newFalseOption);
                }
            }
            for (int i = 0; i < _numbers.Count; i++)
            {
                float randomValue = UnityEngine.Random.Range(0, 100);
                if (randomValue < 15)
                {
                    _responses.Insert(0, _numbers[i]);
                }
                else
                if (randomValue > 85)
                {
                    _responses.Add(_numbers[i]);
                }
                else
                if (randomValue > 50)
                {
                    _responses.Insert((int)(_responses.Count - (_gamesPlayed % _responses.Count)), _numbers[i]);
                }
                else
                {
                    _responses.Insert((int)(_gamesPlayed % _responses.Count), _numbers[i]);
                }
            }
        }
	}
}