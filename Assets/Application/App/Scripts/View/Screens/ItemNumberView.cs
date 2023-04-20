using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using yourvrexperience.Utils;

namespace Innovamat.IdentificationNumber
{
	/* *********************************************
		Class ItemNumberView
		
		-SOLID Principle ("S" Single Responsibility principle):
		
			Class responsible of displaying and reporting
		the interaction with a number.

		-Dessign Pattern MVC ("V" View)
	*/
    public class ItemNumberView : MonoBehaviour, ISlotView
    {
		public const string EventItemNumberViewInited = "EventItemNumberViewInited";
        public const string EventItemNumberViewSelected = "EventItemNumberViewSelected";

		public const float WaitTimeFraction = 0.1f;

		[SerializeField] private TextMeshProUGUI Text;
		[SerializeField] private CanvasGroup Alpha;
		[SerializeField] private Image Background;
		[SerializeField] private Button BtnSelect;

        private GameObject _parent;
        private bool _selected = false;
		private int _number;
		private float _animationTime;
		private Coroutine _currentCoroutine;
		private bool _enableInteraction = true;

		public int Number
		{
			get { return _number; }
		}

        public virtual bool Selected
        {
            get { return _selected; }
            set { _selected = value; }
        }
		public float AlphaItem
		{
			get { return Alpha.alpha; }
		}
		public bool EnableInteraction
		{
			get { return _enableInteraction; }
			set { 
				_enableInteraction = value; 
				BtnSelect.enabled = _enableInteraction;
			}
		}

        public ItemMultiObjectEntry Data => throw new NotImplementedException();

        public void Initialize(params object[] parameters)
        {
			Alpha.alpha = 0;
            _parent = (GameObject)((ItemMultiObjectEntry)parameters[0]).Objects[0];
            _number = (int)((ItemMultiObjectEntry)parameters[0]).Objects[1];
			bool showNumberAsText = (bool)((ItemMultiObjectEntry)parameters[0]).Objects[2];

			if (showNumberAsText)
			{
				string numberInText = MainAppController.Instance.NumberToTextProvider.NumberToText(_number);
				Text.text = numberInText;
			}
			else
			{
				Text.text = _number.ToString();				
			}            

			if (!showNumberAsText)
			{
				BtnSelect.onClick.AddListener(ButtonPressed);
			}            
			
			UIEventController.Instance.DispatchUIEvent(EventItemNumberViewInited, this);
        }

        void OnDestroy()
        {
			Destroy();
        }

        public bool Destroy()
        {
			if (_parent != null)
			{
				_parent = null;
				StopAnimations();
				return true;
			}            
			else
			{
				return false;
			}
        }


        private void ButtonPressed()
        {
			if (_enableInteraction)
			{
				ItemSelected();
			}            
        }

        public void ItemSelected(bool dispatchEvent = true)
        {
			UIEventController.Instance.DispatchUIEvent(EventItemNumberViewSelected, this, _number);
        }

		public void SetColor(Color responseColor)
		{
			Background.color = responseColor;
		}

		public void StopAnimations()
		{
			if (_currentCoroutine != null)
			{
				StopCoroutine(_currentCoroutine);
				_currentCoroutine = null;
			}
		}

		public void ShowAnimation(float timeToAnimate, bool fadein)
		{
			if (fadein)
			{
				Alpha.alpha = 0;
			}
			else
			{
				Alpha.alpha = 1;
			}	
			_animationTime = 0;
			StopAnimations();
			_currentCoroutine = StartCoroutine(FadeItemNumber(timeToAnimate, fadein));
		}

		IEnumerator FadeItemNumber(float timeToAnimate, bool fadein)
		{
			while (_animationTime < timeToAnimate)
			{
				_animationTime += WaitTimeFraction/timeToAnimate;
				if (fadein)
				{
					Alpha.alpha = _animationTime;
				}
				else
				{
					Alpha.alpha =  1 - _animationTime;
				}			
				yield return new WaitForSeconds(WaitTimeFraction);
			}

			if (fadein)
			{
				Alpha.alpha = 1;
			}
			else
			{
				Alpha.alpha = 0;
			}			
		}
    }
}