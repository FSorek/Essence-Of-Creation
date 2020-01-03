﻿namespace Data.Interfaces.StateMachines
{
    public interface IState
    {
        void StateEnter();
        void ListenToState();
        void StateExit();
    }
}