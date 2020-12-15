using System;
using System.Collections.Generic;
using System.Text;

namespace Task1.Models
{
    public class Settings
    {
        int nesting;
        public int Nesting
        {
            set
            {
                nesting = Int16.Parse(value.ToString());
            }
            get { return nesting; }
        }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string EmailTo { get; set; }
        public string EmailFrom { get; set; }
        public string EmailFromPassword { get; set; }
        public string SmptAddress { get; set; }
        int smptPort;
        public int SmptPort
        {
            set
            {
                smptPort = Int16.Parse(value.ToString());
            }
            get { return smptPort; }
        }

    }
}
