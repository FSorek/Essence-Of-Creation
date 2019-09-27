using System;
using NSubstitute;
using NUnit.Framework;

namespace Tests
{
    public class UnitControllerTest
    {
        [Test]
        public void Unit_Takes_Damage_And_Dies()
        {
            var owner = Substitute.For<ITakeDamage>();
            owner.MaxHealth.Returns(100);
            var controller = new UnitController(owner);

            controller.TakeDamage(0,100);
            
            owner.Received(1).Destroy();
        }

        [Test]
        public void Unit_Extends_Already_Applied_Effect()
        {
            var owner = Substitute.For<ITakeDamage>();
            var emptyAction = Substitute.For<Action<ITakeDamage>>();
            var effect1 = new Effect(1, 1, 1, true, emptyAction);
            var effect2 = new Effect(1, 1, 1, true, emptyAction);

            var controller = new UnitController(owner);

            controller.AddEffect(effect1);
            controller.AddEffect(effect2);

            Assert.AreEqual(2, effect1.Duration);
        }

        [Test]
        public void Unit_Adds_Second_Unique_Effect_From_Same_Source()
        {
            var owner = Substitute.For<ITakeDamage>();
            var emptyAction = Substitute.For<Action<ITakeDamage>>();
            var emptyAction2 = Substitute.For<Action<ITakeDamage>>();
            var effect1 = new Effect(1, 1, 1, true, emptyAction);
            var effect2 = new Effect(1, 2, .5f, true, emptyAction2);

            var controller = new UnitController(owner);

            controller.AddEffect(effect1);
            controller.AddEffect(effect2);

            Assert.IsTrue(controller.ActiveEffects.Contains(effect1) && controller.ActiveEffects.Contains(effect2));
        }

        [Test]
        public void Unit_Adds_New_Similar_Effect_But_Different_Attacker()
        {
            var owner = Substitute.For<ITakeDamage>();
            var emptyAction = Substitute.For<Action<ITakeDamage>>();
            var effect1 = new Effect(0, 1, 1, true, emptyAction);
            var effect2 = new Effect(1, 1, 1, true, emptyAction);

            var controller = new UnitController(owner);

            controller.AddEffect(effect1);
            controller.AddEffect(effect2);

            Assert.IsTrue(controller.ActiveEffects.Contains(effect1) && controller.ActiveEffects.Contains(effect2));
        }
    }
}
