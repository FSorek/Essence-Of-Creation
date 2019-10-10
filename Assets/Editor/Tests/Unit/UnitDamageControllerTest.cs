using System;
using NSubstitute;
using NUnit.Framework;

namespace Tests
{
    public class UnitDamageControllerTest
    {
        [Test]
        public void Unit_Takes_Damage_And_Dies()
        {
            var owner = Substitute.For<IUnit>();
            owner.MaxHealth.Returns(new Stat(100));
            var controller = new UnitDamageController(owner);

            controller.TakeDamage(0,new Damage(100,0,0,0));
            
            owner.Received(1).Destroy();
        }

        [Test]
        public void Unit_Takes_10Damage_Then_Regenerates_5Health()
        {
            GameTime.SetOffsetTimeForward(0);
            var owner = Substitute.For<IUnit>();
            owner.MaxHealth.Returns(new Stat(100));
            owner.HealthRegeneration.Returns(new Stat(5));
            var controller = new UnitDamageController(owner);

            controller.TakeDamage(0, new Damage(10, 0, 0, 0));
            controller.RegenerateHealth();
            controller.RegenerateHealth();
            GameTime.SetOffsetTimeForward(1.5f);
            controller.RegenerateHealth();
            
            Assert.AreEqual(95, controller.CurrentHealth);
        }

        [Test]
        public void Unit_Takes_10Damage_But_1ArmorLayer_Reduces_To_9Damage()
        {
            var owner = Substitute.For<IUnit>();
            owner.MaxHealth.Returns(new Stat(100));
            owner.ArmorLayers.Returns(new Stat(1));
            var controller = new UnitDamageController(owner);

            controller.TakeDamage(0, new Damage(10, 0, 0, 0));

            Assert.AreEqual(91, controller.CurrentHealth);
        }

        [Test]
        public void Unit_Takes_10Damage_But_5ArmorLayer_Reduces_To_5Damage()
        {
            var owner = Substitute.For<IUnit>();
            owner.MaxHealth.Returns(new Stat(100));
            owner.ArmorLayers.Returns(new Stat(5));
            var controller = new UnitDamageController(owner);

            controller.TakeDamage(0, new Damage(10, 0, 0, 0));

            Assert.AreEqual(95, controller.CurrentHealth);
        }

        [Test]
        public void Unit_10CrystallineLayers_Takes_Damage_And_Reduces_It_For_Each_Health_Stage()
        {
            var owner = Substitute.For<IUnit>();
            owner.MaxHealth.Returns(new Stat(100));
            owner.CrystallineLayers.Returns(new Stat(10));

            var controller = new UnitDamageController(owner);
            
            controller.TakeDamage(0, new Damage(25,0,0,0));
            Assert.AreEqual(75, controller.CurrentHealth);

            controller.TakeDamage(0, new Damage(25, 0, 0, 0));
            Assert.AreEqual(55, controller.CurrentHealth);

            controller.TakeDamage(0, new Damage(10, 0, 0, 0));
            Assert.AreEqual(47, controller.CurrentHealth);

            controller.TakeDamage(0, new Damage(25, 0, 0, 0));
            Assert.AreEqual(32, controller.CurrentHealth);
        }
    }
}
