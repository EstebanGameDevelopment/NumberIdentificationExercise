using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yourvrexperience.Utils;

namespace Innovamat.IdentificationNumber
{
	/* *********************************************
		Class NumberToTextProviderSpanish
		
		-SOLID Principle ("S" Single Responsibility principle):
		
			Class responsible of transforming number to text

		-SOLID Principle ("O" open-closed principle):
		
			Open for extension, close for modification

		-SOLID Principle ("D" Dependeny inversion principle):
		
			Dependency on abstractions, not concretaions
	*/	
	public class NumberToTextProviderSpanish : NumberToTextProviderEnglish, INumberToTextProvider
    {
		protected override string GetBelowThousand(int number)
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
					if (centesimalDigit == 1)
					{
						int decimalsLeft = centesimals % 100;
						if (decimalsLeft == 0)
						{
							centesimalString = LanguageController.Instance.GetText("number.unit.hundred");
						}
						else
						{
							centesimalString = LanguageController.Instance.GetText("number.unit.hundred.a");
						}
					}
					else
					{
						switch (centesimalDigit)
						{
							case 5:
								centesimalString = LanguageController.Instance.GetText("number.unit.500");
								break;
							case 7:
								centesimalString = LanguageController.Instance.GetText("number.unit.700");
								break;								
							default:
								centesimalString = LanguageController.Instance.GetText("number.unit." + centesimalDigit)
											+ " " + LanguageController.Instance.GetText("number.unit.hundred.b");
								break;
						}
					}
				}
				string decimalsString = GetBelowHundred(centesimals);
				output = ((centesimalString.Length>0)?centesimalString + " ":"") 
						+ ((decimalsString.Length>0)?decimalsString:"");
			}
			return output;
		}

		protected override string GetBelowMillion(int number)
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
				string millesimalString = "";
				if (millesimalDigits > 0)
				{
					if (millesimalDigits == 1)
					{
						millesimalString = LanguageController.Instance.GetText("number.unit.thousand") + " ";
					}
					else
					{
						millesimalString = GetBelowThousand(millesimalDigits) + " " + LanguageController.Instance.GetText("number.unit.thousand");
					}
				}
				int centesimalDigits = millesimals % 1000;
				string centesimalString = GetBelowThousand(centesimalDigits);
				output += millesimalString
						+ ((centesimalString.Length>0)?" " + centesimalString:"");
			}
			return output;
		}

		protected override string GetBelowBillion(int number)
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
				if (millionesimalDigits > 0)
				{
					if (millionesimalDigits == 1)
					{
					
						millionesimalString = LanguageController.Instance.GetText("number.unit.1.a") + " " + LanguageController.Instance.GetText("number.unit.million");
					}
					else
					{
						millionesimalString = GetBelowThousand(millionesimalDigits) + " " + LanguageController.Instance.GetText("number.unit.million.a");
					}
				}
				int millesimalDigits = millionesimals % 1000000;
				string millesimalString = GetBelowMillion(millesimalDigits);
				output = millionesimalString 
						+ ((millesimalString.Length>0)?" " + millesimalString:"");
			}
			return output;
		}
	}
}