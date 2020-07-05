using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace Top_Records.Models
{
    public interface IRecordsRepository
    {
        List<Record> GetRecords();
        List<Record> AddRecord(Record record);
        List<Record> UpdateRecord(Record updatedRecord);
        void ConfirmationURL(string confirmURL, Record record, string viewName);
    }
}
