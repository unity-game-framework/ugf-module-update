using System.Collections;
using NUnit.Framework;
using UGF.Application.Runtime;
using UGF.Update.Runtime;
using UnityEngine;
using UnityEngine.TestTools;

namespace UGF.Module.Update.Runtime.Tests
{
    public class TestUpdateModule
    {
        private class Target : IUpdateHandler
        {
            public int Counter { get; private set; }

            public void OnUpdate()
            {
                Counter++;
            }
        }

        [Test]
        public void Initialize()
        {
            IApplication application = CreateApplication("Module");

            application.Initialize();

            var module = application.GetModule<IUpdateModule>();

            Assert.NotNull(module);
            Assert.AreEqual(module.Provider.Groups.Count, 1);

            application.Uninitialize();

            Assert.AreEqual(module.Provider.Groups.Count, 0);
        }

        [UnityTest]
        public IEnumerator Update()
        {
            IApplication application = CreateApplication("Module");

            application.Initialize();

            var module = application.GetModule<IUpdateModule>();
            var group = module.GetGroup<IUpdateGroupDescribed>("4614ceca8914e5b4d8326f86aded3229");

            Assert.NotNull(group);
            Assert.AreEqual("Group", group.Name);

            var target = new Target();

            group.Collection.Add(target);

            yield return null;

            Assert.AreEqual(1, target.Counter);

            application.Uninitialize();
        }

        private IApplication CreateApplication(string moduleName)
        {
            return new ApplicationConfigured(new ApplicationResources
            {
                new ApplicationConfig
                {
                    Modules =
                    {
                        (IApplicationModuleBuilder)Resources.Load(moduleName, typeof(IApplicationModuleBuilder))
                    }
                }
            });
        }
    }
}
