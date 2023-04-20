using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yourvrexperience.Utils;

namespace Innovamat.IdentificationNumber
{
	/* *********************************************
		Interface INumberToTextProvider
		
		-SOLID Principle ("I" interface segregation principle):
		
			Specific only essential functionality interface to manage the 
		transformation from number to text
	*/
	public interface INumberToTextProvider
    {
		string NumberToText(int number);
	}
}