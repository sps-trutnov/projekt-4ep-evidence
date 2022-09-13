﻿using EvidenceProject.Controllers.RequestClasses;
using Microsoft.AspNetCore.Mvc;

namespace EvidenceProject.Controllers
{
    public class ProjectController : Controller
    {
        [HttpGet("project")]
        public ActionResult Index()
        {
            return Redirect("project/create");
        }

        /// <summary>
        /// Vytvoření/přidání projektu
        /// </summary>        
        [HttpPost("project/create")]
        public ActionResult Create([FromForm] ProjectCreateRequest data)
        {
            return Json("ok");
        }

        [HttpPost("project/{id}")]
        public ActionResult Delete(int id)
        {
            return Json("ok");
        }

        [HttpGet("project/create")]
        public ActionResult Create()
        {
            return View();
        }

        /// <param name="id">Id projektu</param>
        [HttpGet("projectinfo/{id}")]
        public ActionResult ProjectInfo(int id)
        {
            return View();
        }
    }
}
