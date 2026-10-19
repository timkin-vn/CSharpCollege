using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MinesweeperGame.DataAccess.EF.DataContext;

namespace MinesweeperGame.UnitTest
{
    [TestClass]
    public class DatabaseInitTest
    {
        [TestMethod]
        public void CreateDatabaseTest()
        {
            using (var context = new GameDbContext())
            {
                // Инициализация базы данных
                context.Database.Initialize(force: false);

                // Проверка, что БД создана
                Microsoft.VisualStudio.TestTools.UnitTesting.Assert.IsTrue(
                    context.Database.Exists(),
                    "База данных должна существовать");
            }
        }
    }
}