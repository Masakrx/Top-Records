using System.Collections.Generic;

namespace Top_lista_vremena.Models
{
    public interface IRecordsRepository
    {
        List<Record> GetRecords();
        List<Record> AddRecord(Record record);
        List<Record> UpdateRecord(int Id, bool isApproved, string view);
    }
}
