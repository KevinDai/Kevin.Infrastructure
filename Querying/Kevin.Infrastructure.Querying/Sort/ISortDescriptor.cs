using System.ComponentModel;

namespace Kevin.Infrastructure.Querying
{

    public interface ISortDescriptor
    {
        string Member
        {
            get;
            set;
        }

        ListSortDirection SortDirection
        {
            get;
            set;
        }
    }
}
