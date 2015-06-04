using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eas.Motion;
using FakeItEasy;
using Xunit;

namespace Eas.Tests
{
    public class TurnOnForwardLights
    {
        private readonly IRecoverySystem _recoverySystem;
        private readonly LightModule _module;

        public TurnOnForwardLights()
        {
            _recoverySystem = A.Fake<IRecoverySystem>();
            A.CallTo(() => _recoverySystem.PingLight(1)).Returns(true);
            _module = new LightModule(_recoverySystem);
            _module.Lights.Add(new Light(1, LightType.Front));
            _module.Lights.Add(new Light(2, LightType.Rear));
        }

        [Fact(DisplayName = "Turn on Forward light")]
        public void TurnOn()
        {
            var light = _module.GetSpecificLight(LightType.Front);
            light.Turn(on: true);
            Assert.Equal(1, _module.IsTurnedOn().Count());
        }

        
        [Fact(DisplayName = "Turn on 2 forward lights")]
        public void TurnOnTwoForwardLights()
        {
            _module.Lights.Add(new Light(11, LightType.Front));
            _module.TurnOn(LightType.Front);
            Assert.Equal(2, _module.IsTurnedOn().Count());
        }

        
        [Fact(DisplayName = "Turn on no forward lights")]
        public void TurnOnNoForwardLights()
        {
            _module.Lights.RemoveAt(0);
            _module.TurnOn(LightType.Front);
            Assert.Equal(0, _module.IsTurnedOn().Count());
        }
        
    }
}
