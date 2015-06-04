namespace Eas.Motion
{
    public class Light
    {
        private bool _status;
        private readonly int _number;
        private readonly LightType _type;

        public Light() { }

        public Light(int number, LightType type)
        {
            _number = number;
            _type = type;
        }

        public bool Status 
        {
            get { return _status; }
        }
        public LightType LightType 
        {
            get { return _type; }
        }

        public int Number 
        {
            get { return _number; }
        }

        public double Frequency { get; set; }
        public double Wattage { get; set; }    
        
        public void Turn(bool on)
        {
            _status = on;
        }
    }
}