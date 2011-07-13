using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kevin.Infrastructure.Domain
{
    public class PageList<T>
    {

        public PageList(IEnumerable<T> list, int pageIndex, int pageCount, int totalCount)
        {
            _list = list;
            _pageIndex = pageIndex;
            _pageCount = pageCount;
            _totalCount = totalCount;
        }

        public IEnumerable<T> List
        {
            get
            {
                return _list;
            }
        }
        private IEnumerable<T> _list;

        public int PageIndex
        {
            get
            {
                return _pageIndex;
            }

        }
        private int _pageIndex;

        public int PageCount
        {
            get
            {
                return _pageCount;
            }
        }
        private int _pageCount;

        public int TotalCount
        {
            get
            {
                return _totalCount;
            }
        }
        private int _totalCount;
    }
}
