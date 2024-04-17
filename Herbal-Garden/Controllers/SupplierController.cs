using Herbal_Garden.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Herbal_Garden.Models.ViewModels;

  namespace Herbal_Garden.Controllers
{
    public class SupplierController : Controller
    {
        private static readonly HttpClient client;
        private JavaScriptSerializer jss = new JavaScriptSerializer();

        static SupplierController()
        {
            client = new HttpClient();
            client.BaseAddress = new Uri("https://localhost:44323/api/");
        }


        // GET: Supplier
        public ActionResult List()
        {
           


            string url = "SupplierData/ListSuppliers";
            HttpResponseMessage response = client.GetAsync(url).Result;

            

            IEnumerable<SupplierDto> suppliers = response.Content.ReadAsAsync<IEnumerable<SupplierDto>>().Result;
            


            return View(suppliers);
        }

        // GET: Supplier/Details/5
        public ActionResult Details(int id)
        {
            

            DetailsSupplier ViewModel = new DetailsSupplier();

            string url = "SupplierData/FindSupplier/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;

          

            SupplierDto SelectedSupplier = response.Content.ReadAsAsync<SupplierDto>().Result;
            

            ViewModel.SelectedSupplier = SelectedSupplier;

            //showcase information about Vaporizer related to this supplier
            //send a request to gather information about Vaporizer related to a particular Supplier ID
            url = "Supplierdata/listVaporizerforSupplier/" + id;
            response = client.GetAsync(url).Result;
            IEnumerable<VaporizerDto> RelatedVaporizer = response.Content.ReadAsAsync<IEnumerable<VaporizerDto>>().Result;

            ViewModel.RelatedVaporizer = RelatedVaporizer;


            return View(ViewModel);
        }


        public ActionResult Error()
        {

            return View();
        }

        // GET: Supplier/New
        public ActionResult New()
        {
            return View();
        }

        // POST: Supplier/Create
        [HttpPost]
        public ActionResult Create(Supplier supplier)
        {
            
            string url = "SupplierData/addSupplier";


            string jsonpayload = jss.Serialize(supplier);
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

        // GET: Supplier/Edit/5
        public ActionResult Edit(int id)
        {
            string url = "Supplierdata/findSupplier/" + id;
            HttpResponseMessage response = client.GetAsync(url).Result;
            SupplierDto selectedSupplier = response.Content.ReadAsAsync<SupplierDto>().Result;
            return View(selectedSupplier);
        }

        // POST: Supplier/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Supplier supplier)
        {
            string url = "Supplierdata/updateSupplier/" + id;
            string jsonpayload = jss.Serialize(supplier);
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

        // GET: Supplier/Delete/5
        public ActionResult Delete(int id)
        {
            
                string url = "SupplierData/findSupplier/" + id;
                HttpResponseMessage response = client.GetAsync(url).Result;
                SupplierDto selectedsupplier = response.Content.ReadAsAsync<SupplierDto>().Result;
                return View(selectedsupplier);
            
        }

        // POST: Supplier/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            string url = "Supplierdata/deleteSupplier/" + id;
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
