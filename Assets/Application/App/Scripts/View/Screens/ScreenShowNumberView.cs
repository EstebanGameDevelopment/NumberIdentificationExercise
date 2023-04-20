using System.Collections.Generic;
using TMPro;
using UnityEngine;
using yourvrexperience.Utils;

namespace Innovamat.IdentificationNumber
{
	/* *********************************************
		Class ScreenShowNumberView
		
		-SOLID Principle ("S" Single Responsibility principle):
		
			Class responsible of displaying and reporting the
		interaction of the state "Show Number"

		-Dessign Pattern MVC ("V" View)
	*/
	public class ScreenShowNumberView : BaseScreenView, IScreenView
	{
		public const string ScreenName = "ScreenShowNumberView";

		[SerializeField] private TextMeshProUGUI title;
		[SerializeField] private GameObject prefabNumber;
		[SerializeField] private SlotManagerView options;

		public override void Initialize(params object[] parameters)
		{
			base.Initialize(parameters);

			List<int> numbersToShow = (List<int>)parameters[0];
            
			title.text = LanguageController.Instance.GetText("screen.show.number.title");

            List<ItemMultiObjectEntry> numberItemsToCreate = new List<ItemMultiObjectEntry>();
            for (int i = 0; i < numbersToShow.Count; i++)
            {
                numberItemsToCreate.Add(new ItemMultiObjectEntry(this.gameObject, numbersToShow[i], true));
            }
            options.Initialize(numbersToShow.Count, numberItemsToCreate, prefabNumber);			
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