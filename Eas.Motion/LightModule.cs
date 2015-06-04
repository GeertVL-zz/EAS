using System.Collections.Generic;
using System.Linq;

namespace Eas.Motion
{
    public class LightModule
    {
        private readonly IRecoverySystem _recoverySystem;
        private readonly List<string> _logs;
        private readonly List<Light> _lights;
        private bool _alarm;

        public IList<string> Logs
        {
            get { return _logs; }
        }

        public IList<Light> Lights
        {
            get { return _lights; }
        }
        public bool Alarm
        {
            get { return _alarm; }
        }

        public LightModule(IRecoverySystem recoverySystem)
        {
            _recoverySystem = recoverySystem;
            _logs = new List<string>();
            _lights = new List<Light>();
            _alarm = false;
        }

        public Light GetSpecificLight(LightType lightType)
        {
            var specificLight = _lights.Find(i => i.LightType.Equals(lightType));
            if(specificLight == null)
            {
                Logs.Add(string.Format("Light {0} not found", lightType));
                throw new LightNotFoundException();
            }
            if (_recoverySystem != null && !_recoverySystem.PingLight(specificLight.Number))
            {
                _alarm = true;
                Logs.Add(string.Format("Light {0} is not available.", lightType));
                throw new LightNotAvailableException();
            }
            return specificLight;
        }  

        public void TurnOn(LightType lightType)
        {
            Lights
                .Where(i => i.LightType == lightType)
                .ToList()
                .ForEach(i => i.Turn(true));
        }
      
        public IEnumerable<Light> IsTurnedOn()
        {
            return Lights.Where(i => i.Status);
        }        
    }
}