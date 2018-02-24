using System;
using System.Collections.Generic;
using System.Linq;
using Exam.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Exam.Controllers
{
    public class ActivityController : Controller
    {
        private ExamContext _context;
        public ActivityController(ExamContext context)
        {
            _context = context;
        }
        [HttpGet]
        [Route("dashboard")]
        public IActionResult dashboard()
        {
            int? id = HttpContext.Session.GetInt32("UserId");
            User useruser = _context.UserTable.SingleOrDefault(i => i.UserId==id);
            ViewBag.UserName =(string)HttpContext.Session.GetString("UserName");
            var y = _context.ActivityTable.Include(u =>u.Users).ThenInclude(a =>a.User).ToList();
            User oneuser = _context.UserTable.SingleOrDefault(i => i.UserId==id);
            ViewBag.coor=oneuser;
            ViewBag.session = HttpContext.Session.GetInt32("UserId");
            ViewBag.flag =false;
            return View("dashboard",y);
        }
        [HttpGet]
        [Route("activitypage")]
        public IActionResult activitypage()
        {
            ViewBag.errors = new List <string>();
            return View("NewActivity");
        }
        [HttpPost]
        [Route("addactivity")]
        public IActionResult AddActivityToDB(Activity model)
        {
            int? id = HttpContext.Session.GetInt32("UserId");
            User newuser = _context.UserTable.SingleOrDefault(i => i.UserId==id);
            if(ModelState.IsValid)
            {
                int uId = Convert.ToInt32 (id);
                Activity newactivity = new Activity()
                {
                    Title = model.Title,
                    Description = model.Description,
                    Date= model.Date,
                    Time =model.Time,
                    Duration=model.Duration,
                    ActivityLength=model.ActivityLength,
                    UserId = newuser.UserId
                };
                _context.ActivityTable.Add(newactivity);
                _context.SaveChanges();
                return RedirectToAction ("dashboard");
            }
            else {
                return View ("NewActivity");
            }
        }
        [HttpGet]
        [Route("delete/{aId}")]
        public IActionResult DeleteAct(int aId)
        {
            Activity activity=_context.ActivityTable.SingleOrDefault(w=>w.ActivityId==aId);
            _context.ActivityTable.Remove(activity);
            List<ActivityUser> actuser=_context.ActivityUser.Where(w=>w.ActivityId==aId).ToList();
            foreach(var act in actuser){
                _context.ActivityUser.Remove(act);
            }
            _context.SaveChanges();
            return RedirectToAction("dashboard");

        }
        [HttpGet]
        [Route("Join/{aId}")]
        public IActionResult JoinActivity(int aId){
            int? id=HttpContext.Session.GetInt32("UserId");
            int uid=Convert.ToInt32(id);
            ActivityUser join=new ActivityUser(){
                UserId=uid,
                ActivityId=aId
            };
            _context.ActivityUser.Add(join);
            _context.SaveChanges();
            return RedirectToAction("dashboard");
        }
        [HttpGet]
        [Route("Leave/{aId}")]
        public IActionResult LeaveActivity(int aId){
            int? id=HttpContext.Session.GetInt32("UserId");
            int uid=Convert.ToInt32(id);
            ActivityUser mjoins=_context.ActivityUser.SingleOrDefault(w=>w.ActivityId==aId && w.UserId==uid);
            _context.ActivityUser.Remove(mjoins);
            _context.SaveChanges();
            return RedirectToAction("dashboard");
        }
        [HttpGet]
        [Route("thisactivity/{actId}")]
        public IActionResult ThisActivity(int actId){
            List<Activity> Activities = _context.ActivityTable.Include (p => p.Users)
                .ThenInclude (s => s.User)
                .ToList ();
            Activity activity=Activities.SingleOrDefault(w=>w.ActivityId==actId);
            ViewBag.activity=activity;
            return View("ThisActivity");
        }
    }
}