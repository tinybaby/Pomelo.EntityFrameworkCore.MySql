﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.Extensions.Logging;
using Pomelo.Data.MySql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Pomelo.EntityFrameworkCore.MySql.Test
{
 
    public class MysqlRelationalConnectionTest
    {
         [Fact]
        public void Creates_Npgsql_Server_connection_string()
        {
            
            using (var connection = new MySqlRelationalConnection(CreateOptions(), new Logger<MySqlConnection>(new LoggerFactory())))
            {
                Assert.IsType<MySqlConnection>(connection.DbConnection);
            }
        }

        //[Fact]
        public void Can_create_master_connection_string()
        {
            using (var connection = new MySqlRelationalConnection(CreateOptions(), new Logger<MySqlConnection>(new LoggerFactory())))
            {
                using (var master = connection.CreateMasterConnection())
                {
                    Assert.Equal(@"server=localhost;port=3306;Database=mysql;user id=root;password=root", master.ConnectionString);
                }
            }
        }

        public static IDbContextOptions CreateOptions()
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseMySql(@"Server=localhost;Port=3306;Database=ef;Uid=root;Password=root");

            return optionsBuilder.Options;
        }
    }
}
