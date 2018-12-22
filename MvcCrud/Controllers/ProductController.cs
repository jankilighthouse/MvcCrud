using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using MvcCrud.Models;

namespace MvcCrud.Controllers
{
    public class ProductController : Controller
    {
        string connectionstring = @"Data Source = ANKITPATEL-PC\SQLEXPRESS; Initial catalog = MvcCrud; Integrated Security=True";

        // GET: Product
        [HttpGet]
        public ActionResult Index()
        {
            DataTable dtbProduct = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionstring))
            {
                sqlCon.Open();
                SqlDataAdapter sqlDa = new SqlDataAdapter("SELECT* FROM Product", sqlCon);
                sqlDa.Fill(dtbProduct);
            }

                return View(dtbProduct);
        }
        [HttpGet]
        // GET: Product/Create
        public ActionResult Create()
        {
            return View(new ProductModel());
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(ProductModel productModel)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionstring))
            {
                sqlCon.Open();
                string query = "INSERT INTO Product VALUES(@ProductName,@Price,@Count)";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@ProductName", productModel.ProductName);
                sqlCmd.Parameters.AddWithValue("@Price", productModel.Price);
                sqlCmd.Parameters.AddWithValue("@Count", productModel.Count);
                sqlCmd.ExecuteNonQuery();
            }
                return RedirectToAction("Index");
        }

        // GET: Product/Edit/5

        public ActionResult Edit(int id)
        {
            ProductModel productModel = new ProductModel();
            DataTable dtbProduct = new DataTable();
            using (SqlConnection sqlCon = new SqlConnection(connectionstring))
            {
                sqlCon.Open();
                string query = "SELECT * FROM Product Where ProductID= @ProductID";
                SqlDataAdapter sqlda = new SqlDataAdapter(query, sqlCon);
                sqlda.SelectCommand.Parameters.AddWithValue("@ProductID",id);
                sqlda.Fill(dtbProduct);

            }
            if (dtbProduct.Rows.Count == 1)
            {
                productModel.ProductID = Convert.ToInt32(dtbProduct.Rows[0][0].ToString());
                productModel.ProductName = dtbProduct.Rows[0][1].ToString();
                productModel.Price = Convert.ToDecimal(dtbProduct.Rows[0][2].ToString());
                productModel.Count = Convert.ToInt32(dtbProduct.Rows[0][3].ToString());
                return View(productModel);
            }
            else
                return RedirectToAction("Index");


            }
           

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(ProductModel productModel)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionstring))
            {
                sqlCon.Open();
                string query = "Update Product SET ProductName = @ProductName, Price = @Price, Count = @Count Where ProductID = @ProductID";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@ProductID", productModel.ProductID);
                sqlCmd.Parameters.AddWithValue("@ProductName", productModel.ProductName);
                sqlCmd.Parameters.AddWithValue("@Price", productModel.Price);
                sqlCmd.Parameters.AddWithValue("@Count", productModel.Count);
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");

        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            using (SqlConnection sqlCon = new SqlConnection(connectionstring))
            {
                sqlCon.Open();
                string query = "DELETE FROM Product Where ProductID = @ProductID";
                SqlCommand sqlCmd = new SqlCommand(query, sqlCon);
                sqlCmd.Parameters.AddWithValue("@ProductID",id);
                
                sqlCmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        
    }
}
