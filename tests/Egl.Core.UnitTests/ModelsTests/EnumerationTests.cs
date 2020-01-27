using Egl.Core.Models;
using Xunit;

namespace Egl.Core.UnitTests.ModelsTests
{
    public class EnumerationTests
    {
        [Fact]
        public void IsValidEnumrationFromValue()
        {
            var all = Enumeration.GetAll<State>();

            foreach (var item in all)
            {
                var state = Enumeration.FromValue<State>(item.Id);
                Assert.Equal(state.Name, item.Name);
            }
        }

        [Fact]
        public void IsValidEnumerationFromDisplayName()
        {
            var all = Enumeration.GetAll<State>();

            foreach (var item in all)
            {
                var state = Enumeration.FromDisplayName<State>(item.Name);
                Assert.Equal(state.Id, item.Id);
            }
        }
    }

    public class State : Enumeration
    {
        protected State(int id, string name) : base(id, name) { }

        public static State Alabama = new AlabamaState();
        public static State Alaska = new AlaskaState();
        public static State Arizona = new ArizonaState();
        public static State California = new CaliforniaState();

        private class AlabamaState : State
        {
            public AlabamaState() : base(7, "Alabama") { }
        }

        private class AlaskaState : State
        {
            public AlaskaState() : base(1, "Alaska") { }
        }

        private class ArizonaState : State
        {
            public ArizonaState() : base(9, "Arizona") { }
        }

        private class CaliforniaState : State
        {
            public CaliforniaState() : base(53, "California") { }
        }

    }
}
