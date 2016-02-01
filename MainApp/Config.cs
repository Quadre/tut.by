using System;

namespace Configuration
{
    public class Config
    {
        private string domain = @"http://tut.by/"; // default value
        public string Domain { get { return domain; } set { domain = value; } }
    }
}