using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using CreateSystem.Common;
using CreateSystem.DataAccess.DataEntity;
using CreateSystem.Model;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;

namespace CreateSystem.DataAccess
{
    public class LocalDataAccess
    {
        private static LocalDataAccess instance;

        private LocalDataAccess() { }

        public static LocalDataAccess GetInstance()
        {
            return instance ?? (instance = new LocalDataAccess());
        }

        SqlConnection conn;
        SqlCommand comm;
        SqlDataAdapter adapter;

        /// <summary>
        /// 操作完成后对三个变量进行注销
        /// </summary>
        private void Dispose()
        {
            if (adapter != null)
            {
                adapter.Dispose();
                adapter = null;
            }
            if (comm != null)
            {
                comm.Dispose();
                comm = null;
            }
            if (conn != null)
            {
                conn.Close();
                conn.Dispose();
                conn = null;
            }
        }

        /// <summary>
        /// 连接初始化
        /// </summary>
        private bool DBConnection()
        {
            //配置管理器
            string connStr = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
            if (conn == null)
            {
                conn = new SqlConnection(connStr);
            }
            try
            {
                conn.Open();
                return true;
            }
            catch
            {

                return false;
            }
        }

        /// <summary>
        /// 获取用户表信息，用于对比用户输入的是否跟数据库一致
        /// </summary>
        /// <param name="userName">用户输入的名字</param>
        /// <param name="pwd">用户输入的密码</param>
        /// <returns></returns>
        public UserEntity CheckUserInfo(string userName, string pwd)
        {
            try
            {
                if (DBConnection())
                {
                    //查询符合条件的数据
                    string userSql = "select * from create_system_users where user_name=@user_name and password=@password and is_validation=1";
                    //预编译
                    adapter = new SqlDataAdapter(userSql, conn);
                    //对密码加密和用户名写入到adapter属性中
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@user_name", SqlDbType.VarChar) { Value = userName });
                    adapter.SelectCommand.Parameters.Add(new SqlParameter("@password", SqlDbType.VarChar) { Value = MD5Provider.GetMD5String(pwd + "@" + userName) });

                    DataTable table = new DataTable();
                    int count = adapter.Fill(table);

                    if (count <= 0)
                    {
                        throw new Exception("用户名或密码不正确！");
                    }
                    DataRow dataRow = table.Rows[0];
                    if (dataRow.Field<Int32>("is_can_login") == 0)
                    {
                        throw new Exception("当前用户没有权限使用此平台！");
                    }

                    UserEntity userInfo = new UserEntity();
                    userInfo.UserName = dataRow.Field<string>("user_name");
                    userInfo.RealName = dataRow.Field<string>("real_name");
                    userInfo.Password = dataRow.Field<string>("password");
                    userInfo.Avatar = dataRow.Field<string>("avatar");
                    userInfo.Gender = dataRow.Field<int>("gender");
                    return userInfo;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                this.Dispose();
            }
            return null;
        }

        public List<MonsterSeriesModel> GetMonsterSeriesModels()
        {
            try
            {
                List<MonsterSeriesModel> monsterSeriesModels = new List<MonsterSeriesModel>();
                if (DBConnection())
                {
                    //查询符合条件的数据(三表连查)
                    string userSql = @"select a.course_name, a.course_id,b.play_count,b.is_growing,b.growing_rate,c.platform_name from create_system_courses a
                                       left join create_system_play_record b
                                       on a.course_id = b.course_id
                                       left join create_system_platforms c
                                       on b.platform_id = c.platform_id
                                       order by a.course_id,c.platform_id";
                    //预编译
                    adapter = new SqlDataAdapter(userSql, conn);

                    DataTable table = new DataTable();
                    int count = adapter.Fill(table);

                    string courseId = "";
                    MonsterSeriesModel cModel = null; //为防止生成多个一样id的对象 

                    foreach (DataRow dr in table.AsEnumerable())
                    {
                        string tempId = dr.Field<string>("course_id");
                        if (courseId != tempId)
                        {
                            courseId = tempId;
                            cModel = new MonsterSeriesModel();
                            monsterSeriesModels.Add(cModel);

                            cModel.MonsterName = dr.Field<string>("course_name");
                            cModel.SeriesCollection = new LiveCharts.SeriesCollection();
                            cModel.MonsterSeriesDataModels = new System.Collections.ObjectModel.ObservableCollection<MonsterSeriesDataModel>();
                        }
                        if (cModel != null)
                        {
                            cModel.SeriesCollection.Add(new PieSeries
                            {
                                Title = dr.Field<string>("platform_name"),
                                Values = new ChartValues<ObservableValue> { new ObservableValue((double)dr.Field<decimal>("play_count")) },
                                DataLabels = false
                            });

                            cModel.MonsterSeriesDataModels.Add(new MonsterSeriesDataModel
                            {
                                MonsterSeriesName = dr.Field<string>("platform_name"),
                                CurrentValue = dr.Field<decimal>("play_count"),
                                IsGrowing = dr.Field<Int32>("is_growing") == 1,
                                ChangeRate = (int)dr.Field<decimal>("growing_rate")
                            });
                        }
                    }
                    return monsterSeriesModels;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                this.Dispose();
            }
            return null;
        }
    }
}
