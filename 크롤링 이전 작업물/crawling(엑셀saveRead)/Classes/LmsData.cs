using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace crawling.Classes
{
    public class LmsData
    {
        private string lmsSubject;
        private string lmsTitle;
        private string lmsWriter;
        private string lmsRdate;

     

        public string LmsSubject
        {
            get
            {
                return lmsSubject;
            }
            set
            {
                lmsSubject = value;
            }
        }
        public string LmsTitle
        {
            get
            {
                return lmsTitle;
            }
            set
            {
                lmsTitle = value;
            }
        }
        public string LmsWriter
        {
            get
            {
                return lmsWriter;
            }
            set
            {
                lmsWriter = value;
            }
        }
        public string LmsRdate
        {
            get
            {
                return lmsRdate;
            }
            set
            {
                lmsRdate = value;
            }
        }

    }
}
