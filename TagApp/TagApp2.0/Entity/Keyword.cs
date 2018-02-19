using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TagApp2._0.Entity
{
    public class Keyword
    {
        private string _keyWord;
        private string _adjective;

        public string KeyWord
        {
            get
            {
                return _keyWord;
            }
        }

        public string Adjective
        {
            get
            {
                return _adjective;
            }
        }

        public Keyword(string keyword)
        {
            _keyWord = keyword;            
        }

        public Keyword(string keyword, string adjective)
        {
            _keyWord = keyword;
            _adjective = adjective;
        }
    }
}
