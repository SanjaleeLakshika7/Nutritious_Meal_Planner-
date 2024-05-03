﻿using Microsoft.AspNetCore.Mvc.Rendering;

namespace DropDown
{
    public interface ICommonDDL
    {
        SelectList ActiveStatusList();
        SelectList CustomerTypeList();
        SelectList OrderStatusList();
        SelectList PriceTypeList();
        SelectList PromotionTypeList();
        SelectList SalutationList();
        SelectList SortOrderList();
        SelectList StockAvailableList();
        SelectList TaxTypeList();
        SelectList UOMCategoryList();
        string CheckFile(string ImageId);
        SelectList ScheduleTypeList();
    }
}