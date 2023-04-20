using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using yourvrexperience.Utils;

namespace Innovamat.IdentificationNumber
{
	/* *********************************************
		Class HUDPlayGameView
		
		-SOLID Principle ("S" Single Responsibility principle):
		
			Class responsible of displaying the game hud

		-Dessign Pattern MVC ("V" View)
	*/
	public class HUDPlayGameView : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI labelMistakes;
		[SerializeField] private TextMeshProUGUI labelSuccess;
		[SerializeField] private TextMeshProUGUI scoreMistakes;
		[SerializeField] private TextMeshProUGUI scoreSuccess;

		public void Initialize()
		{
			labelMistakes.text = LanguageController.Instance.GetText("game.hud.label.mistakes");
			labelSuccess.text = LanguageController.Instance.GetText("game.hud.label.success");		
			scoreMistakes.text = GameDataModel.Instance.ScoreMistakes.ToString();
			scoreSuccess.text = GameDataModel.Instance.ScoreSuccess.ToString();
		}

		public void SetSuccess(int success)
		{
			scoreSuccess.text = success.ToString();
		}

		public void SetMistakes(int mistakes)
		{
			scoreMistakes.text = mistakes.ToString();
		}

	}
}