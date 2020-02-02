#define PauseAfterTest
using System;
using System.ComponentModel;
using EMCS.Interfaces;
using EMCS.Interfaces.Components.Component;
using EMCS.Interfaces.Entity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xna.Framework;

namespace UnitTestsFramework
{
    [TestClass]
    public class UnitTest1
    {
        

        public void TestComponents() {
            //GameEntity gameEntity = new IGameEntity();
            //var mock = new MockComponent();
            //Console.WriteLine("Add");
            //Console.WriteLine(gameEntity.AddComponent(mock));
            //Console.WriteLine("Get<>");
            //Console.WriteLine(gameEntity.GetComponent<MockComponent>());
            //Console.WriteLine("Get(Type)");
            //Console.WriteLine(gameEntity.GetComponent(typeof(MockComponent)));
            //Console.WriteLine("Get(Component)");
            //Console.WriteLine(gameEntity.GetComponent(mock));
            //Console.WriteLine("Remove");
            //Console.WriteLine(gameEntity.RemoveComponent(mock));
            Assert.IsTrue(true);
#if PauseAfterTest
            Console.ReadKey();
#endif
        }
        public class MockComponent : IEntityComponent<IComponentsContainer>
        {
            public IComponentsContainer Parent { get ; set ; }
            public bool Activated { get ; set ; }

            public bool Enabled => throw new NotImplementedException();

            public int UpdateOrder => throw new NotImplementedException();

            public event Action<IEntityComponent<IComponentsContainer>> OnTryActivate;
            public event Action<IEntityComponent<IComponentsContainer>> OnActivated;
            public event Action<IEntityComponent<IComponentsContainer>> OnTryDeactevated;
            public event Action<IEntityComponent<IComponentsContainer>> OnDeactevated;
            public event Action<IEntityComponent<IComponentsContainer>> OnReset;
            public event EventHandler<EventArgs> EnabledChanged;
            public event EventHandler<EventArgs> UpdateOrderChanged;
            public event PropertyChangedEventHandler PropertyChanged;

            public void Activate(IComponentsContainer parrent)
            {
                Console.WriteLine("ACTIVATED");
            }

            public void Deactivate(IComponentsContainer parrent)
            {
                Console.WriteLine("DEACTIVATED");
            }

            public void Reset()
            {
                throw new NotImplementedException();
            }

            public void Update(GameTime gameTime)
            {
                throw new NotImplementedException();
            }
        }
    }
}
