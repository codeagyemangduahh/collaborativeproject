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
    public class GardenDataController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        //
        /// <summary>
        /// Returns all Gardens in the system.
        /// </summary>
        /// <returns>
        ///
        /// CONTENT: all Gardens in the database.
        /// </returns>
        /// <example>
        /// GET: api/GardenData/ListGarden
        /// </example>
        [HttpGet]
        [Route("api/GardenData/ListGardens")]
        [ResponseType(typeof(GardenDto))]
        public IHttpActionResult ListGarden()
        {
            List<Garden> gardens = db.Gardens.ToList();
            List<GardenDto> GardenDtos = new List<GardenDto>();

            gardens.ForEach(g => GardenDtos.Add(new GardenDto()
            {
                GardenID = g.GardenID,
                GardenName = g.GardenName,

                SoilType = g.SoilType,
                
            }));

            return Ok(GardenDtos);
        }

        /// <summary>
        /// Returns particular Garden in the system.
        /// </summary>
        /// <returns>
        /// HEADER: 200 (OK)
        /// CONTENT: A Garden in the system matching up to the Garden ID primary key
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <param name="id">The primary key of the Garden</param>
        /// <example>
        /// GET: api/GardenData/FindGarden/5
        /// </example>
        [ResponseType(typeof(GardenDto))]
        [HttpGet]
        [Route("api/GardenData/FindGarden/{id}")]
        public IHttpActionResult FindGarden(int id)
        {
            Garden gardens = db.Gardens.Find(id);
            GardenDto GardenDto = new GardenDto()
            {
                GardenID = gardens.GardenID,
                GardenName = gardens.GardenName,

                SoilType = gardens.SoilType,
               
            };
            if (gardens == null)
            {
                return NotFound();
            }

            return Ok(GardenDto);
        }
        /*
        // PUT: api/GardenData/5
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

        // POST: api/GardenData
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
        } */

        /// <summary>
        /// Updates a particular Garden in the system with POST Data input
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
        /// POST: api/GardenData/UpdateGarden/5
        /// FORM DATA: garden JSON Object
        /// </example>
        [ResponseType(typeof(void))]
        [HttpPost]
        [Route("api/GardenData/UpdateGarden/{id}")]
        public IHttpActionResult UpdateGarden(int id, Garden Gardens)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Gardens.GardenID)
            {

                return BadRequest();
            }

            db.Entry(Gardens).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GardenExists(id))
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
        /// Adds a Garden to the system
        /// </summary>
        /// 
        /// <returns>
        /// HEADER: 201 (Created)
        /// CONTENT: Supplier ID, Supplier Data
        /// or
        /// HEADER: 400 (Bad Request)
        /// </returns>
        /// <example>
        /// POST: api/GardenData/AddGarden
        /// FORM DATA: Garden JSON Object
        /// </example>
        [ResponseType(typeof(Supplier))]
        [HttpPost]
        [Route("api/GardenData/AddGarden")]
        public IHttpActionResult AddHerbs(Garden Gardens)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Gardens.Add(Gardens);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = Gardens.GardenID }, Gardens);
        }


        /// <summary>
        /// Deletes a Garden from the system by it's ID.
        /// </summary>
        /// <param name="id">The primary key of the Garden</param>
        /// <returns>
        /// HEADER: 200 (OK)
        /// or
        /// HEADER: 404 (NOT FOUND)
        /// </returns>
        /// <example>
        /// POST: api/GardenData/DeleteGarden/5
        /// FORM DATA: (empty)
        /// </example>
        [ResponseType(typeof(Supplier))]
        [HttpPost]
        [Route("api/GardenData/DeleteGarden/{id}")]
        public IHttpActionResult DeleteGarden(int id)
        {
            Garden gardens = db.Gardens.Find(id);
            if (gardens == null)
            {
                return NotFound();
            }

            db.Gardens.Remove(gardens);
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

        private bool GardenExists(int id)
        {
            return db.Gardens.Count(e => e.GardenID == id) > 0;
        }
    }
}