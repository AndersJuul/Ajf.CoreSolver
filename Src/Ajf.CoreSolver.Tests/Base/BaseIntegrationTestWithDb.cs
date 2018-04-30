using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Diagnostics;
using Ajf.CoreSolver.DbModels;
using NUnit.Framework;

namespace Ajf.CoreSolver.Tests.Base
{
    [TestFixture]
    public abstract class BaseIntegrationTestWithDb:BaseTest
    {
        [SetUp]
        public void SetUp1()
        {
        }

        [TearDown]
        public void TearDown()
        {
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Database.SetInitializer(new TestInitializer());

            //ConnectionString = $"Server=JuulServer2017;Database={_dbName};User Id=rideshare;Password=rideshare";
            var sqlConnectionStringBuilder = new SqlConnectionStringBuilder(ConfigurationManager.ConnectionStrings["CoreSolverConnection"].ConnectionString);
            _dbName = sqlConnectionStringBuilder.InitialCatalog + "-Test." + Environment.MachineName +
                      DateTime.Now.ToString("yyyy-MM-dd.HH.mm.ss");
            sqlConnectionStringBuilder.InitialCatalog = _dbName;
            ConnectionString = sqlConnectionStringBuilder.ConnectionString;

            DbContext = new CoreSolverContext { Database = { Connection = { ConnectionString = ConnectionString } } };
            DbContext.Database.Initialize(true);

            //AutoMapperInitializor.Init();
        }

        public CoreSolverContext DbContext { get; set; }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            TearDownDatabase();
        }

        private void TearDownDatabase()
        {
            using (var con = new SqlConnection(ConnectionString))
            {
                con.Open();
                con.ChangeDatabase("master");
                new SqlCommand(@"ALTER DATABASE [" + _dbName + @"] SET SINGLE_USER WITH ROLLBACK IMMEDIATE",
                        con)
                    .ExecuteNonQuery();
                new SqlCommand(@"DROP DATABASE [" + _dbName + "]",
                        con)
                    .ExecuteNonQuery();
            }

            Debug.WriteLine("Tore down test db: " + _dbName);
        }

        protected string ConnectionString;
        private string _dbName;
    }
}