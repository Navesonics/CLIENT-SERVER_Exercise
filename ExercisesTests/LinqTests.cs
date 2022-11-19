

namespace ExercisesTests
{
    using ExercisesDAL;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    public class LinqTests
    {
        [Fact]
        public void Test1()
        {
            SomeSchoolContext _db = new();
            var selectedStudents = from stu in _db.Students
                                   where stu.Id == 2
                                   select stu;
            Assert.True(selectedStudents.Any());

        }
        [Fact]
        public void Test2()
        {
            SomeSchoolContext _db = new();
            var selectedStudents = from stu in _db.Students
                                   where stu.Title == "Ms." || stu.Title == "Mrs."
                                   select stu;
            Assert.True(selectedStudents.Any());

        }
        [Fact]
        public void Test3()
        {
            SomeSchoolContext _db = new();
            var selectedStudents = from stu in _db.Students
                                   join div in _db.Divisions on stu.DivisionId equals div.Id
                                   where div.Name == "Design"
                                   select stu;
            Assert.True(selectedStudents.Any());

        }

        [Fact]
        public void Test4()
        {
            SomeSchoolContext _db = new();
            Student? selectedStudent = _db.Students.FirstOrDefault(stu => stu.Id == 2);
            Assert.True(selectedStudent!.FirstName == "Gail");
        }

        [Fact]
        public void Test5()
        {
            SomeSchoolContext _db = new();
            var selectedStudents = _db.Students.Where(stu => stu.Title == "Ms." || stu.Title == "Mrs.");
            Assert.True(selectedStudents.Any());
        }

        [Fact]
        public void Test6()
        {
            SomeSchoolContext _db = new();
            var selectedStudents = _db.Students.Where(stu => stu.Division.Name == "Design");
            Assert.True(selectedStudents.Any());
        }
        /*
        [Fact]
        public async Task Test7()
        {
            SomeSchoolContext _db = new();
            Student? selectedStudent = await _db.Students.FirstOrDefaultAsync(stu => stu.LastName == "Cross");
            if (selectedStudent != null)
            {
                string oldPhoneNo = selectedStudent.PhoneNo!;
                string newPhoneNo = oldPhoneNo == "(519)-555-1234" ? "(555)-555-5555" : "(519)-555-1234";
                selectedStudent.PhoneNo = newPhoneNo;
                _db.Entry(selectedStudent).CurrentValues.SetValues(selectedStudent);
            }
            Assert.True(await _db.SaveChangesAsync() == 1);
        }

        [Fact]
        public async Task Test8()
        {
            SomeSchoolContext _db = new();
            Student newStudent = new()
            {
                FirstName = "Joe",
                LastName = "Smith",
                PhoneNo = "(555)555-1234",
                Title = "Mr.",
                DivisionId = 10,
                Email = "js@someschool.com"
            };
            await _db.Students.AddAsync(newStudent);
            await _db.SaveChangesAsync();
            Assert.True(newStudent.Id > 0);
        }
        */
        [Fact]
        public async Task Test9()
        {
            SomeSchoolContext _db = new();
            Student? selectedStudent = await _db.Students.FirstOrDefaultAsync(stu => stu.FirstName == "Evan" && stu.LastName == "Benitez");
            if (selectedStudent != null)
            {
                _db.Students.Remove(selectedStudent);
                Assert.True(await _db.SaveChangesAsync() == 1); // # of rows deleted
            }
            else
            {
                Assert.True(false);
            }
        }

        [Fact]
        public async Task Test7()
        {
            SomeSchoolContext _db = new();
            Student? selectedStudent = await _db.Students.FirstOrDefaultAsync(stu => stu.LastName == "Benitez");
            if (selectedStudent != null)
            {
                string oldEmail = selectedStudent.Email!;
                string newEmail = oldEmail == "js@someschool.com" ? "eb@someschool.com" : "js@someschool.com";
                selectedStudent.Email = newEmail;
                _db.Entry(selectedStudent).CurrentValues.SetValues(selectedStudent);
            }
            Assert.True(await _db.SaveChangesAsync() == 1);
        }

        [Fact]
        public async Task Test8()
        {
            SomeSchoolContext _db = new();
            Student newStudent = new()
            {
                FirstName = "Evan",
                LastName = "Benitez",
                PhoneNo = "(555)555-1234",
                Title = "Mr.",
                DivisionId = 10,
                Email = "js@someschool.com"
            };
            await _db.Students.AddAsync(newStudent);
            await _db.SaveChangesAsync();
            Assert.True(newStudent.Id > 0);
        }


    }
}