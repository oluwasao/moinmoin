using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Interface.TagApp2._0
{
    interface ICategory
    {
        System.Xml.Linq.XDocument LoadCategoryFromXml(string path);
    }
}
