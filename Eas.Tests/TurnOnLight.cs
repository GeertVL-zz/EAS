using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Eas.Motion;
using FakeItEasy;
using Xunit;

namespace Eas.Tests
{
    public class TurnOnLight
    {
        [Fact(DisplayName = "System asks LightModule for specific light")]
        public void SystemAsksSpecificLight()
        {   string specificLight = "Left light";
            var module = new LightModule(null);
            module.Lights.Add(new Light(1, specificLight));
            
            Light light = module.GetSpecificLight(name: specificLight);
            Assert.Equal(specificLight, light.Name);
        }

        
        [Fact(DisplayName = "System asks Light Module for specific light and not found throws exception")]
        public void WhenLightNotFoundThrowException()
        {
            string specificLight = "DoNotExist";
            var module = new LightModule(null);            
            Assert.Throws<LightNotFoundException>(() => module.GetSpecificLight(name: specificLight));            
        }



        [Fact(DisplayName = "System asks Light Module for specific light and not found adds log entry")]
        public void WhenLightNotFoundAddLogEntry()
        {
            string specificLight = "DoNotExist";
            var module = new LightModule(null);            
            Assert.Throws<LightNotFoundException>(() => module.GetSpecificLight(name: specificLight)); 
            Assert.Equal(1, module.Logs.Count);
        }

        
        [Fact(DisplayName = "Light cannot be recovered from the system throws exception")]
        public void LightNotAvailableThrowException()
        {
            var recoverySystem = A.Fake<IRecoverySystem>();
            A.CallTo(() => recoverySystem.PingLight(1)).Returns(false);
            var module = new LightModule(recoverySystem);
            var leftlight = "LeftLight";
            module.Lights.Add(new Light(1, leftlight));

            Assert.Throws<LightNotAvailableException>(() => module.GetSpecificLight(name: leftlight));
        }


        
        [Fact(DisplayName = "Light cannot be recovered from the system ignites alarm")]
        public void LightNotAvailableAlarm()
        {
            var recoverySystem = A.Fake<IRecoverySystem>();
            A.CallTo(() => recoverySystem.PingLight(1)).Returns(false);
            var module = new LightModule(recoverySystem);
            var leftlight = "LeftLight";
            module.Lights.Add(new Light(1, leftlight));

            Assert.Throws<LightNotAvailableException>(() => module.GetSpecificLight(name: leftlight));
            Assert.Equal(true, module.Alarm);
        }

        [Fact(DisplayName = "Light cannot be recovered from the system writes entry in log")]
        public void LightNotAvailableAddLogEntry()
        {
            var recoverySystem = A.Fake<IRecoverySystem>();
            A.CallTo(() => recoverySystem.PingLight(1)).Returns(false);
            var module = new LightModule(recoverySystem);
            var leftlight = "LeftLight";
            module.Lights.Add(new Light(1, leftlight));

            Assert.Throws<LightNotAvailableException>(() => module.GetSpecificLight(name: leftlight));
            Assert.Equal(1, module.Logs.Count);
        }
          
        [Fact(DisplayName = "TurnOnLight")]
        public void TurnOn()
        {
            var recoverySystem = A.Fake<IRecoverySystem>();
            A.CallTo(() => recoverySystem.PingLight(1)).Returns(true);
            var module = new LightModule(recoverySystem);
            var leftlight = "LeftLight";
            module.Lights.Add(new Light(1, leftlight));
            var light = module.GetSpecificLight(leftlight);
            light.Turn(on: true);

            Assert.True(light.Status);
        }

        
        [Fact(DisplayName = "TurnOffLight")]
        public void TurnOff()
        {
            var recoverySystem = A.Fake<IRecoverySystem>();
            A.CallTo(() => recoverySystem.PingLight(1)).Returns(true);
            var module = new LightModule(recoverySystem);
            var leftlight = "LeftLight";
            module.Lights.Add(new Light(1, leftlight));
            var light = module.GetSpecificLight(leftlight);
            light.Turn(on: true);
            light.Turn(on: false);

            Assert.False(light.Status);
        }
        
    }
}
