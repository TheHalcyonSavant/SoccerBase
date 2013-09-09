using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Data.SqlClient;

namespace SoccerBase
{
    public abstract class AbstractBase
    {
        protected WebClient _wc = new WebClient();
        protected string _thisYear = DateTime.Today.Year.ToString();

        private Dictionary<string, string[]> _Links;

        public SqlConnection Connection;
        public int Index = -1;
        public string Table;
        public Dictionary<string, string[]> Links
        {
            get
            {
                return _Links;
            }
            set
            {
                _Links = value;
                this.enrLinks = value.GetEnumerator();
            }
        }
        public Dictionary<string, string[]>.Enumerator enrLinks;

        public AbstractBase()
        {
        }

        abstract public string ProcessLink();
    }
}
