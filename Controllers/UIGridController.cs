using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Responsive_UI_Grid.Models;
using System.Data.SqlClient;
using System.Configuration;

namespace Responsive_UI_Grid.Controllers
{
    public class UIGridController : Controller
    {
        // GET: UIGrid
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult bindUiGrid()
        {
            using (SqlConnection cn=new SqlConnection(ConfigurationManager.ConnectionStrings["UIGridDataEntities"].ConnectionString))
            {
                try
                {
                    List<GridDataModel> _gridDataModelList = new List<GridDataModel>();
                    using (SqlCommand cmd=new SqlCommand())
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.CommandText = "GridData";
                        cn.Open();
                        cmd.Connection = cn;
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            GridDataModel _gridDataObj = new GridDataModel();
                            _gridDataObj.Id = Convert.ToInt32(dr["Id"]);
                            _gridDataObj.age = Convert.ToString(dr["age"]);
                            _gridDataObj.Name = Convert.ToString(dr["Name"]);
                            _gridDataObj.gender = Convert.ToString(dr["gender"]);
                            _gridDataObj.company = Convert.ToString(dr["company"]);
                            _gridDataModelList.Add(_gridDataObj);

                        }
                    }
                    return Json(_gridDataModelList, JsonRequestBehavior.AllowGet);
                }
                catch (Exception e)
                {

                    return null;
                }
            }


        }
    }
}
