using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using yourvrexperience.Utils;

namespace Innovamat.IdentificationNumber
{
	/* *********************************************
		Class MainAppController
		
		-SOLID Principle ("S" Single Responsibility principle):
		
			Class responsible of running the multiple game states 
			and providing access to data and interface resources

		-Dessign Pattern Singleton
		-Dessign Pattern MVC ("C" Controller)
	*/
	public class MainAppController : MonoBehaviour
	{
		public enum StatesApp { MainMenu = 0, ShowNumber, AnswerNumber }

        private static MainAppController _instance;

        public static MainAppController Instance
        {
            get
            {
                if (!_instance)
                {
                    _instance = GameObject.FindObjectOfType(typeof(MainAppController)) as MainAppController;
                }
                return _instance;
            }
        }

		[SerializeField] private GameDataModel gameDataModel;
		[SerializeField] private HUDPlayGameView gameHUDPlayGameView;
		[SerializeField] private GameObject destroyInstructions;

		private INumberToTextProvider _numberToTextProvider;
		private IGameStateCommand _gameStateCommand;
		private HUDPlayGameView _gameHUDPlayGameView;

		public INumberToTextProvider NumberToTextProvider
		{
			get { return _numberToTextProvider; }
		}
		public HUDPlayGameView GameHUDPlayGameView
		{
			get { return _gameHUDPlayGameView; }
		}

		void Awake()
		{
			GameObject.Destroy(destroyInstructions);
		}
		
		void Start()
		{						
			gameDataModel.Initialize();
			ChangeGameStateCommand(StatesApp.MainMenu);
		}

		void OnDestroy()
		{
			_gameHUDPlayGameView = null;
		}

		public void SetUpNumberProvider(string codeLanguage)
		{
			LanguageController.Instance.ChangeLanguage(codeLanguage);
			if (codeLanguage.Equals(LanguageController.CodeLanguageSpanish))
			{
				_numberToTextProvider = new NumberToTextProviderSpanish();
			}
			if (codeLanguage.Equals(LanguageController.CodeLanguageEnglish))
			{
				_numberToTextProvider = new NumberToTextProviderEnglish();
			}
		}

		public void CreateGameHUD()
		{
			if (_gameHUDPlayGameView == null)
			{
				_gameHUDPlayGameView = Instantiate(gameHUDPlayGameView);				
			}			
			_gameHUDPlayGameView.Initialize();
		}

		public void ChangeGameStateCommand(StatesApp newGameState)
		{
			if (_gameStateCommand != null)
			{
				_gameStateCommand.Destroy();
			}
			_gameStateCommand = null;
			switch (newGameState)
			{
				case StatesApp.MainMenu:
					_gameStateCommand = new GameStateMenuCommand();
					break;

				case StatesApp.ShowNumber:
					_gameStateCommand = new GameStateShowNumberCommand();
					break;

				case StatesApp.AnswerNumber:
					_gameStateCommand = new GameStateAnswersCommand();
					break;
			}
			if (_gameStateCommand != null)
			{
				_gameStateCommand.Initialize();
			}					
		}
	}
}