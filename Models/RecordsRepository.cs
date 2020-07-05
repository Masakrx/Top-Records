using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace Top_Records.Models
{
    public class RecordsRepository : IRecordsRepository
    {
        private static List<Record> _topRecords;
        private readonly IConfiguration configuration;
        private readonly string connectionString;
        private EmailData Email;
        public RecordsRepository(IConfiguration config)
        {
            _topRecords = new List<Record>();
            this.configuration = config;
            connectionString = configuration.GetConnectionString("TopRecordsConnection");
            Email = new EmailData(config);
            
        }
        
        public List<Record> GetRecords()
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("spGetRecords", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Record record = new Record();
                        record.ID = (int)dr[0];
                        record.Name = dr[1].ToString();
                        record.Surname = dr[2].ToString();                      
                        record.Time = TimeSpan.Parse(dr[3].ToString());
                        record.Email = dr[4].ToString();
                        record.Approved = (bool)dr[5];

                        _topRecords.Add(record);
                    }
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    connection.Close();
                }
            }
            return _topRecords;
        }

        public List<Record> AddRecord(Record record)
        {
            record.ID = (_topRecords.Max(x => x.ID) == null) ? 1 : _topRecords.Max(x => x.ID) + 1;
            _topRecords.Add(record);

            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("spAddRecord", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@name", SqlDbType.VarChar).Value = record.Name;
                    cmd.Parameters.Add("@surname", SqlDbType.VarChar).Value = record.Surname;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = record.Email;
                    cmd.Parameters.Add("@time", SqlDbType.Time).Value = record.Time;
                    cmd.ExecuteReader();
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    connection.Close();
                }
                
            }
            return _topRecords;
        }
     
        public List<Record> UpdateRecord(Record updatedRecord)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand("spUpdateRecord", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@Id", SqlDbType.Int).Value = updatedRecord.ID;
                    cmd.Parameters.Add("@isApproved", SqlDbType.Bit).Value = updatedRecord.Approved;
                    cmd.ExecuteReader();   
                }
                catch (Exception e)
                {
                    throw e;
                }
                finally
                {
                    connection.Close();
                }
            }

            if (!updatedRecord.Approved)
                _topRecords.Remove(updatedRecord);
                
            return _topRecords;
        }

        public void ConfirmationURL(string confirmURL,Record record, string viewName)
        {
            Email = new EmailData(record, viewName, confirmURL);
        }
    }
}
