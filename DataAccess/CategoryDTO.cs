using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CategoryDTO
    {
        string _ID;

        public string ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }
    }
}
