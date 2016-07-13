using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace DataGridCustomDemo
{
    public class MainWindowVModel : BaseVModel
    {
        private ObservableCollection<Student> _studentList;

        public ObservableCollection<Student> StudentList
        {
            get
            {
                return _studentList;
            }
            set
            {
                this._studentList = value;
                this.OnPropertyChanged("StudentList");
            }
        }

        public MainWindowVModel()
        {
            //LoadData();
            SetPageControlUC();
        }

        public void LoadData()
        {
            if (StudentList == null || StudentList.Count <= 0)
                StudentList = new ObservableCollection<Student>(new List<Student>() { 
                new Student(){StudentNumber="20160100",Name="test1",ClassNo="Class1",EnrollmentTime=DateTime.Now},
                new Student(){StudentNumber="20160101",Name="test2",ClassNo="Class1",EnrollmentTime=DateTime.Now},
                new Student(){StudentNumber="20160102",Name="test3",ClassNo="Class1",EnrollmentTime=DateTime.Now},
                new Student(){StudentNumber="20160103",Name="test4",ClassNo="Class1",EnrollmentTime=DateTime.Now},
                new Student(){StudentNumber="20160104",Name="test5",ClassNo="Class1",EnrollmentTime=DateTime.Now},
            });
        }

        #region 设置分页控制控件逻辑
        private void SetPageControlUC()
        {
            int pagedatacount = 0;
            PagerVModel.AcPageIndex = delegate(object para)
            {
                //GetAttendRecData(PagerVModel.PageSize, PagerVModel.PageIndex, out pagedatacount);
                LoadData();
                PagerVModel.PageDataCount = pagedatacount;
            };
            PagerVModel.AcPageIndex.BeginInvoke(null, null, null);
        }
        #endregion

        #region "PagerVModel" 分页控制Model
        private BaseMultiPagerVModel _pagerVModel;

        public BaseMultiPagerVModel PagerVModel
        {
            get
            {
                return _pagerVModel ?? (_pagerVModel = new BaseMultiPagerVModel());
            }
            set
            {
                this._pagerVModel = value;
                this.OnPropertyChanged("PagerVModel");
            }
        }
        #endregion
        
    }
}
