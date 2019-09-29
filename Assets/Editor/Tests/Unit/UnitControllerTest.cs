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
            owner.MaxHealth.Returns(new Stat(100));
            var controller = new UnitController(owner);

            controller.TakeDamage(0,100);
            
            owner.Received(1).Destroy();
        }

        [Test]
        public void Unit_Extends_Already_Applied_Effect()
        {
            var owner = Substitute.For<ITakeDamage>();
            var emptyAction = Substitute.For<Action<IUnit>>();
            var effect1 = new Effect(1, 1, 1, true, emptyAction, emptyAction, emptyAction);
            var effect2 = new Effect(1, 1, 1, true, emptyAction, emptyAction, emptyAction);

            var controller = new UnitController(owner);

            controller.AddEffect(effect1);
            controller.AddEffect(effect2);

            Assert.AreEqual(2, effect1.Duration);
        }

        [Test]
        public void Unit_Adds_Second_Unique_Effect_From_Same_Source()
        {
            var owner = Substitute.For<ITakeDamage>();
            var emptyAction = Substitute.For<Action<IUnit>>();
            var emptyAction2 = Substitute.For<Action<IUnit>>();
            var effect1 = new Effect(1, 1, 1, true, emptyAction, emptyAction, emptyAction);
            var effect2 = new Effect(1, 2, .5f, true, emptyAction2, emptyAction2, emptyAction2);

            var controller = new UnitController(owner);

            controller.AddEffect(effect1);
            controller.AddEffect(effect2);

            Assert.IsTrue(controller.ActiveEffects.Contains(effect1) && controller.ActiveEffects.Contains(effect2));
        }

        [Test]
        public void Unit_Adds_New_Similar_Effect_But_Different_Attacker()
        {
            var owner = Substitute.For<ITakeDamage>();
            var emptyAction = Substitute.For<Action<IUnit>>();
            var effect1 = new Effect(0, 1, 1, true, emptyAction, emptyAction, emptyAction);
            var effect2 = new Effect(1, 1, 1, true, emptyAction, emptyAction, emptyAction);

            var controller = new UnitController(owner);

            controller.AddEffect(effect1);
            controller.AddEffect(effect2);

            Assert.IsTrue(controller.ActiveEffects.Contains(effect1) && controller.ActiveEffects.Contains(effect2));
        }

        [Test]
        public void Unit_Takes_10Damage_Then_Regenerates_5Health()
        {
            GameTime.SetOffsetTimeForward(0);
            var owner = Substitute.For<ITakeDamage>();
            owner.MaxHealth.Returns(new Stat(100));
            owner.HealthRegeneration.Returns(new Stat(5));
            var controller = new UnitController(owner);

            controller.TakeDamage(0,10);
            controller.RegenerateHealth();
            controller.RegenerateHealth();
            GameTime.SetOffsetTimeForward(1.5f);
            controller.RegenerateHealth();
            
            Assert.AreEqual(95, controller.CurrentHealth);
        }

        [Test]
        public void Unit_Takes_10Damage_But_1ArmorLayer_Reduces_To_9Damage()
        {
            var owner = Substitute.For<ITakeDamage>();
            owner.MaxHealth.Returns(new Stat(100));
            owner.HealthRegeneration.Returns(new Stat(5));
            owner.ArmorLayers.Returns(new Stat(1));
            var controller = new UnitController(owner);

            controller.TakeDamage(0, 10);

            Assert.AreEqual(91, controller.CurrentHealth);
        }

        [Test]
        public void Unit_Takes_10Damage_But_5ArmorLayer_Reduces_To_5Damage()
        {
            var owner = Substitute.For<ITakeDamage>();
            owner.MaxHealth.Returns(new Stat(100));
            owner.HealthRegeneration.Returns(new Stat(5));
            owner.ArmorLayers.Returns(new Stat(5));
            var controller = new UnitController(owner);

            controller.TakeDamage(0, 10);

            Assert.AreEqual(95, controller.CurrentHealth);
        }
    }
}
