using ExercisesDAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ExercisesViewModels
{
    public class DivisionViewModel
    {
        readonly private DivisionDAO _dao;
        public string? Timer { get; set; }
        public string? Name { get; set; }
        public int? Id { get; set; }

        // constructor
        public DivisionViewModel()
        {
            _dao = new DivisionDAO();
        }

        //
        // Retrieve all the students as ViewModel instances
        //
        public async Task<List<DivisionViewModel>> GetAll()
        {
            List<DivisionViewModel> allVms = new();
            try
            {
                List<Division> allDivisions = await _dao.GetAll();
                // we need to convert Student instance to StudentViewModel because
                // the Web Layer isn't aware of the Domain class Student
                foreach (Division div in allDivisions)
                {
                    DivisionViewModel DivVm = new()
                    {
                        Name = div.Name,
                        Id = div.Id,
                        // binary value needs to be stored on client as base64
                        Timer = Convert.ToBase64String(div.Timer!)
                    };
                    allVms.Add(DivVm);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Problem in " + GetType().Name + " " +
                MethodBase.GetCurrentMethod()!.Name + " " + ex.Message);
                throw;
            }
            return allVms;
        }
    }
}
