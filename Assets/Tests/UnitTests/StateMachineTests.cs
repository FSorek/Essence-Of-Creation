using System.Collections;
using System.Collections.Generic;
using Data.Interfaces.StateMachines;
using Data.ScriptableObjects.StateMachines;
using DataBehaviors.StateMachines;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace a_statemachine
{
    public class with_integer_data
    {
        // A Test behaves as an ordinary method
        [Test]
        public void registers_a_state()
        {
            var state = Substitute.For<IState>();
            var stateData = Substitute.For<StateData<int>>();
            var stateMachine = new StateMachine<int>(stateData);
            
            stateMachine.RegisterState(1, state);
            
            Assert.AreEqual(stateMachine.GetState(1), state);
        }
    }
}
