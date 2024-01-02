using Check_In_Check_Out_System.Interfaces;
using Check_In_Check_Out_System.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using NodaTime;

namespace Check_In_Check_Out_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordController : ControllerBase
    {
        private readonly IRecordRepository _recordRepository;

        public RecordController(IRecordRepository recordRepository)
        {
            _recordRepository = recordRepository;
        }


        [HttpPost("signIn")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult SignIn([FromBody] Record record)
        {
            //Check if Form is empty
            if (record == null)
            {
                return BadRequest();
            }

            //Check if Name already exists that day
            //var newSignIn = _recordRepository.GetRecordByName(record.FullName);
            //var today = DateTime.Now.Date;
            if (_recordRepository.HasSignedInToday(record.FullName))
            {
                ModelState.AddModelError("", "User already signed in");
                return StatusCode(500, ModelState);
            }

            //Check if model state is valid
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }


            var newRecord = new Record
            {
                FullName = record.FullName,
                Date = DateTime.Now.Date,
                CheckIn = DateTime.Now.ToString(),
                CheckOut = null
            };

            if (!_recordRepository.SignIn(newRecord))
            {
                ModelState.AddModelError("", "Something went wrong when signing in");
                return StatusCode(500, ModelState);
            }

            return Ok("Successfully Signed in");
        }

        [HttpPut("signOut")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        public IActionResult SignOut([FromBody] Record record)
        {
            //check if form is empty
            if (record == null)
            {
                return BadRequest();
            }

            //check if user signed in today
            if (!_recordRepository.HasSignedInToday(record.FullName))
            {
                ModelState.AddModelError("", "User hasn't signed in today");
                return StatusCode(500, ModelState);
            }

            //check if model state is valid
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            if (_recordRepository.SignOut(record) == false)
            {
                ModelState.AddModelError("", "Something went wrong while signing out");
                return StatusCode(500, ModelState);
            }
            return Ok("Sucessfully signed out");

        }


        [HttpGet("getRecords")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]

        public IActionResult GetRecords()
        {
            var records = _recordRepository.GetRecords();
            return Ok(records);
        }

        [HttpGet("getRecord/{recordId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetRecordByInt(int recordId)
        {
            //check if model state is valid
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            //check if record exists
            if (!_recordRepository.RecordIdExists(recordId))
            {
                return NotFound();
            }

            var record = _recordRepository.GetRecordByInt(recordId);
            return Ok(record);
        }

        [HttpGet("getNames/{name}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetNames(string name)
        {
            //check if record exists
            if (!_recordRepository.NameExists(name))
            {
                return NotFound();
            }

            var record = _recordRepository.GetRecordsByName(name);
            //check if model state is valid
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }


            return Ok(record);
        }

        [HttpGet("getRecordsByDate/{date}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public IActionResult GetRecordsByDate(DateTime date)
        {
            
            //var dateString = date.Date.ToString();
            //check if record exists
            if (_recordRepository.DateExists(date) == false)
            {
                return NotFound("Can't find date");
            }

            var record = _recordRepository.GetRecordsByDate(date);

            //check if model state is valid
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            return Ok(record);
        }

    }
}