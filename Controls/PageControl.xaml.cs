using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DataGridCustomDemo
{
    /// <summary>
    /// PageControl.xaml 的交互逻辑
    /// </summary>
    public partial class PageControl : UserControl
    {
        public PageControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty PageInfoProperty = DependencyProperty.Register("PageInfo", typeof(string), typeof(PageControl), new PropertyMetadata(DataPropertyChanged));
        public static readonly DependencyProperty PageSizeProperty = DependencyProperty.Register("PageSize", typeof(int), typeof(PageControl), new PropertyMetadata(13, DataPropertyChanged));
        public static readonly DependencyProperty PageSizeInitProperty = DependencyProperty.Register("PageSizeInit", typeof(int), typeof(PageControl), new PropertyMetadata(13, PageSizeInitChanged));        
        public static readonly DependencyProperty PageIndexProperty = DependencyProperty.Register("PageIndex", typeof(int), typeof(PageControl), new PropertyMetadata(0, DataPropertyChanged));
        public static readonly DependencyProperty PageDataCountProperty = DependencyProperty.Register("PageDataCount", typeof(int), typeof(PageControl), new PropertyMetadata(0, DataPropertyChanged));
        public static readonly DependencyProperty IsLastPageProperty = DependencyProperty.Register("IsLastPage", typeof(bool), typeof(PageControl), new PropertyMetadata(true));
        public static readonly DependencyProperty PageIndexCommandProperty = DependencyProperty.Register("PageIndexCommand", typeof(ICommand), typeof(PageControl));

        public int PageSizeInit
        {
            get { return (int)GetValue(PageSizeInitProperty); }
            set { SetValue(PageSizeInitProperty,value); }
        }

        public string PageInfo
        {
            get { return (string)GetValue(PageInfoProperty); }
            set { SetValue(PageInfoProperty, value); }
        }

        public int PageSize
        {
            get { return (int)GetValue(PageSizeProperty); }
            set { SetValue(PageSizeProperty, value); }
        }

        public int PageIndex
        {
            get { return (int)GetValue(PageIndexProperty); }
            set { SetValue(PageIndexProperty, value); }
        }

        public int PageDataCount
        {
            get { return (int)GetValue(PageDataCountProperty); }
            set { SetValue(PageDataCountProperty, value); }
        }

        public bool IsLastPage
        {
            get { return (bool)GetValue(IsLastPageProperty); }
            set { SetValue(IsLastPageProperty, value); }
        }


        public ICommand PageIndexCommand
        {
            get { return (ICommand)GetValue(PageIndexCommandProperty); }
            set { SetValue(PageIndexCommandProperty, value); }
        }

        public int PageCount { get; set; }

        private static void DataPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue == e.NewValue)
                return;
            PageControl pctrl = (PageControl)d;
            pctrl.DealWithPropertyChanged(pctrl, e);
            //throw new NotImplementedException();
        }

        private static void PageSizeInitChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (e.OldValue == e.NewValue)
                return;
            //throw new NotImplementedException();
        }

        private void DealWithPropertyChanged(PageControl pctrl, DependencyPropertyChangedEventArgs obj)
        {
            if (obj.Property.Name == "PageInfo")
            {
                this.tbInfo.Text = (string)obj.NewValue;
                return;
            }
            if (PageSize == 0)
                return;
            PageCount = PageDataCount % PageSize > 0 ? (PageDataCount / PageSize + 1) : PageDataCount / PageSize;
            if (PageSize > 0 && PageDataCount > 0)
            {
                this.tbContent.Text = string.Format("{0}/{1}", PageIndex + 1, PageCount);
            }
            if (PageDataCount == 0) //updated by ChiWei 2016-06-21
            {
                this.tbContent.Text = string.Format("0/0");
            }
        }

        private void btPre_Click(object sender, RoutedEventArgs e)
        {
            if (PageIndex <= 0)
                return;
            PageIndex--;
            DealWithPager(PageIndex);
        }

        private void btNxt_Click(object sender, RoutedEventArgs e)
        {
            if (PageIndex + 1 == PageCount)
                return;
            PageIndex++;
            DealWithPager(PageIndex);
        }

        private void DealWithPager(int pageindex)
        {
            IsLastPage = (PageIndex + 1) == PageCount;
            if (PageIndexCommand != null) ExecutePageing();
            //this.tbInfo2.Text = IsLastPage.ToString();
        }

        private void ExecutePageing()
        {
            Dictionary<string, int> dic = new Dictionary<string, int>();
            dic.Add("PageSize", PageSize);
            dic.Add("PageDataCount", PageDataCount);
            dic.Add("PageIndex", PageIndex);
            PageIndexCommand.Execute(dic);
        }
        //public virtual event PropertyChangedEventHandler PropertyChanged;



        public event PropertyChangedEventHandler PropertyChanged;
    }
}
