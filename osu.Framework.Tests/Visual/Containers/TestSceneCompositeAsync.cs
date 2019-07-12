// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using osu.Framework.Graphics.Containers;

namespace osu.Framework.Tests.Visual.Containers
{
    public class TestSceneCompositeAsync : FrameworkTestScene
    {
        [SetUp]
        public void SetUp() => Schedule(Clear);

        [Test]
        public void TestAddToCompositeAsync()
        {
            AsyncLoadingContainer comp = null;
            bool disposed = false;

            AddStep("Add new composite", () =>
            {
                disposed = false;
                Add(comp = new AsyncLoadingContainer());
                comp.ChildContainer.OnDispose += () => disposed = true;
            });

            AddStep("Dispose composite", () =>
            {
                Remove(comp);
                comp.Dispose();
            });

            AddAssert("Is disposed", () => disposed);
        }

        private class AsyncLoadingContainer : CompositeDrawable
        {
            public Container ChildContainer { get; } = new Container();

            protected override void LoadComplete()
            {
                LoadComponentAsync(ChildContainer);
                base.LoadComplete();
            }
        }
    }
}
