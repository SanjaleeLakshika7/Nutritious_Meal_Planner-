using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// Helps to generate drop down lists (select lists)

namespace DropDown
{
    public class CommonDDL : ICommonDDL
    {
        public SelectList ActiveStatusList()
        {
            var objList = new List<_DDLItem>();

            objList.Add(new _DDLItem("A", "Active"));
            objList.Add(new _DDLItem("I", "Inactive"));

            return new SelectList(objList, "Value", "Text");
        }

        public SelectList StockAvailableList()
        {
            var objList = new List<_DDLItem>();

            objList.Add(new _DDLItem("A", "Available"));
            objList.Add(new _DDLItem("N", "Out of Stock"));

            return new SelectList(objList, "Value", "Text");
        }

        public SelectList TaxTypeList()
        {
            var objList = new List<_DDLItem>();

            objList.Add(new _DDLItem("INC", "Included"));
            objList.Add(new _DDLItem("EXC", "Excluded"));

            return new SelectList(objList, "Value", "Text");
        }

        public SelectList UOMCategoryList()
        {
            var objList = new List<_DDLItem>();

            objList.Add(new _DDLItem("WGT", "Weight"));
            objList.Add(new _DDLItem("LEN", "Length"));
            objList.Add(new _DDLItem("VOL", "Volume"));
            objList.Add(new _DDLItem("PCK", "Pack"));

            return new SelectList(objList, "Value", "Text");
        }

        public SelectList SalutationList()
        {
            var objList = new List<_DDLItem>();

            objList.Add(new _DDLItem("Mr.", "Mr."));
            objList.Add(new _DDLItem("Mrs.", "Mrs."));
            objList.Add(new _DDLItem("Miss.", "Miss."));
            objList.Add(new _DDLItem("Ms.", "Ms."));
            objList.Add(new _DDLItem("M/s.", "M/s."));
            objList.Add(new _DDLItem("Dr.", "Dr."));
            objList.Add(new _DDLItem("Prof.", "Prof."));
            objList.Add(new _DDLItem("Rev.", "Rev."));

            return new SelectList(objList, "Value", "Text");
        }

        public SelectList CustomerTypeList()
        {
            var objList = new List<_DDLItem>();

            objList.Add(new _DDLItem("CASH", "Cash"));
            objList.Add(new _DDLItem("CREDIT", "Credit"));

            return new SelectList(objList, "Value", "Text");
        }

        public SelectList PriceTypeList()
        {
            var objList = new List<_DDLItem>();

            objList.Add(new _DDLItem("RT", "Retail"));
            objList.Add(new _DDLItem("WH", "Wholesale"));

            return new SelectList(objList, "Value", "Text");
        }

        public SelectList PromotionTypeList()
        {
            var objList = new List<_DDLItem>();

            objList.Add(new _DDLItem("BLDR", "Bill Discount Rate"));
            objList.Add(new _DDLItem("BLDA", "Bill Discount Amount"));
            objList.Add(new _DDLItem("GRDR", "Group Discount Rate"));
            objList.Add(new _DDLItem("BXGY", "Buy X Get Y Free"));

            return new SelectList(objList, "Value", "Text");
        }

        public SelectList OrderStatusList()
        {
            var objList = new List<_DDLItem>();

            objList.Add(new _DDLItem("P", "Pending"));
            objList.Add(new _DDLItem("A", "In-Progress"));
            objList.Add(new _DDLItem("D", "On Delivery"));
            objList.Add(new _DDLItem("C", "Completed"));
            objList.Add(new _DDLItem("R", "Rejected"));
            objList.Add(new _DDLItem("N", "Cancelled"));

            return new SelectList(objList, "Value", "Text");
        }

        public SelectList PaidStatusList()
        {
            var objList = new List<_DDLItem>();

            objList.Add(new _DDLItem("PD", "Paid"));
            objList.Add(new _DDLItem("NP", "Not Paid"));
          


            return new SelectList(objList, "Value", "Text");
        }
        public  string GetWebURL()
        {
            return SettingHelper.AppSetting("Directory:WebURL");
        }
    }
}

