#define PauseAfterTest
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tanks1990MG_csharp.Application.GameEntityes.Test;

namespace UnitTestsFramework
{
    [TestClass]
    public class UnitTest1
    {
        

        public void TestComponents() {
            GameEntity gameEntity = new GameEntity();
            var mock = new MockComponent();
            Console.WriteLine("Add");
            Console.WriteLine(gameEntity.AddComponent(mock));
            Console.WriteLine("Get<>");
            Console.WriteLine(gameEntity.GetComponent<MockComponent>());
            Console.WriteLine("Get(Type)");
            Console.WriteLine(gameEntity.GetComponent(typeof(MockComponent)));
            Console.WriteLine("Get(Component)");
            Console.WriteLine(gameEntity.GetComponent(mock));
            Console.WriteLine("Remove");
            Console.WriteLine(gameEntity.RemoveComponent(mock));
            Assert.IsTrue(true);
#if PauseAfterTest
            Console.ReadKey();
#endif
        }
        public class MockComponent : IEntityComponent<IComponentsContainer>
        {
            public IComponentsContainer Parent { get ; set ; }
            public bool Activated { get ; set ; }

            public void Activate(IComponentsContainer parrent)
            {
                Console.WriteLine("ACTIVATED");
            }

            public void Deactivate(IComponentsContainer parrent)
            {
                Console.WriteLine("DEACTIVATED");
            }
        }
    }
}
