using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yourvrexperience.Utils;

namespace Innovamat.IdentificationNumber
{
	/* *********************************************
		Class NumberToTextProviderEnglish
		
		-SOLID Principle ("S" Single Responsibility principle):
		
			Class responsible of transforming number to text

		-SOLID Principle ("O" open-closed principle):
		
			Open for extension, close for modification
	*/	
	public class NumberToTextProviderEnglish : INumberToTextProvider
    {
		protected virtual string GetBelowHundred(int number)
		{
			string output = "";
			int decimals = (number % 100);
			if (decimals == 0)
			{
				return output;
			}
			else
			{
				if ((decimals >= 10) && (decimals < 30))
				{
					output = LanguageController.Instance.GetText("number.unit." + decimals);
				}
				else
				{
					if (decimals > 30)
					{
						int units = decimals % 10;
						int decimalDigit = (decimals / 10) * 10;
						if (units > 0)
						{
							output = LanguageController.Instance.GetText("number.unit." + decimalDigit) 
								   + LanguageController.Instance.GetText("number.unit.separator") 
								   + LanguageController.Instance.GetText("number.unit." + units);
						}
						else
						{
							output = LanguageController.Instance.GetText("number.unit." + decimalDigit);
						}						
					}
					else
					{
						output = LanguageController.Instance.GetText("number.unit." + decimals);
					}
				}
				return output;
			}
		}

		protected virtual string GetBelowThousand(int number)
		{
			string output = "";
			int centesimals = number % 1000;
			if (centesimals == 0)
			{
				return output;
			}
			else
			{
				int centesimalDigit = centesimals / 100;
				string centesimalString = "";
				if (centesimalDigit > 0)
				{
					centesimalString = LanguageController.Instance.GetText("number.unit." + centesimalDigit)
										+ " " + LanguageController.Instance.GetText("number.unit.hundred");
				}
				string decimalsString = GetBelowHundred(centesimals);
				output = ((centesimalString.Length>0)?centesimalString + " ":"") 
						+ ((decimalsString.Length>0)?decimalsString:"");
			}
			return output;
		}

		protected virtual string GetBelowMillion(int number)
		{
			string output = "";
			int millesimals = number % 1000000;
			if (millesimals == 0)
			{
				return output;
			}
			else
			{
				int millesimalDigits = millesimals / 1000;
				string millesimalString = GetBelowThousand(millesimalDigits);
				int centesimalDigits = millesimals % 1000;
				string centesimalString = GetBelowThousand(millesimals);
				output = millesimalString 
						+ ((millesimalString.Length > 0)?" " + LanguageController.Instance.GetText("number.unit.thousand"):"")
						+ ((centesimalString.Length>0)?" " + centesimalString:"");
			}
			return output;
		}

		protected virtual string GetBelowBillion(int number)
		{
			string output = "";
			int millionesimals = number % 1000000000;
			if (millionesimals == 0)
			{
				return output;
			}
			else
			{
				int millionesimalDigits = millionesimals / 1000000;
				string millionesimalString = GetBelowThousand(millionesimalDigits);
				int millesimalDigits = millionesimals % 1000000;
				string millesimalString = GetBelowMillion(millesimalDigits);
				output = millionesimalString 
						+ ((millionesimalString.Length > 0)?" " + LanguageController.Instance.GetText("number.unit.million"):"")
						+ ((millesimalString.Length>0)?" " + millesimalString:"");
			}
			return output;
		}

		public virtual string NumberToText(int number)
		{
			return GetBelowBillion(number).Trim();
		}

	}
}