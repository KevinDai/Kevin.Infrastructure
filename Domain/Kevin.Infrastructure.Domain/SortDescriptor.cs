using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kevin.Infrastructure.Domain
{
    public class ListSortDescription
    {

        public ListSortDescription(String property, ListSortDirection direction)
        {
            this.PropertyDescriptor = property;
            this.SortDirection = direction;
        }

        public string PropertyDescriptor
        {
            get;
            set;
        }

        public ListSortDirection SortDirection
        {
            get;
            set;
        }
    }
}
