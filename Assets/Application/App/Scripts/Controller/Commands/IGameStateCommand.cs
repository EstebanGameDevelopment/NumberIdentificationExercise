using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using yourvrexperience.Utils;

namespace Innovamat.IdentificationNumber
{
	/* *********************************************
		Interface IGameStateCommand
		
		-SOLID Principle ("I" interface segregation principle):
		
			Specific only essential functionality interface to manage the game states
	*/
	public interface IGameStateCommand
    {
		void Initialize();
		void Destroy();
	}
}