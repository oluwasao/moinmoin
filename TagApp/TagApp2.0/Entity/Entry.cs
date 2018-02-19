using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TagApp2._0.Entity
{
    public class Entry
    {
        private readonly Guid _id = Guid.NewGuid();

        public Guid Id
        {
            get { return _id; }
        }

        public List<Tag> Tags { get; set; }

        public Entry()
        {
            Tags = new List<Tag>();
        }
    }
}
