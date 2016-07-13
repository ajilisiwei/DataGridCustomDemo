using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataGridCustomDemo
{
    /// <summary>
    /// 支持一个界面使用多个分页的模式
    /// </summary>
    public class BaseMultiPagerVModel : BaseVModel
    {

        private int _PageDataCount;
        private int _PageIndex = 0;
        private bool _IsLastPage;
        public Action<object> AcPageIndex = null;
        private RelayCommand _PageIndexChangedCommand;
        public RelayCommand PageIndexChangedCommand { 
            get { return _PageIndexChangedCommand ?? (_PageIndexChangedCommand = new RelayCommand(AcPageIndex)); }
            set { _PageIndexChangedCommand = value; }
        }

        /// <summary>
        /// 数据总行数
        /// </summary>
        public int PageDataCount
        {
            get { return _PageDataCount; }
            set { _PageDataCount = value; OnPropertyChanged("PageDataCount"); }
        }

        /// <summary>
        /// 当前的页面，从0开始
        /// </summary>
        public int PageIndex
        {
            get { return _PageIndex; }
            set { _PageIndex = value; OnPropertyChanged("PageIndex"); }
        }

        private int _pageSize=15;

        /// <summary>
        /// 每页面的总行数
        /// </summary>
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; OnPropertyChanged("PageSize"); }
        }

        /// <summary>
        /// 是否最后一条记录
        /// </summary>
        public bool IsLastPage
        {
            get { return _IsLastPage; }
            set { _IsLastPage = value; OnPropertyChanged("IsLastPage"); }
        }

        public BaseMultiPagerVModel(Action<object> ac)
        {
            AcPageIndex = ac;
        }
        public BaseMultiPagerVModel() { }

    }
}
