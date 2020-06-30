using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Top_lista_vremena.Models
{
    public class TopListRepository : ITopListRepository
    {
        private static List<TopTime> _topTimeList;
        private static List<TopTime> _unapprovedTopTimeList;
        private readonly IConfiguration configuration;
        public readonly string connectionString;
        public TopListRepository(IConfiguration config)
        {
            _topTimeList = new List<TopTime>();
            _unapprovedTopTimeList = new List<TopTime>();
            this.configuration = config;
            connectionString = configuration.GetConnectionString("TopListConnection");

        }
        public bool AddTopListRecord(TopTime record)
        {
            if (_topTimeList == null)
                _topTimeList = new List<TopTime>();
            if (_topTimeList.Count != 0)
                record.ID = _topTimeList.Max(x => x.ID) + 1;
            else
                record.ID = 1;

            _topTimeList.Add(record);

            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spAddTopListRecord", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@name", record.Name);
                    cmd.Parameters.AddWithValue("@surname", record.Surname);
                    cmd.Parameters.AddWithValue("@time", record.Time);
                    connection.Open();
                    cmd.ExecuteReader();
                    connection.Close();
                }
                catch (Exception e)
                {
                    return false;
                    throw e;
                }
            }
            return true;
        }

        public List<TopTime> GetTopList()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spGetTopListRecords", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        TopTime record = new TopTime();
                        record.ID = (int)dr[0];
                        record.Name = dr[1].ToString();
                        record.Surname = dr[2].ToString();
                        record.Time = TimeSpan.Parse(dr[3].ToString());

                        _topTimeList.Add(record);
                    }
                    connection.Close();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return _topTimeList;
        }

        public List<TopTime> GetUnapprovedTopList()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("spGetUnapprovedTopListRecords", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    connection.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        TopTime record = new TopTime();
                        record.ID = (int)dr[0];
                        record.Name = dr[1].ToString();
                        record.Surname = dr[2].ToString();
                        record.Time = TimeSpan.Parse(dr[3].ToString());

                        _unapprovedTopTimeList.Add(record);
                    }
                    connection.Close();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return _unapprovedTopTimeList;
        }
    }
}
