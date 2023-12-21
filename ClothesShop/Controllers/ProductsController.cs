using ClothesShop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using PagedList;
using ClothesShop.Models.CommentView;

namespace ClothesShop.Controllers
{
    public class ProductsController : Controller
    {
        ClothesShopEntities db = new ClothesShopEntities();
        // GET: Products
        public ActionResult Index(int? page)
        {

            int iSize = 9;
            int iPageNum = (page ?? 1);
         
            var dac = from d in db.Products select d;
            return View(dac.OrderBy(s => s.ProductId).ToPagedList(iPageNum, iSize));
        }
        public ActionResult AoThun()
        {
            int c = 1;
            var dc = from s in db.Categories
                     join p in db.Products on s.CategoryId equals p.CategoryId
                     where p.CategoryId == c
                     select p;
            return View(dc);

        }
        public ActionResult AoSoMi()
        {
            int c = 2;
            var dan = from s in db.Categories
                      join p in db.Products on s.CategoryId equals p.CategoryId
                      where p.CategoryId == c
                      select p;
            return View(dan);

        }
        public ActionResult Dam()
        {
            int c = 3;
            var du = from s in db.Categories
                     join p in db.Products on s.CategoryId equals p.CategoryId
                     where p.CategoryId == c
                     select p;
            return View(du);

        }
        public ActionResult Quan()
        {
            int c = 4;
            var l = from s in db.Categories
                    join p in db.Products on s.CategoryId equals p.CategoryId
                    where p.CategoryId == c
                    select p;
            return View(l);

        }
        public ActionResult DoBoi()
        {
            int c = 5;
            var hs = from s in db.Categories
                     join p in db.Products on s.CategoryId equals p.CategoryId
                     where p.CategoryId == c
                     select p;
            return View(hs);

        }
        public ActionResult AoKhoac()
        {
            int c = 6;
            var hq = from s in db.Categories
                     join p in db.Products on s.CategoryId equals p.CategoryId
                     where p.CategoryId == c
                     select p;
            return View(hq);

        }


        public ActionResult ChiTIetSanPham(int id)
        {
            var ctsp = from s in db.Products where s.ProductId == id select s;

            var rate1 = (from s in db.Comments where s.ProductId == id && s.Rate == 1 select s).Count();
            var rate2 = (from s in db.Comments where s.ProductId == id && s.Rate == 2 select s).Count();
            var rate3 = (from s in db.Comments where s.ProductId == id && s.Rate == 3 select s).Count();
            var rate4 = (from s in db.Comments where s.ProductId == id && s.Rate == 4 select s).Count();
            var rate5 = (from s in db.Comments where s.ProductId == id && s.Rate == 5 select s).Count();
            ViewBag.Rate1 = rate1;
            ViewBag.Rate2 = rate2;
            ViewBag.Rate3 = rate3;
            ViewBag.Rate4 = rate4;
            ViewBag.Rate5 = rate5;


            ViewBag.ListComment = new CommentDAO().ListCommentViewModel(0, id);

            return View(ctsp);
        }

        [ChildActionOnly]
        public ActionResult _ChildComment(int parentid, int productid)
        {
            var data = new CommentDAO().ListCommentViewModel(parentid, productid);
            var sessionUser = (Customer)Session["cmt"];


            return PartialView("_ChildComment", data);
        }


        public JsonResult AddNewComment(string commentmsg, int productid, string username,  int parentid, string rate, int customerid)
        {

            using (ClothesShopEntities db = new ClothesShopEntities()) // Thay "YourDbContext" bằng tên thực tế của DbContext của bạn
            {
                Comment comment = new Comment
                {
                    CommentMsg = commentmsg,
                    CommentDate = DateTime.Now,

                    ProductId = productid,
                    UserName = username,

                    ParentID = parentid,
                    Rate = Convert.ToInt32(rate),
                    CustomerId = customerid,

                };

                db.Comments.Add(comment);
                db.SaveChanges();
                return Json(new { status = true }, JsonRequestBehavior.AllowGet);
            }
            //    try
            //    {
            //        var dao = new CommentDAO();
            //        Comment comment = new Comment();





            //        comment.CommentMsg = commentmsg;
            //        comment.ProductId = productid;
            //        comment.CustomerId = customerid;
            //        comment.ParentID = parentid;
            //        comment.Rate = Convert.ToInt32(rate);
            //        comment.UserName = username;
            //        comment.CommentDate = DateTime.Now;

            //    if (addcomment == true)
            //    {

            //        return Json(new { status = true }, JsonRequestBehavior.AllowGet);
            //    }
            //    else
            //    {
            //        return Json(new { status = false }, JsonRequestBehavior.AllowGet);
            //    }
            //}
            //    catch
            //    {
            //        return Json(new { status = true }, JsonRequestBehavior.AllowGet);
            //    }

        }

        public ActionResult GetComment(int productid)
        {
            var data = new CommentDAO().ListCommentViewModel(0, productid);
            
            return PartialView("_ChildComment", data);
        }
    }


}
