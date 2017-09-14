using FairylandWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace FairylandWebAPI.DataAccess
{
    public class ProductInfo
    {
        private static string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["localDB"].ConnectionString;
        }

        internal List<Product> GetAllProducts()
        {
            DataTable allProduct = new DataTable();
            using (SqlConnection myConnection = new SqlConnection(GetConnectionString()))
            {
                myConnection.Open();
                using (SqlCommand cmd = new SqlCommand("sp_GetAllProducts", myConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (SqlDataAdapter a = new SqlDataAdapter(cmd))
                    {
                        a.Fill(allProduct);
                    }
                }
                myConnection.Close();
            }
            return Mapper.BindDataList<Product>(allProduct);

        }

        internal string SaveProduct(Product item)
        {
            int id=0;
            using (SqlConnection myConnection = new SqlConnection(GetConnectionString()))
            {
                myConnection.Open();
                using (SqlCommand cmd = new SqlCommand("sp_AddProduct", myConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter returnParameter = cmd.Parameters.Add("@result", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@productCode",SqlDbType.VarChar).Value = item.ProductCode;
                    cmd.Parameters.Add("@productName", SqlDbType.VarChar).Value = item.ProductName ?? "";
                    cmd.Parameters.Add("@keywords", SqlDbType.VarChar).Value = item.Keywords ?? "";
                    cmd.Parameters.Add("@shortDescription", SqlDbType.VarChar).Value = item.ShortDescription ?? "";
                    cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = item.LongDescription ?? "";
                    cmd.Parameters.Add("@isActive", SqlDbType.Bit).Value = item.IsActive;
                    cmd.Parameters.Add("@unitCost", SqlDbType.VarChar).Value = item.UnitCost.ToString();
                    cmd.Parameters.Add("@unitPrice", SqlDbType.VarChar).Value = item.UnitPrice.ToString();
                    cmd.Parameters.Add("@categoryId",SqlDbType.Int).Value =item.CategoryId;
                    cmd.Parameters.Add("@lastModifiedBy", SqlDbType.VarChar).Value = item.LastModifiedBy ?? "";
                    cmd.Parameters.Add("@lastModified", SqlDbType.VarChar).Value = item.LastModified ?? "";

                    cmd.ExecuteNonQuery();
                    id = (int)returnParameter.Value;
                }
                myConnection.Close();
            }
            return id == 1 ? "true" : "false";
        }

        internal Product GetProductById(int productId)
        {
            DataTable product = new DataTable();
            using (SqlConnection myConnection = new SqlConnection(GetConnectionString()))
            {
                myConnection.Open();
                using (SqlCommand cmd = new SqlCommand("sp_GetProductById", myConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@productId", SqlDbType.Int).Value = productId;
                    using (SqlDataAdapter a = new SqlDataAdapter(cmd))
                    {
                        a.Fill(product);
                    }
                }
                myConnection.Close();
            }
            return Mapper.BindData<Product>(product);
        }


        internal string DeleteProductById(Product productData)
        {
            int id = 0;
            using (SqlConnection myConnection = new SqlConnection(GetConnectionString()))
            {
                myConnection.Open();
                using (SqlCommand cmd = new SqlCommand("sp_DeleteProduct", myConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter returnParameter = cmd.Parameters.Add("@result", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@productId", SqlDbType.VarChar).Value = productData.ProductId;

                    cmd.ExecuteNonQuery();
                    id = (int)returnParameter.Value;
                }
                myConnection.Close();
            }
            return id == 1 ? "true" : "false";
        }

        internal string UpdateProduct(Product item)
        {
            int id = 0;
            using (SqlConnection myConnection = new SqlConnection(GetConnectionString()))
            {
                myConnection.Open();
                using (SqlCommand cmd = new SqlCommand("sp_UpdateProduct", myConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter returnParameter = cmd.Parameters.Add("@result", SqlDbType.Int);
                    returnParameter.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("@productCode", SqlDbType.VarChar).Value = item.ProductCode;
                    cmd.Parameters.Add("@productName", SqlDbType.VarChar).Value = item.ProductName ?? "";
                    cmd.Parameters.Add("@keywords", SqlDbType.VarChar).Value = item.Keywords ?? "";
                    cmd.Parameters.Add("@shortDescription", SqlDbType.VarChar).Value = item.ShortDescription ?? "";
                    cmd.Parameters.Add("@description", SqlDbType.VarChar).Value = item.LongDescription ?? "";
                    cmd.Parameters.Add("@isActive", SqlDbType.Bit).Value = item.IsActive;
                    cmd.Parameters.Add("@unitCost", SqlDbType.VarChar).Value = item.UnitCost.ToString();
                    cmd.Parameters.Add("@unitPrice", SqlDbType.VarChar).Value = item.UnitPrice.ToString();
                    cmd.Parameters.Add("@categoryId", SqlDbType.Int).Value = item.CategoryId;
                    cmd.Parameters.Add("@lastModifiedBy", SqlDbType.VarChar).Value = item.LastModifiedBy ?? "";
                    cmd.Parameters.Add("@lastModified", SqlDbType.VarChar).Value = item.LastModified ?? "";
                    cmd.Parameters.Add("@productId", SqlDbType.Int).Value = item.ProductId;
                    cmd.ExecuteNonQuery();
                    id = (int)returnParameter.Value;
                }
                myConnection.Close();
            }
            return id == 1 ? "true" : "false";
        }
    }
}