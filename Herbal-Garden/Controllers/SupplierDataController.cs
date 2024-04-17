using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Herbal_Garden.Models;

namespace Herbal_Garden.Controllers
{
    public class SupplierDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/SupplierData
        //
        /// <summary>
        /// Returns all Suppliers in the system.
        /// </summary>
        /// <returns>
        ///
        /// CONTENT: all Suppliers in the database
        /// </returns>
        /// <example>
        /// GET: api/SupplierData/ListSuppliers
        /// </example>
        [HttpGet]
        [Route("api/SupplierData/ListSuppliers")]
        [ResponseType(typeof(SupplierDto))]
        public IHttpActionResult ListHerbs()
        {
            List<Supplier> suppliers = db.Suppliers.ToList();
            List<SupplierDto> SupplierDtos = new List<SupplierDto>();

            suppliers.ForEach(s => SupplierDtos.Add(new SupplierDto()
            {
                SupplierID = s.SupplierID,
                SupplierName= s.SupplierName,

                
            }));

            return Ok(SupplierDtos);
        }

        /*// GET: api/SupplierData/5
        [ResponseType(typeof(Herbs))]
        public IHttpActionResult GetHerbs(int id)
        {
            Herbs herbs = db.Herbss.Find(id);
            if (herbs == null)
            {
                return NotFound();
            }

            return Ok(herbs);
        }*/


        /// <summary>
        /// Returns particular Supplier in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        
        
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the Herb</param>
        /// <example>
        /// GET: api/SupplierData/FindSupplier/5
        /// </example>
        [ResponseType(typeof(SupplierDto))]
        [HttpGet]
        [Route("api/SupplierData/FindSupplier/{id}")]
        public IHttpActionResult FindSupplier(int id)
        {
            Herbs Herbs = db.Herbss.Find(id);
            HerbsDto HerbsDto = new HerbsDto()
            {
                HerbsID = Herbs.HerbsID,
                HerbsName = Herbs.HerbsName,

                GardenID = Herbs.Garden.GardenID,
                GardenName = Herbs.Garden.GardenName,
            };
            if (Herbs == null)
            {
                return NotFound();
            }

            return Ok(HerbsDto);
        }

        /// <summary>
        /// Updates a particular Supplier in the system with POST Data input
        /// </summary>
        /// 
        /// <returns>
        /// HEADER: 204 (Success, No Content Response)
        /// or
        /// HEADER: 400 (Bad Request)
        /// or
        /// HEADER: 404 (Not Found)
        /// </returns>
        /// <example>
        /// POST: api/SupplierData/UpdateSupplier/5
        /// FORM DATA: Supplier JSON Object
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        [Route("api/SupplierData/UpdateSupplier/{id}")]
        public IHttpActionResult UpdateSupplier(int id, Supplier Suppliers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Suppliers.SupplierID)
            {

                return BadRequest();
            }

            db.Entry(Suppliers).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

        /// <summary>
        /// Adds an Supplier to the system
        /// </summary>
        /// 
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Supplier ID, Supplier Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/SupplierData/AddSuppliers
        /// FORM DATA: Herbs JSON Object
        /// </example>
        [ResponseType(typeof(Supplier))]
        [HttpPost]
        [Route("api/SupplierData/AddSupplier")]
        public IHttpActionResult AddHerbs(Supplier suppliers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Suppliers.Add(suppliers);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = suppliers.SupplierID }, suppliers);
        }




/*
        // PUT: api/SupplierData/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutHerbs(int id, Herbs herbs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != herbs.HerbsID)
            {
                return BadRequest();
            }

            db.Entry(herbs).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupplierExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }


        // POST: api/SupplierData
        [ResponseType(typeof(Herbs))]
        public IHttpActionResult PostHerbs(Herbs herbs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Herbss.Add(herbs);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = herbs.HerbsID }, herbs);
        }*/

        /// <summary>
        /// Deletes a Supplier from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the Supplier</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/SupplierData/DeleteSupplier/5
        /// FORM DATA: (empty)
        /// </example>
        [ResponseType(typeof(Supplier))]
        [HttpPost]
        [Route("api/SupplierData/DeleteSupplier/{id}")]
        public IHttpActionResult DeleteSupplier(int id)
        {
            Supplier suppliers = db.Suppliers.Find(id);
            if (suppliers == null)
            {
                return NotFound();
            }

            db.Suppliers.Remove(suppliers);
            db.SaveChanges();

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool SupplierExists(int id)
        {
            return db.Suppliers.Count(e => e.SupplierID == id) > 0;
        }
    }
}