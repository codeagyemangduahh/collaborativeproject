using Herbal_Garden.Models;
using Herbal_Garden.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Herbal_Garden.Controllers
{
    public class GardenController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();
        static GardenController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44323/api/");
        }
        // GET: Garden
        public ActionResult List()
        {
            string url = "GardenData/ListGardens";
            HttpResponseMessage response = client.GetAsync(url).Result;



            IEnumerable<GardenDto> gardens = response.Content.ReadAsAsync<IEnumerable<GardenDto>>().Result;



            return View(gardens);
        }

        // GET: Garden/Details/5
        public ActionResult Details(int id)
        {
            DetailsGarden ViewModel = new DetailsGarden();

            string url = "GardenData/FindGarden/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;



            GardenDto Selectedgarden = response.Content.ReadAsAsync<GardenDto>().Result;


            ViewModel.SelectedGarden = Selectedgarden;

            //info about Herbs related 
            url = "Gardendata/listHerbsforGarden/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<HerbsDto> Relatedherbs = response.Content.ReadAsAsync<IEnumerable<HerbsDto>>().Result;

            ViewModel.RelatedHerb = Relatedherbs;


            return View(ViewModel);
        }
        public ActionResult Error()
        {

            return View();
        }

        // GET: Garden/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Garden/Create
        [HttpPost]
        public ActionResult Create(Garden garden)
        {


            string url = "GardenData/addGarden";


            string jsonpayload = jss.Serialize(garden);
            Debug.WriteLine(jsonpayload);

            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";

            HttpResponseMessage response = client.PostAsync(url, content).Result;
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }


        }

        // GET: Garden/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "GardenData/findGarden/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            GardenDto selectedgarden = response.Content.ReadAsAsync<GardenDto>().Result;
            return View(selectedgarden);
        }

        // POST: Garden/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Garden garden)
        {

            string url = "Gardendata/updateGarden/" + id;
            string jsonpayload = jss.Serialize(garden);
            HttpContent content = new StringContent(jsonpayload);
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;
            Debug.WriteLine(content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }




        }

        // GET: Garden/Delete/5
        public ActionResult Delete(int id)
        {
            string url = "GardenData/findGarden/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            GardenDto selectedgarden = response.Content.ReadAsAsync<GardenDto>().Result;
            return View(selectedgarden);
        }

        // POST: Garden/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            string url = "Gardendata/deleteGarden/" + id;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("List");
            }
            else
            {
                return RedirectToAction("Error");
            }
        }
    }

}
