using System.Collections.Generic;
using TMPro;
using UnityEngine;
using yourvrexperience.Utils;

namespace Innovamat.IdentificationNumber
{
	/* *********************************************
		Class ScreenAnswerNumberView
		
		-SOLID Principle ("S" Single Responsibility principle):
		
			Class responsible of displaying and reporting the
		interaction of the state "Select Answer"

		-Dessign Pattern MVC ("V" View)
	*/
	public class ScreenAnswerNumberView : BaseScreenView, IScreenView
	{
		public const string ScreenName = "ScreenAnswerNumberView";

		[SerializeField] private TextMeshProUGUI title;
		[SerializeField] private GameObject prefabNumber;
		[SerializeField] private SlotManagerView options;

		public override void Initialize(params object[] parameters)
		{
			base.Initialize(parameters);

			List<int> numbersForAnswer = (List<int>)parameters[0];

			if (GameDataModel.Instance.Numbers.Count > 1)
			{
				title.text = LanguageController.Instance.GetText("screen.answer.number.title.multiple");
			}
			else
			{
				title.text = LanguageController.Instance.GetText("screen.answer.number.title.single");
			}

            List<ItemMultiObjectEntry> numberItemsToCreate = new List<ItemMultiObjectEntry>();
            for (int i = 0; i < numbersForAnswer.Count; i++)
            {
                numberItemsToCreate.Add(new ItemMultiObjectEntry(this.gameObject, numbersForAnswer[i], false));
            }
            options.Initialize(numbersForAnswer.Count, numberItemsToCreate, prefabNumber);			
		}

		public override void Destroy()
		{
			if (options != null)
			{
				base.Destroy();
				options.ClearCurrentGameObject(true);
			}
		}
	}
}