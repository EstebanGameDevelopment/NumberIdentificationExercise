using TMPro;
using UnityEngine;
using UnityEngine.UI;
using yourvrexperience.Utils;

namespace Innovamat.IdentificationNumber
{
	/* *********************************************
		Class ScreenMainMenuView
		
		-SOLID Principle ("S" Single Responsibility principle):
		
			Class responsible of displaying and reporting the
		interaction of the state "Menu"

		-Dessign Pattern MVC ("V" View)
	*/
	public class ScreenMainMenuView : BaseScreenView, IScreenView
	{
		public const string ScreenName = "ScreenMainMenuView";

		public const string EventScreenMainMenuViewPlayGame = "EventScreenMainMenuViewPlayGame";

		[SerializeField] private TextMeshProUGUI titleScreen;
		[SerializeField] private Button buttonPlay;

		public override string NameScreen 
		{ 
			get { return ScreenName; }
		}

		public override void Initialize(params object[] parameters)
		{
			base.Initialize(parameters);

			buttonPlay.onClick.AddListener(OnButtonPlay);

			InitTexts();

			SystemEventController.Instance.Event += OnSystemEvent;
		}

		private void InitTexts()
		{
			titleScreen.text = LanguageController.Instance.GetText("screen.main.menu.title");

			buttonPlay.transform.GetComponentInChildren<TextMeshProUGUI>().text = LanguageController.Instance.GetText("screen.main.menu.play.game");
		}

		public override void Destroy()
		{
			base.Destroy();
		}

        private void OnButtonPlay()
		{
			UIEventController.Instance.DispatchUIEvent(EventScreenMainMenuViewPlayGame);
		}

		private void OnSystemEvent(string nameEvent, object[] parameters)
        {
            if (nameEvent.Equals(LanguageController.EventLanguageControllerChangedCodeLanguage))
			{
				InitTexts();
			}
        }
	}
}