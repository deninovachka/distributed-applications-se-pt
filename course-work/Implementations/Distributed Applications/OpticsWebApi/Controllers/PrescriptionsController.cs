using Common.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OpticsWebApi.Entities;
using OpticsWebApi.RequestModels;
using OpticsWebApi.ResultModels;
using System.Drawing;

namespace OpticsWebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrescriptionsController : BaseController
    {
        private readonly OpticsDBContext conText;

        public PrescriptionsController(OpticsDBContext context)
        {
            conText = context;
        }

        // GET: api/Prescription
        [HttpGet]
      
        public IActionResult GetPrescriptions([FromQuery] string? glassesType, int pageNumber = 1, int pageSize = 10)
        {
            List<Prescription> prescriptions = new List<Prescription>();
            if (!string.IsNullOrEmpty(glassesType))
            {
                prescriptions = conText.Prescriptions.Where(c => c.GlassesType == glassesType).Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                prescriptions = conText.Prescriptions.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            }
            return Ok(prescriptions);
        }

        // GET: api/Prescription/5
        [HttpGet("{id}")]
        public IActionResult GetPrescription(int id)
        {
            var prescriptionEntity = conText
            .Prescriptions
            .FirstOrDefault(p => p.Id == id); 

            if (prescriptionEntity == null)
            {
                return NotFound();
            }

            var prescription = new GetPrescriptionModel
            {
                DoctorID = prescriptionEntity.DoctorID,
                CustomerID = prescriptionEntity.CustomerID,
                PrescriptionDate = prescriptionEntity.PrescriptionDate,
                GlassesType = prescriptionEntity.GlassesType,
                SphOD = prescriptionEntity.SphOD,
                CylOD = prescriptionEntity.CylOD,
                AxOD = prescriptionEntity.AxOD,
                SphOS = prescriptionEntity.SphOS,
                CylOS = prescriptionEntity.CylOS,
                AxOS = prescriptionEntity.AxOS,
                PD = prescriptionEntity.PD,
            };

            return Ok(prescription);
        }

        // PUT: api/Prescription/5

        [HttpPut("{id}")]
        public IActionResult PutPrescription(int id, CreatePrescriptionModel updatedPrescription)
        {

            Prescription prescription = conText.Prescriptions
                                .Where(i => i.Id == id)
                                .FirstOrDefault();
            if (prescription == null)
            {
                return NotFound();
            }

            prescription.DoctorID = updatedPrescription.DoctorID;
            prescription.CustomerID = updatedPrescription.CustomerID;
            prescription.PrescriptionDate = updatedPrescription.PrescriptionDate;
            prescription.GlassesType = updatedPrescription.GlassesType;
            prescription.SphOD = updatedPrescription.SphOD;
            prescription.CylOD= updatedPrescription.CylOD;
            prescription.AxOD = updatedPrescription.AxOD;
            prescription.SphOS = updatedPrescription.SphOS;
            prescription.CylOS = updatedPrescription.CylOS;
            prescription.AxOS = updatedPrescription.AxOS;
            prescription.PD = updatedPrescription.PD;


            conText.Prescriptions.Update(prescription);
            conText.SaveChanges();

            conText.Dispose();

            return Ok(prescription);
        }

        // POST: api/Prescription

        [HttpPost]
        public IActionResult CreatePrescription(CreatePrescriptionModel prescriptionRequest)
        {
            Doctor doctor = conText.Doctors.FirstOrDefault(d => d.Id == prescriptionRequest.DoctorID);
            if (doctor == null)
            {
                return NotFound("Doctor not found");
            }

            Customer customer = conText.Customers.FirstOrDefault(c => c.Id == prescriptionRequest.CustomerID);
            if (customer == null)
            {
                return NotFound("Customer not found");
            }

            Prescription prescription = new Prescription()
            {
            DoctorID = prescriptionRequest.DoctorID,
            CustomerID = prescriptionRequest.CustomerID,
            PrescriptionDate = prescriptionRequest.PrescriptionDate,
            GlassesType = prescriptionRequest.GlassesType,
            SphOD = prescriptionRequest.SphOD,
            CylOD = prescriptionRequest.CylOD,
            AxOD = prescriptionRequest.AxOD,
            SphOS = prescriptionRequest.SphOS,
            CylOS = prescriptionRequest.CylOS,
            AxOS = prescriptionRequest.AxOS,
            PD = prescriptionRequest.PD,
            CreatedAt = DateTime.Now,

            };

            conText.Prescriptions.Add(prescription);
            conText.SaveChanges();

            return Created();
        }

        // DELETE: api/Prescriptions/5
        [HttpDelete("{id}")]
        public IActionResult DeletePrescription(int id)
        {
            var prescription = conText.Prescriptions.Find(id);
            if (prescription == null)
            {
                return NotFound();
            }

            conText.Prescriptions.Remove(prescription);
            conText.SaveChanges();

            return NoContent();
        }


    }
}

