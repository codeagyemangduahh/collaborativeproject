using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Herbal_Garden.Models;

namespace Herbal_Garden.Controllers
{
    public class VaporizerDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/VaporizerData
        /// <summary>
        /// Returns all Vaporizer in the system.
        /// </summary>
        /// <returns>
        ///
        /// CONTENT: all Vaporizer in the database, including their associated herbs.
        /// </returns>
        /// <example>
        /// GET: api/VaporizerData/ListVaporizer
        /// </example>
        [HttpGet]
        [Route("api/VaporizerData/ListVaporizer")]
        [ResponseType(typeof(VaporizerDto))]
        public IHttpActionResult ListVaporizers()
        {
            List<Vaporizer> vaporizers = db.Vaporizers.ToList();
            List<VaporizerDto> VaporizerDtos = new List<VaporizerDto>();

            vaporizers.ForEach(v => VaporizerDtos.Add(new VaporizerDto()
            {
                VaporizerID = v.VaporizerID,
                VaporizerName = v.VaporizerName,

                SupplierID = v.Supplier.SupplierID,
                SupplierName = v.Supplier.SupplierName,
            }));

            return Ok(VaporizerDtos);
        }

        /// <summary>
        /// Returns particular Vaporizer in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: A Vaporizer in the system matching up to the Vaporizer ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the Vaporizer</param>
        /// <example>
        /// GET: api/VaporizerData/FindVaporizer/5
        /// </example>
        [ResponseType(typeof(VaporizerDto))]
        [HttpGet]
        [Route("api/VaporizerData/FindVaporizer/{id}")]
        public IHttpActionResult FindVaporizer(int id)
        {
            Vaporizer Vaporizer = db.Vaporizers.Find(id);
            VaporizerDto VaporizerDto = new VaporizerDto()
            {
                VaporizerID = Vaporizer.VaporizerID,
                VaporizerName = Vaporizer.VaporizerName,
                 
                SupplierID = Vaporizer.Supplier.SupplierID,
                SupplierName = Vaporizer.Supplier.SupplierName
            };
            if (Vaporizer == null)
            {
                return NotFound();
            }

            return Ok(VaporizerDto);
        }




        // GET: api/VaporizerData/5
        /// <summary>
        /// Gathers information about all Vaporizers related to a particular Supplier
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Vaporizers in the database, including their associated suppliers matched with a particular supplier ID
        /// </returns>
        /// <param name="id">Supplier ID.</param>
        /// <example>
        /// GET: api/VaporizerData/ListVaporizerForSupplier/3
        /// </example>
        [HttpGet]
        [Route("api/VaporizerData/ListVaporizerForSupplier/{id}")]
        [ResponseType(typeof(VaporizerDto))]
        public IHttpActionResult ListVaporizerForSupplier(int id)
        {
            //SQL Equivalent:
            //Select * from Vaporizer where Vaporizer.SupplierID = {id}
            List<Vaporizer> vaporizers = db.Vaporizers.Where(v => v.SupplierID == id).ToList();
            List<VaporizerDto> VaporizerDtos = new List<VaporizerDto>();

            vaporizers.ForEach(v => VaporizerDtos.Add(new VaporizerDto()
            {
                VaporizerID = v.VaporizerID,
                VaporizerName = v.VaporizerName,
               
                SupplierID = v.Supplier.SupplierID,
                SupplierName = v.Supplier.SupplierName,
            }));

            return Ok(VaporizerDtos);
        }

        // PUT: api/VaporizerData/5
        /*
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
                if (!HerbsExists(id))
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
        */

        /// <summary>
        /// Gathers information about vaporizers related to a particular Herbs
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Vaporizers in the database, including their associated Supplier that match to a particular Herbs id
        /// </returns>
        /// <param name="id">Keeper ID.</param>
        /// <example>
        /// GET: api/AnimalData/ListvaporizersForHerbs/1
        /// </example>
        [HttpGet]
        [Route("api/AnimalData/ListvaporizersForHerbs/{id}")]

        [ResponseType(typeof(VaporizerDto))]
        public IHttpActionResult ListVaporizersForHerbs(int id)
        {
            

            //all animals that have keepers which match with our ID
            List<Vaporizer> vaporizers = db.Vaporizers.Where(
                v => v.Herbss.Any(
                    h => h.HerbsID == id
                )).ToList();
            List<VaporizerDto> VaporizerDtos = new List<VaporizerDto>();

            vaporizers.ForEach(v => VaporizerDtos.Add(new VaporizerDto()
            {
                VaporizerID = v.VaporizerID,
                VaporizerName = v.VaporizerName,
                
                SupplierID = v.Supplier.SupplierID,
                SupplierName = v.Supplier.SupplierName,
            }));

            return Ok(VaporizerDtos);
        }


        // POST: api/VaporizerData
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
        }

        /// <summary>
        /// Associates a particular Herbs with a particular Vaporizer
        /// </summary>
        
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST api/AnimalData/AssociateVaporizerWithHerbs/9/1
        /// </example>
        [HttpPost]
        [Route("api/VaporizerData/AssociateVaporizerWithHerbs/{vaporizerid}/{Herbsid}")]
        public IHttpActionResult AssociateVaporizerWithHerbs(int VaporizerID, int HerbsID)
        {

            Vaporizer SelectedVaporizer = db.Vaporizers.Include(v => v.Herbss).Where(v => v.VaporizerID == VaporizerID).FirstOrDefault();
            Herbs SelectedHerbs = db.Herbss.Find(HerbsID);

            if (SelectedVaporizer == null || SelectedHerbs == null)
            {
                return NotFound();
            }


            SelectedVaporizer.Herbss.Add(SelectedHerbs);
            db.SaveChanges();

            return Ok("working fine");
        }
        /// <summary>
        /// Removes an association between a particular Vaporizer and a particular Herb
        /// </summary>
        
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST api/VaporizerData/UnAssociateVaporizerWithherb/9/1
        /// </example>
        [HttpPost]
        [Route("api/VaporizerData/UnAssociateVaporizerWithHerbs/{vaporizerid}/{herbid}")]
        public IHttpActionResult UnAssociateAnimalWithKeeper(int VaporizerID, int HerbsID)
        {

            Vaporizer SelectedVaporizer = db.Vaporizers.Include(v => v.Herbss).Where(v => v.VaporizerID == VaporizerID).FirstOrDefault();
            Herbs SelectedHerbs = db.Herbss.Find(HerbsID);

            if (SelectedVaporizer == null || SelectedHerbs == null)
            {
                return NotFound();
            }

            

            //todo: verify that the Herbs actually is associated with the Vaporizer

            SelectedVaporizer.Herbss.Remove(SelectedHerbs);
            db.SaveChanges();

            return Ok("removed");
        }

        /// <summary>
        /// Updates a particular Vaporizer in the system with POST Data input
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
        /// POST: api/VaporizerData/UpdateVaporizer/5
        /// FORM DATA: Animal JSON Object
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        [Route("api/VaporizerData/UpdateVaporizer/{id}")]
        public IHttpActionResult UpdateVaporizer(int id, Vaporizer Vaporizer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Vaporizer.VaporizerID)
            {

                return BadRequest();
            }

            db.Entry(Vaporizer).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VaporizerExists(id))
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
        /// Adds an Vaporizer to the system
        /// </summary>
        /// 
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Vaporizer ID, Vaporizer Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/VaporizerData/AddVaporizer
        /// FORM DATA: Animal JSON Object
        /// </example>
        [ResponseType(typeof(Vaporizer))]
        [HttpPost]
        [Route("api/VaporizerData/AddVaporizer")]
        public IHttpActionResult AddVaporizer(Vaporizer vaporizer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Vaporizers.Add(vaporizer);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = vaporizer.VaporizerID }, vaporizer);
        }

        /// <summary>
        /// Deletes a vaporizer from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the Vaporizer</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/VaporizerData/DeleteVaporizer/5
        /// FORM DATA: (empty)
        /// </example>
        [ResponseType(typeof(Vaporizer))]
        [HttpPost]
        [Route("api/VaporizerData/DeleteVaporizer/{id}")]
        public IHttpActionResult DeleteVaporizer(int id)
        {
            Vaporizer vaporizer = db.Vaporizers.Find(id);
            if (vaporizer == null)
            {
                return NotFound();
            }

            db.Vaporizers.Remove(vaporizer);
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

        private bool HerbsExists(int id)
        {
            return db.Herbss.Count(e => e.HerbsID == id) > 0;
        }
        private bool VaporizerExists(int id)
        {
            return db.Vaporizers.Count(v => v.VaporizerID == id) > 0;
        }
    }
}