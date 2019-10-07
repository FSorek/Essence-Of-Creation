using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class AttackControllerTest
    {
        [Test]
        public void Attacks_Then_Attacks_Again_After_1Second()
        {
            var adata = ScriptableObject.CreateInstance<TowerAttackData>();
            adata.TargetLimit = 1;
            adata.Range = 5;
            adata.AttackTimer = 1f;

            var obelisk = Substitute.For<IEntity>();
            obelisk.Position.Returns(Vector3.zero);

            ICanAttack module = Substitute.For<ICanAttack>();
            module.AttackData.Returns(adata);
            module.Entity.Returns(obelisk);

            ITakeDamage unit = Substitute.For<ITakeDamage>();
            ITakeDamage[] targets = new ITakeDamage[1]
            {
                unit
            };

            IWaveManager waveManager = Substitute.For<IWaveManager>();
            waveManager.EnemiesAlive.Returns(targets);

            AttackController controller = new AttackController(module, waveManager);

            GameTime.SetOffsetTimeForward(1f);
            controller.Tick();
            GameTime.SetOffsetTimeForward(2f);
            controller.Tick();

            module.Received(2).Attack(targets[0]);
        }

        [Test]
        public void Can_Not_Attack_After_Initial_Attack()
        {
            var adata = ScriptableObject.CreateInstance<TowerAttackData>();
            adata.TargetLimit = 1;
            adata.Range = 5;
            adata.AttackTimer = 1f;

            var obelisk = Substitute.For<IEntity>();
            obelisk.Position.Returns(Vector3.zero);

            ICanAttack module = Substitute.For<ICanAttack>();
            module.AttackData.Returns(adata);
            module.Entity.Returns(obelisk);

            ITakeDamage unit = Substitute.For<ITakeDamage>();
            ITakeDamage[] targets = new ITakeDamage[1]
            {
                unit
            };

            IWaveManager waveManager = Substitute.For<IWaveManager>();
            waveManager.EnemiesAlive.Returns(targets);

            AttackController controller = new AttackController(module, waveManager);

            controller.Tick();
            GameTime.SetOffsetTimeForward(.1f);
            var result = controller.CanAttack();

            Assert.False(result);
        }

        [Test]
        public void Two_Target_Limit_Shoots_Twice_At_The_Same_Time_On_Different_Targets()
        {
            var adata = ScriptableObject.CreateInstance<TowerAttackData>();
            adata.TargetLimit = 2;
            adata.Range = 5;
            adata.AttackTimer = 1f;

            var obelisk = Substitute.For<IEntity>();
            obelisk.Position.Returns(Vector3.zero);

            ICanAttack module = Substitute.For<ICanAttack>();
            module.AttackData.Returns(adata);
            module.Entity.Returns(obelisk);

            ITakeDamage unit1 = Substitute.For<ITakeDamage>();
            ITakeDamage unit2 = Substitute.For<ITakeDamage>();

            ITakeDamage[] targets = new ITakeDamage[2]
            {
                unit1,unit2
            };

            IWaveManager waveManager = Substitute.For<IWaveManager>();
            waveManager.EnemiesAlive.Returns(targets);

            AttackController controller = new AttackController(module, waveManager);

            GameTime.SetOffsetTimeForward(1f);
            controller.Tick();

            module.Received().Attack(targets[0]);
            module.Received().Attack(targets[1]);
        }

        [Test]
        public void Two_Target_Limit_Shoots_Once_On_A_Single_Target()
        {
            var adata = ScriptableObject.CreateInstance<TowerAttackData>();
            adata.TargetLimit = 2;
            adata.Range = 5;
            adata.AttackTimer = 1f;

            var obelisk = Substitute.For<IEntity>();
            obelisk.Position.Returns(Vector3.zero);

            ICanAttack module = Substitute.For<ICanAttack>();
            module.AttackData.Returns(adata);
            module.Entity.Returns(obelisk);

            ITakeDamage unit1 = Substitute.For<ITakeDamage>();

            ITakeDamage[] targets = new ITakeDamage[1]
            {
                unit1
            };

            IWaveManager waveManager = Substitute.For<IWaveManager>();
            waveManager.EnemiesAlive.Returns(targets);

            AttackController controller = new AttackController(module, waveManager);

            GameTime.SetOffsetTimeForward(1f);
            controller.Tick();

            module.Received().Attack(targets[0]);
        }
    }
}
