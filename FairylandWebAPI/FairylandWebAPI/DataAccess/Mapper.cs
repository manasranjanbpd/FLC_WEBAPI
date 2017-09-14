using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FairylandWebAPI.DataAccess
{
    public class Mapper
    {
        public static T BindData<T>(DataTable dt)
        {
            DataRow dr = dt.Rows[0];

            // Get all columns' name
            List<string> columns = new List<string>();
            foreach (DataColumn dc in dt.Columns)
            {
                columns.Add(dc.ColumnName.ToLowerInvariant());
            }

            // Create object
            var ob = Activator.CreateInstance<T>();

            // Get all fields
            var fields = typeof(T).GetFields();
            foreach (var fieldInfo in fields)
            {
                if (columns.Contains(fieldInfo.Name.ToLowerInvariant()))
                {
                    // Fill the data into the field
                    fieldInfo.SetValue(ob, dr[fieldInfo.Name]);
                }
            }

            // Get all properties
            var properties = typeof(T).GetProperties();
            foreach (var propertyInfo in properties)
            {
                if (columns.Contains(propertyInfo.Name.ToLowerInvariant()))
                {
                    if (propertyInfo.GetMethod.ReturnParameter.ToString().Trim() == "Int32")
                    {
                        propertyInfo.SetValue(ob, Convert.ToInt32(dr[propertyInfo.Name]));
                    }
                    else
                    {
                        propertyInfo.SetValue(ob, dr[propertyInfo.Name]);
                    }
                }
            }

            return ob;
        }
        public static List<T> BindDataList<T>(DataTable dt)
        {
            List<string> columns = new List<string>();
            foreach (DataColumn dc in dt.Columns)
            {
                columns.Add(dc.ColumnName.ToLowerInvariant());
            }

            var fields = typeof(T).GetFields();
            var properties = typeof(T).GetProperties();

            List<T> lst = new List<T>();

            foreach (DataRow dr in dt.Rows)
            {
                var ob = Activator.CreateInstance<T>();

                foreach (var fieldInfo in fields)
                {
                    if (columns.Contains(fieldInfo.Name.ToLowerInvariant()))
                    {
                        fieldInfo.SetValue(ob, dr[fieldInfo.Name]);
                    }
                }

                foreach (var propertyInfo in properties)
                {
                    if (columns.Contains(propertyInfo.Name.ToLowerInvariant()))
                    {
                        if (propertyInfo.GetMethod.ReturnParameter.ToString().Trim() == "Int32") 
                        {
                            propertyInfo.SetValue(ob, Convert.ToInt32(dr[propertyInfo.Name]));
                        }
                        else
                        {
                            propertyInfo.SetValue(ob, dr[propertyInfo.Name]);
                        }
                    }
                }

                lst.Add(ob);
            }

            return lst;
        }
    }
}