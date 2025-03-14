using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Common.Repositories;
using OpticsWebApi.Entities;
using OpticsWebApi.RequestModels;

namespace OpticsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : BaseController
    {
        private readonly OpticsDBContext conText;

        public DoctorsController(OpticsDBContext context)
        {
            conText = context;
        }

        // GET: api/Doctors
        [HttpGet]
       
        public IActionResult GetDoctor([FromQuery]string? name, int pageNumber=1,int pageSize=10)
        {
           
            List<Doctor> doctors = new List<Doctor>();
            if (!string.IsNullOrEmpty(name))
            {
                doctors = conText.Doctors.Where(c => c.DoctorName == name).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }                     
            else
            {
                doctors = conText.Doctors.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
            return Ok(doctors);
       
           
        }

        // GET: api/Doctors/5
        [HttpGet("{id}")]
        public IActionResult GetDoctor(int id)
        {
            var doctor = conText.Doctors.Find(id);

            if (doctor == null)
            {
                return NotFound();
            }

            return Ok(doctor);
        }

        // PUT: api/Doctors/5
        [HttpPut("{id}")]
        public IActionResult PutDoctor(int id, CreateDoctorModel updatedDoctor)
        {

            Doctor doctor = conText.Doctors
                                .Where(i => i.Id == id)
                                .FirstOrDefault();
            if (doctor == null)
            {
                return NotFound();
            }

            doctor.DoctorName = updatedDoctor.DoctorName;
            doctor.DoctorSpecialization = updatedDoctor.DoctorSpecialization;
            doctor.DoctorPhone = updatedDoctor.DoctorPhone;
            doctor.DoctorEmail = updatedDoctor.DoctorEmail;
            doctor.DoctorFee = updatedDoctor.DoctorFee;
           

            conText.Doctors.Update(doctor);
            conText.SaveChanges();

            conText.Dispose();

            return Ok(doctor);
        }

        //POST: api/Doctors/CreateDoctors
        [HttpPost]
        public IActionResult CreateDoctor(CreateDoctorModel customerRequest)
        {
            Doctor doctor = new Doctor()
            {
                DoctorName = customerRequest.DoctorName,
                DoctorSpecialization = customerRequest.DoctorSpecialization,
                DoctorPhone = customerRequest.DoctorPhone,
                DoctorEmail = customerRequest.DoctorEmail,
                DoctorFee= customerRequest.DoctorFee,
                CreatedAt = DateTime.Now,

            };
            conText.Doctors.Add(doctor);
            conText.SaveChanges();

            return Created();
        }


        // DELETE: api/Doctors/5
        [HttpDelete("{id}")]
        public IActionResult DeleteDoctor(int id)
        {
            var doctor = conText.Doctors.Find(id);
            if (doctor == null)
            {
                return NotFound();
            }

            conText.Doctors.Remove(doctor);
            conText.SaveChanges();

            return NoContent();
        }


    }


}
