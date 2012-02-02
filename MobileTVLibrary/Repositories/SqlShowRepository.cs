using System;
using System.Data.SqlClient;
using MobileTVLibrary.Models;
using MobileTVLibrary.Models.Factories;

namespace MobileTVLibrary.Repositories
{
    /// <summary>
    /// Repository for Shows
    /// </summary>
    public class SqlShowRepository : IRepository<Show>
    {
        private string connectionString;
        private IFactory<Show> factory;

        public SqlShowRepository(IFactory<Show> factory, string connectionString)
        {
            this.factory = factory;
            this.connectionString = connectionString;
        }

        #region IShowRepository Members

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Show Find(int id)
        {
            throw new NotImplementedException();
        }

        public Show Insert(Show show)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                //string insertSql = "";
            }
            return show;
        }

        public void Update(Show show)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}