namespace Eas.Motion
{
    public class Light
    {
        private bool _status;
        private readonly int _number;
        private readonly string _name;

        public Light() { }

        public Light(int number, string name)
        {
            _number = number;
            _name = name;
        }

        public bool Status 
        {
            get { return _status; }
        }
        public string Name 
        {
            get { return _name; }
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