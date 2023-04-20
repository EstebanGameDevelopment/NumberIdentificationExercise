using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yourvrexperience.Utils;

namespace Innovamat.IdentificationNumber
{
	/* *********************************************
		Class GameStateShowNumberCommand
		
		-SOLID Principle ("S" Single Responsibility principle):
		
			Class responsible of managing the state "Show Number"

		-Dessign Pattern Command
		-Dessign Pattern MVC ("C" Controller)
	*/
	public class GameStateShowNumberCommand : IGameStateCommand
    {
		enum StatesShowNumber { Appear, Stay, Disappear}
		public const string EventGameStateShowNumberCommandShowAnimationCompleted = "EventGameStateShowNumberCommandShowAnimationCompleted";

		private List<ItemNumberView> _itemsNumbers = new List<ItemNumberView>();
		private StatesShowNumber _state;

		public void Initialize()
		{
			SystemEventController.Instance.Event += OnSystemEvent;
			UIEventController.Instance.Event += OnUIEvent;

			_state = StatesShowNumber.Appear;
			MainAppController.Instance.CreateGameHUD();
			GameDataModel.Instance.CreateNumberToFind();			
			ScreenController.Instance.CreateScreen(ScreenShowNumberView.ScreenName, true, false, GameDataModel.Instance.Numbers);

			SystemEventController.Instance.DelaySystemEvent(EventGameStateShowNumberCommandShowAnimationCompleted, GameDataModel.Instance.DelayToShow);
		}

        public void Destroy()
		{
			_itemsNumbers.Clear();
			if (SystemEventController.Instance != null) SystemEventController.Instance.Event -= OnSystemEvent;
			if (UIEventController.Instance != null) UIEventController.Instance.Event -= OnUIEvent;
		}

        private void ActivateItemNumberAnimation(float delay, bool fadein)
		{
			foreach(ItemNumberView item in _itemsNumbers)
			{
				item.ShowAnimation(delay, fadein);
			}
		}

        private void OnUIEvent(string nameEvent, object[] parameters)
        {
			if (nameEvent.Equals(ItemNumberView.EventItemNumberViewInited))
			{
				ItemNumberView itemNumber = (ItemNumberView)parameters[0];
				itemNumber.ShowAnimation(GameDataModel.Instance.DelayToShow, true);
				_itemsNumbers.Add(itemNumber);
			}
        }

        private void OnSystemEvent(string nameEvent, object[] parameters)
        {
            if (nameEvent.Equals(EventGameStateShowNumberCommandShowAnimationCompleted))
			{
				switch (_state)
				{
					case StatesShowNumber.Appear:
						_state = StatesShowNumber.Stay;
						SystemEventController.Instance.DelaySystemEvent(EventGameStateShowNumberCommandShowAnimationCompleted, GameDataModel.Instance.DelayToStay);
						break;

					case StatesShowNumber.Stay:
						_state = StatesShowNumber.Disappear;
						ActivateItemNumberAnimation(GameDataModel.Instance.DelayToDisappear, false);
						SystemEventController.Instance.DelaySystemEvent(EventGameStateShowNumberCommandShowAnimationCompleted, GameDataModel.Instance.DelayToDisappear);
						break;

					case StatesShowNumber.Disappear:
						MainAppController.Instance.ChangeGameStateCommand(MainAppController.StatesApp.AnswerNumber);
						break;
				}
			}
        }
	}
}