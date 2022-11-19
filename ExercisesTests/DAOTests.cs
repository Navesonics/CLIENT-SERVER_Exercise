using Xunit;
using ExercisesDAL;
using System.Threading.Tasks;
using System.Collections.Generic;
namespace ExerciseTests
{
    public class DAOTests
    {
        [Fact]
        public async Task Student_GetByLastnameTest()
        {
            StudentDAO dao = new();
            Student selectedStudent = await dao.GetByLastname("Cross");
            Assert.NotNull(selectedStudent);
        }

        [Fact]
        public async Task Student_GetByIdTest()
        {
            StudentDAO dao = new();
            int id = 1;
            Student selectedStudent = await dao.GetById(id);
            Assert.NotNull(selectedStudent);
        }

        [Fact]
        public async Task Student_GetAllTest()
        {
            StudentDAO dao = new();
            List<Student> allStudents = await dao.GetAll();
            Assert.True(allStudents.Count > 0);
        }

        [Fact]
        public async Task Student_AddTest()
        {
            StudentDAO dao = new();
            Student newStudent = new()
            {
                FirstName = "Joe",
                LastName = "Smith",
                PhoneNo = "(555)555-1234",
                Title = "Mr.",
                DivisionId = 10,
                Email = "js@someschool.com"
            };
            Assert.True(await dao.Add(newStudent) > 0);
        }

        //[Fact]
        //public async Task Student_UpdateTest()
        //{
        //    StudentDAO dao = new();
        //    Student? studentForUpdate = await dao.GetByLastname("Smith");
        //    if (studentForUpdate != null)
        //    {
        //        string oldPhoneNo = studentForUpdate.PhoneNo!;
        //        string newPhoneNo = oldPhoneNo == "519-555-1234" ? "555-555-5555" : "519-555-1234";
        //        studentForUpdate!.PhoneNo = newPhoneNo;
        //    }
        //    Assert.True(await dao.Update(studentForUpdate!) == 1); // 1 indicates the # of rows updated
        //}
        [Fact]
        public async Task Student_UpdateTest()
        {
            StudentDAO dao = new();
            Student? studentForUpdate = await dao.GetByLastname("Smith");
            if (studentForUpdate != null)
            {
                string oldPhoneNo = studentForUpdate.PhoneNo!;
                string newPhoneNo = oldPhoneNo == "519-555-1234" ? "555-555-5555" : "519-555-1234";
                studentForUpdate!.PhoneNo = newPhoneNo;
            }
            Assert.True(await dao.Update(studentForUpdate!) == UpdateStatus.Ok); // 1 indicates the # of rows updated
        }

        [Fact]
        public async Task Student_DeleteTest()
        {
            StudentDAO dao = new();
            Student? studentForUpdate = await dao.GetByLastname("Smith");
            if (studentForUpdate != null)
            {
                Assert.True(await dao.Delete(studentForUpdate.Id!) == 1); // # of rows deleted
            }
            else
            {
                Assert.True(false);
            }
        }

        public async Task Student_AddTest2()
        {
            StudentDAO dao = new();
            Student newStudent = new()
            {
                FirstName = "Evan Wilson",
                LastName = "Benitez",
                PhoneNo = "(555)555-22331",
                Title = "Mr.",
                DivisionId = 10,
                Email = "ewb@someschool.com"
            };
            Assert.True(await dao.Add(newStudent) > 0);
        }

        [Fact]
        public async Task Student_ConcurrencyTest()
        {
            StudentDAO dao1 = new();
            StudentDAO dao2 = new();
            Student studentForUpdate1 = await dao1.GetByLastname("Smith");
            Student studentForUpdate2 = await dao2.GetByLastname("Smith");
            if (studentForUpdate1 != null)
            {
                string? oldPhoneNo = studentForUpdate1.PhoneNo;
                string? newPhoneNo = oldPhoneNo == "519-555-1234" ? "555-555-5555" : "519-555-1234";
                studentForUpdate1.PhoneNo = newPhoneNo;
                if (await dao1.Update(studentForUpdate1) == UpdateStatus.Ok)
                {
                    // need to change the phone # to something else
                    studentForUpdate2.PhoneNo = "666-666-6668";
                    Assert.True(await dao2.Update(studentForUpdate2) == UpdateStatus.Stale);
                }
                else
                    Assert.True(false); // first update failed
            }
            else
                Assert.True(false); // didn't find student 1
        }

        [Fact]
        public async Task Student_LoadPicsTest()
        {
            {
                ExercisesDALPicUtil util = new();
                Assert.True(await util.AddStudentPicsToDb());
            }
        }

    }
}