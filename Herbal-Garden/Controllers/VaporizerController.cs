using Herbal_Garden.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Herbal_Garden.Models.ViewModels;
using Herbal_Garden.Migrations;


namespace Herbal_Garden.Controllers
{
    public class VaporizerController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static VaporizerController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44323/api/");
        }



        // GET: Vaporizer/List
        public ActionResult List()
        {
            string url = "VaporizerData/ListVaporizer";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<VaporizerDto> vaporizers = response.Content.ReadAsAsync<IEnumerable<VaporizerDto>>().Result;    
            return View(vaporizers);
        }

        // GET: Vaporizer/Details/5
        public ActionResult Details(int id)
        {
            DetailsVaporizer ViewModel = new DetailsVaporizer();


           

            string url = "VaporizerData/FindVaporizer/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

           

            VaporizerDto SelectedVaporizer = response.Content.ReadAsAsync<VaporizerDto>().Result;
            

            ViewModel.SelectedVaporizer = SelectedVaporizer;

            //show associated Herbs with this Vaporizer
            url = "Herbsdata/listHerbsforVaporizer/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<HerbsDto> CompatibleHerbs = response.Content.ReadAsAsync<IEnumerable<HerbsDto>>().Result;

            ViewModel.CompatibleHerbs = CompatibleHerbs;

            url = "Herbsdata/listHerbsnotWorkingWithVaporizer/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<HerbsDto> AvailableHerbs = response.Content.ReadAsAsync<IEnumerable<HerbsDto>>().Result;

            ViewModel.AvailableHerbs = AvailableHerbs;


            return View(ViewModel);
        }

        //POST: Vaporizer/Associate/{VaporizerId}/{HerbsID}
        [HttpPost]
        public ActionResult Associate(int id, int HerbsID)
        {
            Debug.WriteLine("Attempting to associate Vaaporizer :" + id + " with Herb " + HerbsID);

            //call our api to associate Vaporizer with Herbs
            string url = "animaldata/AssociateVaporizerWithHerbs/" + id + "/" + HerbsID;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("Details/" + id);
        }

        //Get: Vaporizer/UnAssociate/{Vaporizerid}/{HerbsID}
        [HttpGet]
        public ActionResult UnAssociate(int id, int HerbsID)
        {
            

            //call our api to associate Vaporizer with herbs
            string url = "VaporizerData/UnAssociateVaporizerWithHerbs/" + id + "/" + HerbsID;
            HttpContent content = new StringContent("");
            content.Headers.ContentType.MediaType = "application/json";
            HttpResponseMessage response = client.PostAsync(url, content).Result;

            return RedirectToAction("Details/" + id);
        }

        public ActionResult Error()
        {

            return View();
        }


        // GET: Vaporizer/New
        public ActionResult New()
        {
            //information about all Suppliers in the system.
            //GET api/supplierdata/listsuppliers

            string url = "supplierData/listSuppliers";
            HttpResponseMessage response = client.GetAsync(url).Result;
            IEnumerable<SupplierDto> SupplierOptions = response.Content.ReadAsAsync<IEnumerable<SupplierDto>>().Result;

            return View(SupplierOptions);
        }


        // GET: Vaporizer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Vaporizer/Create
        [HttpPost]
        public ActionResult Create(Vaporizer vaporizer)
        {
           
            
            string url = "VaporizerData/addVaporizer";


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

        // GET: Vaporizer/Edit/5
        public ActionResult Edit(int id)
        {
            UpdateVaporizer ViewModel = new UpdateVaporizer();

            //the existing Vaporzier information
            string url = "VaporizerData/findVaporizer/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            VaporizerDto SelectedVaporizer = response.Content.ReadAsAsync<VaporizerDto>().Result;
            ViewModel.SelectedVaporizer = SelectedVaporizer;

            // all Suppliers to choose from when updating this Vaporizers
            //the existing animal information
            url = "SupplierData/listSuppliers/";
            response = client.GetAsync(url).Result;
            IEnumerable<SupplierDto> SupplierOptions = response.Content.ReadAsAsync<IEnumerable<SupplierDto>>().Result;

            ViewModel.SupplierOptions = SupplierOptions;

            return View(ViewModel);
        }

        // POST: Vaporizer/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Vaporizer vaporizer)
        {
            string url = "VaporizerData/UpdateVaporizer/" + id;
            string jsonpayload = jss.Serialize(vaporizer);
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

        // GET: Vaporizer/Delete/5
        public ActionResult DeleteConfirm(int id)
        {
            string url = "Vaporzierdata/findVaporizer/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            VaporizerDto selectedVaporizer = response.Content.ReadAsAsync<VaporizerDto>().Result;
            string jsonpayload = jss.Serialize(selectedVaporizer);
            return View(selectedVaporizer);
        }

        // POST: Vaporizer/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            string url = "VaporizerData/deleteVaporizer/" + id;
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
