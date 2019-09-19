using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using UnityEngine;

namespace Tests
{
    public class ProjectileControllerTest
    {
        [Test]
        public void Projectile_Destroyed_Called_Once_On_Target_Null()
        {
            ITakeDamage target = null;
            var owner = Substitute.For<ICanAttack>();
            var projectile = Substitute.For<IEntity>();

            var controller = new ProjectileController(target, owner, projectile);
            controller.Tick(() => true);
            
            projectile.Received(1).Destroy();
        }

        [Test]
        public void Projectile_Hit_And_Destroyed_Itself()
        {
            var target = Substitute.For<ITakeDamage>();
            var owner = Substitute.For<ICanAttack>();
            var projectile = Substitute.For<IEntity>();
            var attackData = ScriptableObject.CreateInstance<TowerAttackData>();
            var returnsTrue = Substitute.For<Func<bool>>();

            target.Position.Returns(Vector3.zero);
            projectile.Position.Returns(Vector3.zero);
            attackData.ProjectileSpeed = 50;
            attackData.CanFollowTarget = true;
            owner.AttackData.Returns(attackData);
            returnsTrue.Invoke().Returns(true);

            var controller = new ProjectileController(target, owner, projectile);
            controller.Tick(returnsTrue);

            returnsTrue.Received(1).Invoke();
            projectile.Received(1).Destroy();
        }

        [Test]
        public void Projectile_Too_Far_From_Target_Continues_To_Move()
        {
            var target = Substitute.For<ITakeDamage>();
            var owner = Substitute.For<ICanAttack>();
            var projectile = Substitute.For<IEntity>();
            var attackData = ScriptableObject.CreateInstance<TowerAttackData>();
            var returnsTrue = Substitute.For<Func<bool>>();
            var initialProjectilePosition = new Vector3(100,0,0);

            projectile.Position.Returns(Vector3.zero + initialProjectilePosition);
            target.Position.Returns(Vector3.zero);
            attackData.ProjectileSpeed = 1;
            attackData.CanFollowTarget = true;
            owner.AttackData.Returns(attackData);
            returnsTrue.Invoke().Returns(true);

            var controller = new ProjectileController(target, owner, projectile);
            GameTime.SetTimeSinceLastFrame(1f);
            controller.Tick(returnsTrue);

            Assert.AreNotEqual(initialProjectilePosition, projectile.Position);
            returnsTrue.DidNotReceive().Invoke();
            projectile.DidNotReceive().Destroy();
        }
    }
}
