using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tanks1990MG_csharp.Application.States.Solution;

namespace Tanks1990MG_csharp.Application.States.Interfaces
{
    static public class StateBuilder
    {
        public enum StateID {
#if DEBUG
            TEST
#endif
                ,LoadingScreen
                ,MainMenu };

        static private Dictionary<StateID, IAppState> HotStates = new Dictionary<StateID, IAppState>();

        static public async Task<IAppState> GetStateAsync(StateID ID)
        {
            return await Task<IAppState>.Run(() => GetState(ID));
        }


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
                case StateID.LoadingScreen:

                    break;
#if DEBUG
                case StateID.TEST:
                    stateToReturn = new TestState();
                    break;
#endif
                default:
                    throw new Exception("Wrong ID!");
            }

            if (stateToReturn.DontUnloadFromMemory)
                HotStates.Add(ID, stateToReturn);
            return stateToReturn;
        }

    }
}
