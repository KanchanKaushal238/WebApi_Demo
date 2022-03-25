using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GetPost_WebApi_Demo.Controllers
{
    public class EmployeesController : ApiController
    {
        public IEnumerable<Employee> Get()
        {
            using (EMPLOYEE_DATABASEEntities entities = new EMPLOYEE_DATABASEEntities())
            {
                return entities.Employees.ToList();
            }
        }
        public Employee Get(int id)
        {
            using (EMPLOYEE_DATABASEEntities entities = new EMPLOYEE_DATABASEEntities())
            {
                return entities.Employees.FirstOrDefault(e => e.ID == id);

            }
        }
        public IHttpActionResult Post(Employee emp)
        {
            using (var entities = new EMPLOYEE_DATABASEEntities())
            {
                entities.Employees.Add(new Employee()
                {
                    ID = emp.ID,
                    FirstName = emp.FirstName,
                    LastName = emp.LastName,
                    Salary = emp.Salary,
                    Gender = emp.Gender

                });
                entities.SaveChanges();

            }
            return Ok();
        }
        public  IHttpActionResult Put(Employee emp)
        {
            using (var entities = new EMPLOYEE_DATABASEEntities())
            {
                var update = (from s in entities.Employees where s.ID == emp.ID select s).FirstOrDefault<Employee>();
                if(update != null)
                {
                    update.FirstName = emp.FirstName;
                    update.LastName = emp.LastName;
                    update.Gender = emp.Gender;
                    update.Salary = emp.Salary;

                    entities.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            return Ok();
        }
        public IHttpActionResult Delete(int id)
        {
            if(id<=0)
            {
                return BadRequest("Not a valid id");
            }
            using (var entities = new EMPLOYEE_DATABASEEntities())
            {
                var delete = entities.Employees.Where(s=>s.ID == id).FirstOrDefault();
                entities.Entry(delete).State = System.Data.Entity.EntityState.Deleted;

                entities.SaveChanges();
               
            }
            return Ok();

        }
    }
}
