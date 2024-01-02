using Check_In_Check_Out_System.Models;

namespace Check_In_Check_Out_System.Interfaces
{
    public interface IRecordRepository
    {
        public bool SignIn(Record record);
        public bool SignOut(Record record);
        public bool HasSignedInToday(string name);
        public IEnumerable<Record> GetRecords();
        public IEnumerable<Record> GetRecordsByName(string name);
        public IEnumerable<Record> GetRecordsByDate(DateTime date);
        public Record GetRecordByInt(int id);
        public Record GetRecordByName(string name);
        public bool Save();
        public bool RecordIdExists(int id);
        public bool NameExists(string name);
        public bool DateExists(DateTime date);
    }
}
