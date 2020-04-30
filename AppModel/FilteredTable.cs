﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace AppBoxPro
{
    public class FilteredTable
    {
        public delegate bool FilterDataRowItemDelegate(object sourceObj, string fillteredOperator, JToken fillteredObj, string column);
        public FilterDataRowItemDelegate FilterDataRowItem
        {
            get;
            set;
        }

        // 表格过滤
        public DataTable GetFilteredTable(JArray filteredData)
        {
            // 示例使用的模拟数据是一次性返回的，因此我们需要新建一个 DataTable 来过滤返回的数据
            // 注：实际应用环境请不要这么做！！！（可以将过滤条件直接用于数据库检索）
            DataTable source = null;// DataSourceUtil.GetDataTable();

            DataTable result = source.Clone();

            foreach (DataRow row in source.Rows)
            {
                bool filtered = true;
                foreach (JObject filteredObj in filteredData)
                {
                    if (!CheckDataRow(row, filteredObj))
                    {
                        filtered = false;
                        break;
                    }
                }

                if (filtered)
                {
                    result.Rows.Add(row.ItemArray);
                }
            }

            return result;
        }


        private bool CheckDataRow(DataRow row, JObject filteredObj)
        {
            // 下面约定 ColumnID 和数据库的字段名称一样
            // 如果你想获取 DataField 属性，可以通过如下代码：filteredObj.Value<string>("f_dataField");
            string columnID = filteredObj.Value<string>("column");
            object rowitemData = row[columnID];

            bool multi = filteredObj.Value<bool>("multi");
            if (multi)
            {
                string matcher = filteredObj.Value<string>("matcher");
                JArray items = filteredObj.Value<JArray>("items");

                bool valid = false;
                if (matcher == "all")
                {
                    valid = true;
                }
                foreach (JObject item in items)
                {
                    string itemOperator = item.Value<string>("operator");
                    JToken itemValue = item.Value<JToken>("value");

                    if (FilterDataRowItem(rowitemData, itemOperator, itemValue, columnID))
                    {
                        if (matcher == "any")
                        {
                            valid = true;
                            break;
                        }
                    }
                    else
                    {
                        if (matcher == "all")
                        {
                            valid = false;
                            break;
                        }
                    }
                }

                return valid;
            }
            else
            {
                JObject item = filteredObj.Value<JObject>("item");
                string itemOperator = item.Value<string>("operator");
                JToken itemValue = item.Value<JToken>("value");

                return FilterDataRowItem(rowitemData, itemOperator, itemValue, columnID);
            }
        }



    }
}