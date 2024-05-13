using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using slot_3.Models;

namespace slot_3.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        // Tạo danh sách của 1 student

        private static List<Student> data = new List<Student>()
        {
            new Student("SV01", "Nguyen Van A", 20),
            new Student("SV02", "Nguyen Van B", 23),
            new Student("SV03", "Nguyen Van C", 21),
        };

        // tạo api/get getAllStudent()
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(data);
        }

        // Tạo api/get/1
        // Nếu không có kết quả thì show ra là "No data"

        [HttpGet("id")]
        public IActionResult GetStudentById(string id)
        {
            var student = data.Where(s => s.Code.Contains(id)).ToList();

            if (String.IsNullOrEmpty(id))
            {
                return BadRequest("id is required");
            }

            if (student.Count == 0)
            {
                return BadRequest("No data");
            }

            return Ok(student);
        }

        // Tạo api/post
        // check empty, check code existed, check age > 18

        [HttpPost]

        public IActionResult CreateStudent(Student student)
        {
            if (data.Where(s => s.Code.Contains(student.Code)).ToList().Count > 0)
                return BadRequest("duplicated student code");
            if (student.Age < 18 || student.Name == "" || student.Code == "") return BadRequest("Not Old Enough");
            data.Add(student);
            return Ok(student);
        }

        // Tạo api/put - update student in data
        // check empty, check age > 18

        [HttpPut("id")]

        public IActionResult UpdateStudent(Student student, string id)
        {
            if (id != student.Code) return BadRequest("cant change student code");
            if (data.Where(s => s.Code.Contains(student.Code)).ToList().Count > 0)
                return BadRequest("duplicated student code");
            if (student.Age < 18) return BadRequest("Not Old Enough");
            var students = data.Where(s => s.Code.Contains(id)).ToList();
            students.ForEach(s =>
            {
                s.Age = student.Age;
                s.Name = student.Name;
            });
            return Ok(students);
        }

        // tạo api/delete/1 - delete student by id

        public IActionResult DeleteStudent(string id)
        {
            var student = data.Where(s => s.Code.Contains(id)).ToList();
            if (String.IsNullOrEmpty(id))
            {
                return BadRequest("id is required");
            }

            if (student.Count == 0)
            {
                return BadRequest("No data");
            }
            student.ForEach(s =>
            {
                student.Remove(s);
            });
            return Ok(student);
        }
    
    // Xóa student mà id không tồn tại "Id not exists"
    }
}
