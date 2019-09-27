﻿using System;
using UnityEngine;
using NSubstitute;
using NUnit.Framework;

namespace Tests
{
    
    public class EffectTest
    {
        [Test]
        public void Effect_Removed_When_Expired()
        {
            GameTime.SetOffsetTimeForward(10);
            var unit = Substitute.For<ITakeDamage>();
            var emptyAction = Substitute.For<Action<ITakeDamage>>();
            var effect = new Effect(0, 1, 0.5f, true, emptyAction);

            effect.Tick(unit);
            GameTime.SetOffsetTimeForward(11.01f);
            effect.Tick(unit);

            unit.Received(1).RemoveEffect(effect);
        }

        [Test]
        public void Effect_Interval_HalfSecond_Duration_OneSecond_Ticks_Twice()
        {
            GameTime.SetOffsetTimeForward(10);
            ITakeDamage unit = Substitute.For<ITakeDamage>();
            var emptyAction = Substitute.For<Action<ITakeDamage>>();
            var effect = new Effect(0, 1, 0.5f, true, emptyAction);

            effect.Tick(unit);
            GameTime.SetOffsetTimeForward(10.01f);
            effect.Tick(unit);
            GameTime.SetOffsetTimeForward(10.49f);
            effect.Tick(unit);
            GameTime.SetOffsetTimeForward(10.5f);
            effect.Tick(unit);
            GameTime.SetOffsetTimeForward(10.99f);
            effect.Tick(unit);
            GameTime.SetOffsetTimeForward(11);
            effect.Tick(unit);

            emptyAction.Received(2).Invoke(unit);
        }

        [Test]
        public void Effect_Gets_Extended_Correctly()
        {
            GameTime.SetOffsetTimeForward(10);
            ITakeDamage unit = Substitute.For<ITakeDamage>();
            var emptyAction = Substitute.For<Action<ITakeDamage>>();
            var effect = new Effect(0, 1, 0.5f, true, emptyAction);

            GameTime.SetOffsetTimeForward(10.99f);
            effect.Tick(unit);
            effect.Extend();
            GameTime.SetOffsetTimeForward(11.99f);
            effect.Tick(unit);

            unit.DidNotReceive().RemoveEffect(effect);
        }
    }
}
