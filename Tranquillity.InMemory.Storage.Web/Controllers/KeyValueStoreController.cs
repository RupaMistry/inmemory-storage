using Tranquillity.InMemory.Storage.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Tranquillity.InMemory.Storage.Web.Controllers
{
    [AllowAnonymous]
    public class KeyValueStoreController : Controller
    {
        // GET: KeyValueStore
        [HttpGet]
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}