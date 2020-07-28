namespace H21WebApplication
{
    internal class dbConnectionClass
    {
        private string connectioninfo;

        public dbConnectionClass()
        {
        }

        internal string Connectioninfo { 
            get => connectioninfo; 
            set => connectioninfo = value; 
        }
    }
}