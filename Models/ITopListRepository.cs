using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Top_lista_vremena.Models
{
    public interface ITopListRepository
    {
        List<TopTime> GetTopList();
        bool AddTopListRecord(TopTime record);
    }
}
