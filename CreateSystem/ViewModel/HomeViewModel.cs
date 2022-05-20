using CreateSystem.Common;
using CreateSystem.DataAccess;
using CreateSystem.Model;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateSystem.ViewModel
{
    public class HomeViewModel : BindObject
    {
        private int _instrumentValue;
        /// <summary>
        /// 仪表盘的值
        /// </summary>
        public int InstrumentValue
        {
            get { return _instrumentValue; }
            set { _instrumentValue = value; OnPropertyChanged(); }
        }

        private int _min = 0;
        /// <summary>
        /// 仪表盘最小值
        /// </summary>
        public int Min
        {
            get => _min;
            set
            {
                _min = value;
                OnPropertyChanged();
            }
        }

        private int _max = 100;
        /// <summary>
        /// 仪表盘最大值
        /// </summary>
        public int Max
        {
            get => _max;
            set
            {
                _max = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 访问数据
        /// </summary>
        public ObservableCollection<MonsterSeriesModel> MonsterSeriesModels { get; set; } = new ObservableCollection<MonsterSeriesModel>();

        private int _itemCount;
        /// <summary>
        /// 内容数据的最大值(用于表的列个数绑定，防止换行导致显示有问题)
        /// </summary>
        public int ItemCount
        {
            get => _itemCount;
            set
            {
                _itemCount = value;
                OnPropertyChanged();
            }
        }


        public HomeViewModel()
        {
            RefreshInstrumentValue();
            InitMonsterSeries();
        }

        Random random = new Random();
        bool taskSwitch = true;
        List<Task> tasks = new List<Task>();
        /// <summary>
        /// 加载仪表盘
        /// </summary>
        private void RefreshInstrumentValue()
        {
            var task = Task.Factory.StartNew(async () =>
            {
                while (taskSwitch)
                {
                    InstrumentValue = random.Next(Math.Min(InstrumentValue - 5, 0), Math.Max(InstrumentValue + 5, 100));
                    await Task.Delay(4000);
                }
            });
            tasks.Add(task);
        }

        private void InitMonsterSeries()
        {
            #region 测试数据
            //MonsterSeriesModels.Add(new MonsterSeriesModel()
            //{
            //    MonsterName = "哈哈怪",
            //    SeriesCollection = new LiveCharts.SeriesCollection() { new PieSeries() {
            //        Title ="哈",
            //        Values = new ChartValues<ObservableValue>(){new ObservableValue(10)},
            //        DataLabels = false
            //    } },
            //    MonsterSeriesDataModels = new ObservableCollection<MonsterSeriesDataModel>()
            //    {
            //        new MonsterSeriesDataModel()
            //        {
            //            MonsterSeriesName = "哈",
            //            CurrentValue = 10,
            //            IsGrowing = false,
            //            ChangeRate = 10
            //        },
            //        new MonsterSeriesDataModel()
            //        {
            //            MonsterSeriesName = "哈",
            //            CurrentValue = 10,
            //            IsGrowing = false,
            //            ChangeRate = 10
            //        },
            //        new MonsterSeriesDataModel()
            //        {
            //            MonsterSeriesName = "哈",
            //            CurrentValue = 10,
            //            IsGrowing = false,
            //            ChangeRate = 10
            //        },
            //        new MonsterSeriesDataModel()
            //        {
            //            MonsterSeriesName = "哈",
            //            CurrentValue = 10,
            //            IsGrowing = false,
            //            ChangeRate = 10
            //        },
            //        new MonsterSeriesDataModel()
            //        {
            //            MonsterSeriesName = "哈",
            //            CurrentValue = 10,
            //            IsGrowing = false,
            //            ChangeRate = 10
            //        },
            //    }
            //});
            #endregion
            var list = LocalDataAccess.GetInstance().GetMonsterSeriesModels();
            ItemCount = list.Max(x => x.SeriesCollection.Count);
            list.ForEach(x => MonsterSeriesModels.Add(x));
        }

        public void Dispose()
        {
            try
            {
                taskSwitch = false;
                Task.WaitAll(tasks.ToArray());
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
