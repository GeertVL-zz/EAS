using System.Collections.Generic;

namespace Eas.Motion
{
    public class LightModule
    {
        private readonly IRecoverySystem _recoverySystem;
        private readonly List<string> _logs;
        private readonly List<Light> _lights;
        private bool _alarm;

        public LightModule(IRecoverySystem recoverySystem)
        {
            _recoverySystem = recoverySystem;
            _logs = new List<string>();
            _lights = new List<Light>();
            _alarm = false;
        }

        public Light GetSpecificLight(string name)
        {
            var specificLight = _lights.Find(i => i.Name.Equals(name));
            if(specificLight == null)
            {
                Logs.Add(string.Format("Light {0} not found", name));
                throw new LightNotFoundException();
            }
            if (_recoverySystem != null && !_recoverySystem.PingLight(specificLight.Number))
            {
                _alarm = true;
                Logs.Add(string.Format("Light {0} is not available.", name));
                throw new LightNotAvailableException();
            }
            return specificLight;
        }

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
            get {  return _alarm; }
        }
    }
}