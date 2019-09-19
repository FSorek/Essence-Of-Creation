using UnityEngine;
using NSubstitute;
using NUnit.Framework;

namespace Tests
{
    public class EntitySimpleMoveTest
    {
        [Test]
        public void Entity_Moved_To_The_Right_By_One_Unit()
        {
            var entity = Substitute.For<IEntity>();
            var movement = new EntitySimpleMove(entity);
            var targetPosition = new Vector3(100, 0, 0);

            entity.Position.Returns(Vector3.zero);

            GameTime.SetTimeSinceLastFrame(1f);
            movement.Move(targetPosition, 1);

            Assert.AreEqual(new Vector3(1,0,0), entity.Position);
        }
    }
}