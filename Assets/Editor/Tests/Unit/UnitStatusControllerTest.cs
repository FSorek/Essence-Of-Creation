using System;
using System.Linq;
using NSubstitute;
using NUnit.Framework;

namespace Tests
{
    public class UnitStatusControllerTest
    {
        [Test]
        public void Unit_Adds_Second_Unique_Effect_From_Same_Source()
        {
            var unit = Substitute.For<IUnit>();
            var status = Substitute.For<IStatus>();
            var status2 = Substitute.For<IStatus>();
            status.Stacks.Returns(false);
            status2.Stacks.Returns(false);
            status.Equals(status2).Returns(false);

            var controller = new UnitStatusController(unit);
            controller.AddStatus(status);
            controller.AddStatus(status2);

            status.DidNotReceive().Extend();
            Assert.AreEqual(2, controller.CurrentStatuses.Length);
        }

        [Test]
        public void Effect_Removed_When_Expired()
        {
            var unit = Substitute.For<IUnit>();
            var status = Substitute.For<IStatus>();
            status.Tick(unit).Returns(false);
            status.Stacks.Returns(true);
            status.Equals(status).Returns(true);

            var controller = new UnitStatusController(unit);
            controller.AddStatus(status);
            controller.Tick();
            Assert.IsTrue(controller.CurrentStatuses.Contains(status));
            status.Tick(unit).Returns(true);
            controller.Tick();
            Assert.IsFalse(controller.CurrentStatuses.Contains(status));
        }

        [Test]
        public void Effect_Gets_Extended_Correctly()
        {
            var unit = Substitute.For<IUnit>();
            var status = Substitute.For<IStatus>();
            status.Tick(unit).Returns(false);
            status.Stacks.Returns(false);
            status.Equals(status).Returns(true);

            var controller = new UnitStatusController(unit);
            controller.AddStatus(status);
            controller.AddStatus(status);

            status.Received(1).Extend();
            Assert.AreEqual(1, controller.CurrentStatuses.Length);
        }
    }
}
