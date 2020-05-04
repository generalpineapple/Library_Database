using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryDatabaseWPF.Models
{
    public class Condition
    {
        public int ConditionId { get; }
        public ConditionType conditionType { get; }

        public Condition(int id, ConditionType type)
        {
            ConditionId = id;
            conditionType = type;
        }


    }
}
