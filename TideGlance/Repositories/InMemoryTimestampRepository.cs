using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TideGlance.Repositories
{
    internal class InMemoryTimestampRepository : ITimestampRepository
    {
        private SortedList<DateTime, List<object>> Repository { get; } = [];
        private int count = 0;

        public void Append(RecordTimestamp record)
        {
            if (Repository.TryGetValue(record.Timestamp, out var values))
                values.Add(record);
            else
                Repository.Add(record.Timestamp, [record]);
            count++;
        }

        public int CleanBefore(DateTime threshold)
        {
            if (Repository.Keys.Last() < threshold)
            {
                Repository.Clear();
                var total = count;
                count = 0;
                return total;
            }

            var i = 0;
            while (Repository.Keys[0] < threshold)
            {
                count -= Repository.Values[0].Count();
                Repository.RemoveAt(0);
                i++;
            }
            return i;
        }

        public int CountRecords()
            => count;

        public int CountTimestamps()
            => Repository.Count();

    }
}
