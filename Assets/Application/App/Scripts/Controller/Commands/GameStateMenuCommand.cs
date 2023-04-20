using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yourvrexperience.Utils;

namespace Innovamat.IdentificationNumber
{
	/* *********************************************
		Class GameStateMenuCommand
		
		-SOLID Principle ("S" Single Responsibility principle):
		
			Class responsible of managing the state "Main Menu"

		-Dessign Pattern Command
	*/
	public class GameStateMenuCommand : IGameStateCommand
    {
		public void Initialize()
		{
			UIEventController.Instance.Event += OnUIEvent;

			MainAppController.Instance.SetUpNumberProvider(GameDataModel.Instance.CodeLanguage);
			ScreenController.Instance.CreateScreen(ScreenMainMenuView.ScreenName, true, false);
		}

		public void Destroy()
		{
			if (UIEventController.Instance != null) UIEventController.Instance.Event -= OnUIEvent;
		}
		
		private void OnUIEvent(string nameEvent, object[] parameters)
		{
			if (nameEvent.Equals(ScreenMainMenuView.EventScreenMainMenuViewPlayGame))
			{
				MainAppController.Instance.ChangeGameStateCommand(MainAppController.StatesApp.ShowNumber);
			}
		}
	}
}