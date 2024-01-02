using Check_In_Check_Out_System.Data;
using Check_In_Check_Out_System.Interfaces;
using Check_In_Check_Out_System.Models;
using System.Linq;

namespace Check_In_Check_Out_System.Repository
{
    public class RecordRepository : IRecordRepository
    {

        private readonly ApplicationDbContext _context;

        public RecordRepository(ApplicationDbContext context)
        {
            _context = context;
        }


        public Record GetRecordByInt(int id)
        {
            return _context.Records.Where(r => r.Id == id).FirstOrDefault();
        }

        public IEnumerable<Record> GetRecordsByName(string name)
        {
            return _context.Records.Where(f => f.FullName.Contains(name)).ToList();
        }

        public Record GetRecordByName(string name)
        {
            return _context.Records.Where(n => n.FullName == name).FirstOrDefault();
        }

        public IEnumerable<Record> GetRecordsByDate(DateTime date)
        {
            return _context.Records.Where(f => f.Date.Date == date).ToList();
        }

        public bool Save()
        {
           var saved = _context.SaveChanges();
           return saved > 0 ? true: false;
        }

        public bool SignIn(Record record)
        {
            _context.Add(record);
            return Save();
        }

        public bool SignOut(Record record)
        {
            var signOutTime = DateTime.Now.ToString();
            Record recordToUpdate = GetRecordByName(record.FullName);
            recordToUpdate.CheckOut = signOutTime;
            _context.Update(recordToUpdate);
            return Save();
        }

        public IEnumerable<Record> GetRecords()
        {
            return _context.Records.OrderBy(r => r.Id);
        }

        public bool HasSignedInToday(string name)
        {
            // Check if the person has already been registered on the current day
            DateTime today = DateTime.Now.Date;

            var existingRecord = _context.Records.Where(a => a.FullName == name && a.Date == today).FirstOrDefault();

            return existingRecord != null;
        }

        public bool RecordIdExists(int id)
        {
            return _context.Records.Any(i => i.Id == id);
        }
        public bool NameExists(string name)
        {
            return _context.Records.Any(i => i.FullName.Contains(name));
        }
        public bool DateExists(DateTime date)
        {
            return _context.Records.Any(i => i.Date.Date == date);
        }
    }
}
