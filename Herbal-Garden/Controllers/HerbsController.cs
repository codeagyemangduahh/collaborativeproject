using Herbal_Garden.Migrations;
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
    public class HerbsController : Controller
    {

        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static HerbsController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44323/api/");
        }
        // GET: Herbs/List
        public ActionResult List()
        {
            string url = "HerbsData/ListHerbs";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<HerbsDto> herbs = response.Content.ReadAsAsync<IEnumerable<HerbsDto>>().Result;
            return View(herbs);
        }

        // GET: Herbs/Details/5
        public ActionResult Details(int id)
        {
            DetailsHerbs ViewModel = new DetailsHerbs();




            string url = "HerbsData/FindHerbs/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;



            HerbsDto Selectedherbs = response.Content.ReadAsAsync<HerbsDto>().Result;


            ViewModel.SelectedHerbs = Selectedherbs;

            //show associated Vaporizer with this Herbs
            url = "Vaporizerdata/listVaporizerforHerbs/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<VaporizerDto> Compatiblevapes = response.Content.ReadAsAsync<IEnumerable<VaporizerDto>>().Result;

            ViewModel.ComaptibleVaporizer = Compatiblevapes;

            url = "Vaporizerdata/listVaporizernotWorkingWithHerbs/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<VaporizerDto> Availablevapes = response.Content.ReadAsAsync<IEnumerable<VaporizerDto>>().Result;

            ViewModel.AvailableVaporizer = Availablevapes;


            return View(ViewModel);
        }

        public ActionResult Error()
        {

            return View();
        }



        // GET: Herbs/New
        public ActionResult New()
        {
            //information about all Suppliers in the system.
            //GET api/supplierdata/listsuppliers

            string url = "HerbsData/listHerbs";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<HerbsDto> VaporizerOptions = response.Content.ReadAsAsync<IEnumerable<HerbsDto>>().Result;

            return View(VaporizerOptions);
        }

        // POST: Herbs/Create
        [HttpPost]
        public ActionResult Create(Vaporizer vaporizer)
        {
            string url = "HerbsData/addHerb";


            string jsonpayload = jss.Serialize(vaporizer);
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

        // GET: Herbs/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateHerbs ViewModel = new UpdateHerbs();

            //the existing Herbs information
            string url = "HerbsData/findHerbs/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            HerbsDto Selectedherbs = response.Content.ReadAsAsync<HerbsDto>().Result;
            ViewModel.SelectedHerb = Selectedherbs;

            // all Gardens to choose from when updating this Herbs
            //the existing animal information
            url = "GardenData/listGarden/";
            response = client.GetAsync(url).Result;
            IEnumerable<GardenDto> SupplierOptions = response.Content.ReadAsAsync<IEnumerable<GardenDto>>().Result;

            ViewModel.GardenOptions = SupplierOptions;

            return View(ViewModel);
        }

        // POST: Herbs/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Herbs herbs)
        {

            string url = "HerbsData/UpdateHerb/" + id;
            string jsonpayload = jss.Serialize(herbs);
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

        // GET: Herbs/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "Herbsdata/findHerbs/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            VaporizerDto selectedherbs = response.Content.ReadAsAsync<VaporizerDto>().Result;
            string jsonpayload = jss.Serialize(selectedherbs);
            return View(selectedherbs);
        }

        // POST: Herbs/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            string url = "HerbsData/deleteHerbs/" + id;
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
