using CarDealerShip.Dtos.Common;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarDealerShip.Mappings
{
    //各功能共用的 DTO 與 ViewModel 轉換
    public static class CommonMappings
    {
        //把選項清單轉成畫面下拉選單使用的 SelectListItem
        public static List<SelectListItem> ToSelectList(this IEnumerable<OptionDto> options)
        {
            return options
                .Select(o => new SelectListItem { Text = o.Name, Value = o.Id.ToString() })
                .ToList();
        }
    }
}
