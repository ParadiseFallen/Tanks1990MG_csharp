using System;
using System.Collections.Generic;
using Tanks1990MG_csharp.Application.States.Solution;

namespace Tanks1990MG_csharp.Application.States.Interfaces
{
    static public class StateBuilder
    {
        public enum StateID { TEST,MainMenu };

        static private Dictionary<StateID, IAppState> HotStates = new Dictionary<StateID, IAppState>();

        /// <summary>
        /// Jist return state by ID
        /// </summary>
        /// <param name="ID">State id</param>
        /// <returns>IAppState</returns>
        static public IAppState GetState(StateID ID)
        {
            IAppState stateToReturn = null;

            if (HotStates.TryGetValue(ID, out stateToReturn)) { stateToReturn.Load(); return stateToReturn; }

            switch (ID)
            {
                case StateID.MainMenu:
                    stateToReturn = new MainMenuAppState();
                    break;
                case StateID.TEST:
                    stateToReturn = new TestState();
                    break;
                default:
                    throw new Exception("Wrong ID!");
            }

            if (stateToReturn.DontUnloadFromMemory)
                HotStates.Add(ID, stateToReturn);
            return stateToReturn;
        }

    }
}
