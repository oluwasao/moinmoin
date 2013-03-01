using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
namespace TagApp.Entity
{
   public class Category : Interface.TagApp.ICategory
    {        
        public System.Xml.Linq.XDocument LoadCategoryFromXml(string path)
        {
            XDocument document = XDocument.Load(path);
            return document;
        }

        public static List<Category> LoadCategories(string path)
        {
            List<Category> catList = new List<Category>();
            XDocument categoriesDoc = XDocument.Load(path);
            var categories = from cat in categoriesDoc.Element("Categories").Elements()
                             where cat.Name == "Category"
                             select cat;
            foreach (var xElement in categories)
            {
                var xName = (from e in xElement.Elements()
                             where e.Name == "Name"
                             select e).FirstOrDefault();
                Category c = new Category()
                                 {
                                     Name = xName.Value.Trim()
                                 };
                catList.Add(c);
            }
            return catList;
        }

       public string Name { get; set; }
    }
    
}
