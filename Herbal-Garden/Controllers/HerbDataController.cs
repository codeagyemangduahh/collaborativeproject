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
    public class HerbDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        //
        /// <summary>
        /// Returns all Herbs in the system.
        /// </summary>
        /// <returns>
        ///
        /// CONTENT: all Herbs in the database, including their associated Gardens.
        /// </returns>
        /// <example>
        /// GET: api/HerbData/Listherbs
        /// </example>
        [HttpGet]
        [Route("api/HerbData/ListHerbs")]
        [ResponseType(typeof(HerbsDto))]
        public IHttpActionResult ListHerbs()
        {
            List<Herbs> herbs = db.Herbss.ToList();
            List<HerbsDto> HerbsDtos = new List<HerbsDto>();

            herbs.ForEach(h => HerbsDtos.Add(new HerbsDto()
            {
                HerbsID = h.HerbsID,
                HerbsName = h.HerbsName,

                GardenID = h.Garden.GardenID,
                GardenName = h.Garden.GardenName,
            }));

            return Ok(HerbsDtos);
        }

        /// <summary>
        /// Returns particular Herb in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: A Herb in the system matching up to the Herb ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the Herb</param>
        /// <example>
        /// GET: api/VaporizerData/FindVaporizer/5
        /// </example>
        [ResponseType(typeof(VaporizerDto))]
        [HttpGet]
        [Route("api/HerbData/FindHerb/{id}")]
        public IHttpActionResult FindHerb(int id)
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
        /// Gathers information about all Herbs related to a particular Garden
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Vaporizers in the database, including their associated suppliers matched with a particular supplier ID
        /// </returns>
        /// <param name="id">Garden ID.</param>
        /// <example>
        /// GET: api/HerbsData/ListHerbsForGarden/3
        /// </example>
        [HttpGet]
        [Route("api/HerbsData/ListHerbsForGarden/{id}")]
        [ResponseType(typeof(HerbsDto))]
        public IHttpActionResult ListVaporizerForSupplier(int id)
        {
            
            List<Herbs> herbs = db.Herbss.Where(h => h.GardenID == id).ToList();
            List<HerbsDto> HerbsDtos = new List<HerbsDto>();

            herbs.ForEach(h => HerbsDtos.Add(new HerbsDto()
            {
                HerbsID = h.HerbsID,
                HerbsName = h.HerbsName,

                GardenID = h.Garden.GardenID,
                GardenName = h.Garden.GardenName,
            }));

            return Ok(HerbsDtos);
        }

        /// <summary>
        /// Returns all Keepers in the system associated with a particular animal.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Keepers in the database taking care of a particular animal
        /// </returns>
        /// <param name="id">Animal Primary Key</param>
        /// <example>
        /// GET: api/KeeperData/ListKeepersForAnimal/1
        /// </example>
        [HttpGet]
        [Route("api/HerbsData/ListHerbsForVaporizer/{id}")]
        [ResponseType(typeof(HerbsDto))]
        public IHttpActionResult ListHerbsForVaporizer(int id)
        {

            

            List<Herbs> Herbs = db.Herbss.Where(
                k => k.Vaporizers.Any(
                    a => a.VaporizerID == id)
                ).ToList();
            List<HerbsDto> HerbsDtos = new List<HerbsDto>();

            Herbs.ForEach(k => HerbsDtos.Add(new HerbsDto()
            {
                 HerbsID = k.HerbsID,
                HerbsName = k.HerbsName,
                Treatment = k.Treatment
            }));

            return Ok(HerbsDtos);
        }

        /// <summary>
        /// Returns Herbs in the system not compatible with a Vaporizer.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Herbs in the database not compatible with the vaporizers
        /// </returns>
        /// <param name="id">Vaporizer Primary Key</param>
        /// <example>
        /// GET: api/HerbsData/ListHerbsNotWorkWithVaporizer/{id}
        /// </example>
        [HttpGet]
        [Route("api/HerbsData/ListHerbsNotWorkWithVaporizer/{id}")]
        [ResponseType(typeof(HerbsDto))]
        public IHttpActionResult ListKeepersNotCaringForAnimal(int id)
        {
            List<Herbs> Herbss = db.Herbss.Where(
                k => !k.Vaporizers.Any(
                    a => a.VaporizerID == id)
                ).ToList();
            List<HerbsDto> HerbsDtos = new List<HerbsDto>();

            Herbss.ForEach(k => HerbsDtos.Add(new HerbsDto()
            {
                HerbsID = k.HerbsID,
                HerbsName = k.HerbsName,
                Treatment = k.Treatment
            }));

            return Ok(HerbsDtos);
        }


        // PUT: api/Herbdata/5
        /* [ResponseType(typeof(void))]
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

        // POST: api/Herbdata
        /* [ResponseType(typeof(Herbs))]
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
        /// Gathers information about Herb related to a particular Vaporizers
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: all Vaporizers in the database, including their associated Supplier that match to a particular Herbs id
        /// </returns>
        /// <param name="id"> ID.</param>
        /// <example>
        /// GET: api/AnimalData/ListHerbsForVaporizers/1
        /// </example>
        [HttpGet]
        [Route("api/HerbData/ListHerbsForVaporizers/{id}")]

        [ResponseType(typeof(HerbsDto))]
        public IHttpActionResult ListHerbsForVaporizers(int id)
        {


            //all Herbs that have Vaporizers which match with our ID
            List<Herbs> herbs = db.Herbss.Where(
                h => h.Vaporizers.Any(
                    v => v.VaporizerID == id
                )).ToList();
            List<HerbsDto> HerbsDtos = new List<HerbsDto>();

            herbs.ForEach(h => HerbsDtos.Add(new HerbsDto()
            {
                HerbsID = h.HerbsID,
                HerbsName = h.HerbsName,

                GardenID = h.Garden.GardenID,
                GardenName =h.Garden.GardenName,
            }));

            return Ok(HerbsDtos);
        }

        /// <summary>
        /// Updates a particular Herbs in the system with POST Data input
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
        /// POST: api/HerbsData/UpdateHerbs/5
        /// FORM DATA: Herbs JSON Object
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        [Route("api/HerbsData/UpdateHerbs/{id}")]
        public IHttpActionResult UpdateVaporizer(int id, Herbs Herbs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Herbs.HerbsID)
            {

                return BadRequest();
            }

            db.Entry(Herbs).State = EntityState.Modified;

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

        /// <summary>
        /// Adds an Herbs to the system
        /// </summary>
        /// 
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Vaporizer ID, Vaporizer Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/HerbsData/AddHerbs
        /// FORM DATA: Herbs JSON Object
        /// </example>
        [ResponseType(typeof(Herbs))]
        [HttpPost]
        [Route("api/HerbsData/AddHerbs")]
        public IHttpActionResult AddHerbs(Herbs herbs)
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
        /// Deletes a Herbs from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the Herbs</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/HerbData/DeleteHerbs/5
        /// FORM DATA: (empty)
        /// </example>
        [ResponseType(typeof(Herbs))]
        [HttpPost]
        [Route("api/HerbsData/DeleteHerbs/{id}")]
        public IHttpActionResult DeleteHerbs(int id)
        {
            Herbs herbs = db.Herbss.Find(id);
            if (herbs == null)
            {
                return NotFound();
            }

            db.Herbss.Remove(herbs);
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
    }
}