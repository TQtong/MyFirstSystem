using CreateSystem.Common;
using CreateSystem.DataAccess;
using CreateSystem.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace CreateSystem.ViewModel
{
    public class CenterViewModel
    {
        /// <summary>
        /// 大小分类
        /// </summary>
        public ObservableCollection<MonsterCategoryItemModel> MonsterSizeClass { get; set; }

        /// <summary>
        /// 外貌分类
        /// </summary>
        public ObservableCollection<MonsterCategoryItemModel> MonsterStyleClass { get; set; }

        /// <summary>
        /// 爱好分类
        /// </summary>
        public ObservableCollection<MonsterCategoryItemModel> MonsterHobbyClass { get; set; }

        /// <summary>
        /// 查询后怪兽对象，用于渲染界面
        /// </summary>
        public ObservableCollection<MonsterModel> MonsterModels { get; set; } = new ObservableCollection<MonsterModel>();

        /// <summary>
        /// 原始怪兽对象，用于数据过滤
        /// </summary>
        private List<MonsterModel> OriginalMonsterModel { get; set; } = new List<MonsterModel>();

        /// <summary>
        /// 点击跳转命令
        /// </summary>
        public CommandBase OpenCourseUrlCommand { get; set; } = new CommandBase();

        /// <summary>
        /// 过滤怪兽管理者命名
        /// </summary>
        public CommandBase SelectMonsterManagerCommand { get; set; } = new CommandBase();


        public CenterViewModel()
        {
            OpenCourseUrlCommand.DoCanExecute = new Func<object, bool>((o) => true);
            OpenCourseUrlCommand.DoExecute = new Action<object>((o) => { System.Diagnostics.Process.Start(o.ToString()); });//打开链接

            SelectMonsterManagerCommand.DoCanExecute = new Func<object, bool>((o) => true);
            SelectMonsterManagerCommand.DoExecute = new Action<object>(SelectMonsterManager);

            InitCategory();

            InitMonsterModel();
        }

        /// <summary>
        /// 通过怪兽管理者查询数据
        /// </summary>
        /// <param name="o"></param>
        private void SelectMonsterManager(object o)
        {
            string teacher = o.ToString();
            List<MonsterModel> temp = OriginalMonsterModel;
            if (teacher != "全部")
            {
                temp = OriginalMonsterModel.Where(c => c.MonsterManagers.Exists(e => e == teacher)).ToList();
            }
            MonsterModels.Clear();

            foreach (var item in temp)
                MonsterModels.Add(item);
        }

        private void InitCategory()
        {
            MonsterSizeClass = new ObservableCollection<MonsterCategoryItemModel>();
            MonsterSizeClass.Add(new MonsterCategoryItemModel("全部", true));
            MonsterSizeClass.Add(new MonsterCategoryItemModel("公开课"));
            MonsterSizeClass.Add(new MonsterCategoryItemModel("VIP课程"));

            MonsterStyleClass = new ObservableCollection<MonsterCategoryItemModel>();
            MonsterStyleClass.Add(new MonsterCategoryItemModel("全部", true));
            MonsterStyleClass.Add(new MonsterCategoryItemModel("C#"));
            MonsterStyleClass.Add(new MonsterCategoryItemModel("ASP.NET"));
            MonsterStyleClass.Add(new MonsterCategoryItemModel("微服务"));
            MonsterStyleClass.Add(new MonsterCategoryItemModel("Java"));
            MonsterStyleClass.Add(new MonsterCategoryItemModel("Vue"));
            MonsterStyleClass.Add(new MonsterCategoryItemModel("微信小程序"));
            MonsterStyleClass.Add(new MonsterCategoryItemModel("Winform"));
            MonsterStyleClass.Add(new MonsterCategoryItemModel("WPF"));
            MonsterStyleClass.Add(new MonsterCategoryItemModel("上位机"));

            MonsterHobbyClass = new ObservableCollection<MonsterCategoryItemModel>();
            MonsterHobbyClass.Add(new MonsterCategoryItemModel("全部", true));
            foreach (var item in LocalDataAccess.GetInstance().GetMonsterName())
                MonsterHobbyClass.Add(new MonsterCategoryItemModel(item));
        }

        /// <summary>
        /// 初始化怪兽对象
        /// </summary>
        private void InitMonsterModel()
        {
            for (int i = 0; i < 10; i++)
            {
                MonsterModels.Add(new MonsterModel { IsShowSkeleton = true });
            }
            Task.Run(new Action(async () =>
            {
                OriginalMonsterModel = LocalDataAccess.GetInstance().GetMonsterModel();
                await Task.Delay(4000);

                System.Windows.Application.Current.Dispatcher.Invoke(new Action(() =>
                {
                    MonsterModels.Clear();
                    foreach (var item in OriginalMonsterModel)
                        MonsterModels.Add(item);
                }));
            }));
        }
    }
}
