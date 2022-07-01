/*****************************************************************************
*项目名称:SwaggerWithMiniProfiler.Repository.ContextSugar
*项目描述:
*类 名 称:DataBaseInitializer
*类 描 述:
*创 建 人:Chuanmingxie
*创建时间:2022/5/31 18:54:55
*修 改 人:
*修改时间:
*作用描述:<FUNCTION>
*Copyright @ chuanming 2022. All rights reserved
******************************************************************************/
using Newtonsoft.Json;
using SwaggerWithMiniProfiler.Model.Entities;
using SwaggerWithMiniProfiler.Shared;
using SwaggerWithMiniProfiler.Shared.BaseDBOperate;
using SwaggerWithMiniProfiler.Shared.HelperTool;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SwaggerWithMiniProfiler.Repository.ContextSugar
{
    public class DataBaseInitializer
    {
        private static string SeedDataFolder = "CloudWhales.SeedData/{0}.tsv";

        /// <summary>
        /// 异步添加种子数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <param name="WebRootPath"></param>
        /// <returns></returns>
        public static async Task SeedAsync(DBSugarContext dbContext, string WebRootPath)
        {
            try
            {
                if (string.IsNullOrEmpty(WebRootPath))
                {
                    throw new Exception("获取wwwpathroot路径异常");
                }
                SeedDataFolder = Path.Combine(WebRootPath, SeedDataFolder);
                Console.WriteLine("********** Blog.Core DataBase Set **********");
                Console.WriteLine($"Is Multi-DataBase:{HelperAppSettings.app(new string[] { "DataBaseSetting", "MultiDBEnabled" })}");
                Console.WriteLine($"Is CQRS:{HelperAppSettings.app(new string[] { "DataBaseSetting", "CQRSEnabled" })}");
                Console.WriteLine();
                Console.WriteLine($"Master DB ConnectionID:{DBSugarContext.ConnectionID}");
                Console.WriteLine($"Master DB DataBaseType:{DBSugarContext.DbType}");
                Console.WriteLine($"Master DB ConnectionString:{DBSugarContext.ConnectionString}");
                Console.WriteLine();
                if (HelperAppSettings.app(new string[] { "DataBaseSetting", "MultiDBEnabled" }).ObjectToBool())
                {
                    var subordinateIndex = 0;
                    BaseDBConnection.MultiConnectionString.multiDBAll
                        .Where(x => x.ConnectionID != MainDb.CurrentDbConnId).ToList().ForEach(m =>
                    {
                        subordinateIndex++;
                        Console.WriteLine($"Subordinate {subordinateIndex} ConnectionID:{m.ConnectionID}");
                        Console.WriteLine($"Subordinate {subordinateIndex} DataBaseType:{m.DbType}");
                        Console.WriteLine($"Subordinate {subordinateIndex} ConnectionString:{m.ConnectionString}");
                    });
                }
                else if (HelperAppSettings.app(new string[] { "DataBaseSetting", "CQRSEnabled" }).ObjectToBool())
                {
                    var subordinateIndex = 0;
                    BaseDBConnection.MultiConnectionString.dbSubordinate
                        .Where(x => x.ConnectionID != MainDb.CurrentDbConnId).ToList().ForEach(m =>
                        {
                            subordinateIndex++;
                            Console.WriteLine($"Subordinate {subordinateIndex} ConnectionID:{m.ConnectionID}");
                            Console.WriteLine($"Subordinate {subordinateIndex} DataBaseType:{m.DbType}");
                            Console.WriteLine($"Subordinate {subordinateIndex} ConnectionString:{m.ConnectionString}");
                        });
                }
                Console.WriteLine();

                //建库
                CreateDataBaseFrame(dbContext);
                //建表
                CreateDataBaseTable(dbContext);
                //建数据
                await CreateDataBaseData(dbContext);

                Console.WriteLine();
            }
            catch (Exception ex)
            {
                throw new Exception("其他错误:" + ex.Message);
            }
        }

        /// <summary>
        /// 为数据表添加数据
        /// </summary>
        /// <param name="dbContext"></param>
        /// <returns></returns>
        private async static Task CreateDataBaseData(DBSugarContext dbContext)
        {
            if (HelperAppSettings.app(new string[] { "DataBaseSetting", "SeedDBDataEnabled" }).ObjectToBool())
            {
                JsonSerializerSettings settings = new JsonSerializerSettings();
                JsonConvert.DefaultSettings = new Func<JsonSerializerSettings>(() =>
                {
                    settings.DateFormatHandling = DateFormatHandling.MicrosoftDateFormat;
                    settings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                    settings.NullValueHandling = NullValueHandling.Ignore;
                    return settings;
                });

                Console.WriteLine($"Seeding Database data (The DB ID:{DBSugarContext.ConnectionID})...");

                #region BlogArticle

                if (!await dbContext.DB.Queryable<BlogArticle>().AnyAsync())
                {
                    var data = FileHelper.ReadFile(string.Format(SeedDataFolder, "BlogArticle"), Encoding.UTF8);
                    dbContext.GetEnrityDB<BlogArticle>().InsertRange(HelperJson.ParseFormByJson<List<BlogArticle>>(data));
                    Console.WriteLine("Table : BlogArticle Created Success!");
                }
                else
                    Console.WriteLine("Table : BlogArticle Already Existes...");

                #endregion

                #region ApiMenuModules

                if (!await dbContext.DB.Queryable<ApiMenuModules>().AnyAsync())
                {
                    var data = FileHelper.ReadFile(string.Format(SeedDataFolder, "ApiMenuModules"), Encoding.UTF8);
                    dbContext.GetEnrityDB<ApiMenuModules>().InsertRange(JsonConvert.DeserializeObject<List<ApiMenuModules>>(data, settings));
                    Console.WriteLine("Table : ApiMenuModules Created Success!");
                }
                else
                    Console.WriteLine("Table : ApiMenuModules Already Existes...");

                #endregion

                #region ApiPermissionModules

                if (!await dbContext.DB.Queryable<ApiPermissionModules>().AnyAsync())
                {
                    var data = FileHelper.ReadFile(string.Format(SeedDataFolder, "ApiPermissionModules"), Encoding.UTF8);
                    dbContext.GetEnrityDB<ApiPermissionModules>().InsertRange(JsonConvert.DeserializeObject<List<ApiPermissionModules>>(data, settings));
                    Console.WriteLine("Table : ApiPermissionModules Created Success!");
                }
                else
                    Console.WriteLine("Table : ApiPermissionModules Already Existes...");

                #endregion

                #region ApiRoleMenuPermission

                if (!await dbContext.DB.Queryable<ApiRoleMenuPermission>().AnyAsync())
                {
                    var data = FileHelper.ReadFile(string.Format(SeedDataFolder, "ApiRoleMenuPermission"), Encoding.UTF8);
                    dbContext.GetEnrityDB<ApiRoleMenuPermission>().InsertRange(JsonConvert.DeserializeObject<List<ApiRoleMenuPermission>>(data, settings));
                    Console.WriteLine("Table : ApiRoleMenuPermission Created Success!");
                }
                else
                    Console.WriteLine("Table : ApiRoleMenuPermission Already Existes...");

                #endregion

                #region BlogTopic

                if (!await dbContext.DB.Queryable<BlogTopic>().AnyAsync())
                {
                    var data = FileHelper.ReadFile(string.Format(SeedDataFolder, "BlogTopic"), Encoding.UTF8);
                    dbContext.GetEnrityDB<BlogTopic>().InsertRange(JsonConvert.DeserializeObject<List<BlogTopic>>(data, settings));
                    Console.WriteLine("Table : BlogTopic Created Success!");
                }
                else
                    Console.WriteLine("Table : BlogTopic Already Existes...");

                #endregion

                #region BlogTopicDetial

                if (!await dbContext.DB.Queryable<BlogTopicDetial>().AnyAsync())
                {
                    var data = FileHelper.ReadFile(string.Format(SeedDataFolder, "BlogTopicDetial"), Encoding.UTF8);
                    dbContext.GetEnrityDB<BlogTopicDetial>().InsertRange(JsonConvert.DeserializeObject<List<BlogTopicDetial>>(data, settings));
                    Console.WriteLine("Table : BlogTopicDetial Created Success!");
                }
                else
                    Console.WriteLine("Table : BlogTopicDetial Already Existes...");

                #endregion

                #region SystemRoleType

                if (!await dbContext.DB.Queryable<SystemRoleType>().AnyAsync())
                {
                    var data = FileHelper.ReadFile(string.Format(SeedDataFolder, "SystemRoleType"), Encoding.UTF8);
                    dbContext.GetEnrityDB<SystemRoleType>().InsertRange(JsonConvert.DeserializeObject<List<SystemRoleType>>(data, settings));
                    Console.WriteLine("Table : SystemRoleType Created Success!");
                }
                else
                    Console.WriteLine("Table : SystemRoleType Already Existes...");

                #endregion

                #region SystemUserRole

                if (!await dbContext.DB.Queryable<SystemUserRole>().AnyAsync())
                {
                    var data = FileHelper.ReadFile(string.Format(SeedDataFolder, "SystemUserRole"), Encoding.UTF8);
                    dbContext.GetEnrityDB<SystemUserRole>().InsertRange(JsonConvert.DeserializeObject<List<SystemUserRole>>(data, settings));
                    Console.WriteLine("Table : SystemUserRole Created Success!");
                }
                else
                    Console.WriteLine("Table : SystemUserRole Already Existes...");

                #endregion

                #region SystemUserInfo

                if (!await dbContext.DB.Queryable<SystemUserInfo>().AnyAsync())
                {
                    var data = FileHelper.ReadFile(string.Format(SeedDataFolder, "SystemUserInfo"), Encoding.UTF8);
                    dbContext.GetEnrityDB<SystemUserInfo>().InsertRange(JsonConvert.DeserializeObject<List<SystemUserInfo>>(data, settings));
                    Console.WriteLine("Table : SystemUserInfo Created Success!");
                }
                else
                    Console.WriteLine("Table : SystemUserInfo Already Existes...");

                #endregion

                #region ScheduleTask

                if (!await dbContext.DB.Queryable<ScheduleTask>().AnyAsync())
                {
                    var data = FileHelper.ReadFile(string.Format(SeedDataFolder, "ScheduleTask"), Encoding.UTF8);
                    dbContext.GetEnrityDB<ScheduleTask>().InsertRange(JsonConvert.DeserializeObject<List<ScheduleTask>>(data, settings));
                    Console.WriteLine("Table : ScheduleTask Created Success!");
                }
                else
                    Console.WriteLine("Table : ScheduleTask Already Existes...");

                #endregion

                HelperConsole.WriteSuccessLine($"Done Seeding DataBase");
            }
        }

        /// <summary>
        /// 为数据库添加表结构
        /// </summary>
        /// <param name="dbContext"></param>
        private static void CreateDataBaseTable(DBSugarContext dbContext)
        {
            //创建数数据库的表，便利指定命名空间下的class,不应加载多余命名空间
            Console.WriteLine("Crate Tables....");
            var path = AppDomain.CurrentDomain.RelativeSearchPath ?? AppDomain.CurrentDomain.BaseDirectory;

            var refernceAssemblies = Directory
                .GetFiles(path, "SwaggerWithMiniProfiler.Model.dll")
                .Select(Assembly.LoadFrom).ToArray();

            var modelTypes = refernceAssemblies
                .SelectMany(a => a.DefinedTypes)
                .Select(type => type.AsType())
                .Where(x => x.IsClass && x.Namespace != null && x.Namespace.Equals("SwaggerWithMiniProfiler.Model.Entities"))
                .ToList();

            modelTypes.ForEach(t =>
            {
                if (!dbContext.DB.DbMaintenance.IsAnyTable(t.Name))
                {
                    Console.WriteLine(t.Name);
                    dbContext.DB.CodeFirst.InitTables(t);
                }
            });

            HelperConsole.WriteSuccessLine($"Tables Created successfully!");
            Console.WriteLine();
        }

        /// <summary>
        /// 创建数据库物理架构
        /// </summary>
        /// <param name="dbContext"></param>
        private static void CreateDataBaseFrame(DBSugarContext dbContext)
        {
            Console.WriteLine($"Create Database (The DB Id):{DBSugarContext.ConnectionID}");
            if (DBSugarContext.DbType != SqlSugar.DbType.Oracle)
            {
                //创建数据库(此时没有表和数据)
                dbContext.DB.DbMaintenance.CreateDatabase();
                HelperConsole.WriteSuccessLine($"DataBase Carete Successfully！");
            }
            else
            {
                //Oracle 数据库不支持数据库
                HelperConsole.WriteSuccessLine($"Oracle 数据库不支持该操作,可以手动创建Oracle 数据库！");
            }
        }
    }
}
