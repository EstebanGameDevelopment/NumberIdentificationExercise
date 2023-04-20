using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yourvrexperience.Utils;

namespace Innovamat.IdentificationNumber
{
	/* *********************************************
		Class GameStateAnswersCommand
		
		-SOLID Principle ("S" Single Responsibility principle):
		
			Class responsible of managing the state "Select Answer"

		-Dessign Pattern Command
		-Dessign Pattern MVC ("C" Controller)
	*/
	public class GameStateAnswersCommand : IGameStateCommand
    {
		enum StatesAnswersNumber { Appear, Stay, Disappear };

		public const string EventGameStateAnswersCommandShowAnimationCompleted = "EventGameStateAnswersCommandShowAnimationCompleted";
		public const string EventGameStateAnswersCommandRestoreInteraction = "EventGameStateAnswersCommandRestoreInteraction";

		private List<ItemNumberView> _itemsNumbers = new List<ItemNumberView>();
		private StatesAnswersNumber _state;
		private int _numberMistakes = 0;
		private int _numberSuccess = 0;

		public void Initialize()
		{
			UIEventController.Instance.Event += OnUIEvent;
			SystemEventController.Instance.Event += OnSystemEvent;

			_state = StatesAnswersNumber.Appear;
			GameDataModel.Instance.GenerateAnswers();			
			ScreenController.Instance.CreateScreen(ScreenAnswerNumberView.ScreenName, true, false, GameDataModel.Instance.Responses);

			SystemEventController.Instance.DelaySystemEvent(EventGameStateAnswersCommandShowAnimationCompleted, GameDataModel.Instance.DelayToShow);
		}

        public void Destroy()
		{
			_itemsNumbers.Clear();
			if (UIEventController.Instance != null) UIEventController.Instance.Event -= OnUIEvent;
			if (SystemEventController.Instance != null) SystemEventController.Instance.Event -= OnSystemEvent;
		}

		private void EnableItemsInteraction(bool enableInteraction)
		{
			foreach (ItemNumberView item in _itemsNumbers)
			{
				item.EnableInteraction = enableInteraction;
			}
		}

		private void ShowFinalResult()
		{
			EnableItemsInteraction(false);
			foreach (ItemNumberView item in _itemsNumbers)
			{
				if (GameDataModel.Instance.Numbers.Contains(item.Number))
				{
					if (!item.Selected)
					{
						item.Selected = true;
						item.SetColor(Color.green);
					}
				}
			}
		}

		private void FadeOutAnimation()
		{
			foreach (ItemNumberView item in _itemsNumbers)
			{
				if (item.AlphaItem != 0)
				{
					item.ShowAnimation(GameDataModel.Instance.DelayToDisappear, false);
				}
			}
		}

		private void RunEndGame()
		{
			EnableItemsInteraction(false);
			ShowFinalResult();
			FadeOutAnimation();
			_state = StatesAnswersNumber.Disappear;
			SystemEventController.Instance.DelaySystemEvent(EventGameStateAnswersCommandShowAnimationCompleted, GameDataModel.Instance.DelayToDisappear);						
		}

        private void OnSystemEvent(string nameEvent, object[] parameters)
        {
            if (nameEvent.Equals(EventGameStateAnswersCommandShowAnimationCompleted))
			{
				switch (_state)
				{
					case StatesAnswersNumber.Appear:
						_state = StatesAnswersNumber.Stay;
						EnableItemsInteraction(true);
						break;

					case StatesAnswersNumber.Stay:						
						break;

					case StatesAnswersNumber.Disappear:
						MainAppController.Instance.ChangeGameStateCommand(MainAppController.StatesApp.ShowNumber);
						break;
				}
			}
			if (nameEvent.Equals(EventGameStateAnswersCommandRestoreInteraction))
			{
				EnableItemsInteraction(true);
			}
        }

		private void OnUIEvent(string nameEvent, object[] parameters)
        {
			if (nameEvent.Equals(ItemNumberView.EventItemNumberViewInited))
			{
				ItemNumberView itemNumber = (ItemNumberView)parameters[0];
				itemNumber.ShowAnimation(GameDataModel.Instance.DelayToShow, true);
				itemNumber.EnableInteraction = false;
				_itemsNumbers.Add(itemNumber);
			}
            if (nameEvent.Equals(ItemNumberView.EventItemNumberViewSelected))
			{
				ItemNumberView selectedItemNumber = (ItemNumberView)parameters[0];
				if (!selectedItemNumber.Selected)
				{
					selectedItemNumber.Selected = true;
					if (GameDataModel.Instance.Numbers.Contains(selectedItemNumber.Number))
					{
						selectedItemNumber.SetColor(Color.green);
						_numberSuccess++;
						GameDataModel.Instance.ScoreSuccess++;					
						MainAppController.Instance.GameHUDPlayGameView.SetSuccess(GameDataModel.Instance.ScoreSuccess);
						if (_numberSuccess == GameDataModel.Instance.Numbers.Count)
						{
							RunEndGame();
						}
					}
					else
					{
						selectedItemNumber.SetColor(Color.red);
						selectedItemNumber.ShowAnimation(GameDataModel.Instance.DelayToDisappear, false);
						_numberMistakes++;
						GameDataModel.Instance.ScoreMistakes++;
						MainAppController.Instance.GameHUDPlayGameView.SetMistakes(GameDataModel.Instance.ScoreMistakes);
						EnableItemsInteraction(false);
						if (_numberMistakes > 1)
						{
							RunEndGame();
						}
						else
						{
							SystemEventController.Instance.DelaySystemEvent(EventGameStateAnswersCommandRestoreInteraction, GameDataModel.Instance.DelayToDisappear);
						}
					}				
				}
			}
        }
	}
}