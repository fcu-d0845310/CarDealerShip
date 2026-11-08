using CarDealerShip.Dtos.Branch;
using CarDealerShip.ViewModels;

namespace CarDealerShip.Mappings
{
    //分店功能的 DTO 與 ViewModel 轉換
    public static class BranchMappings
    {
        //列表的一筆資料
        public static BranchListItemViewModel ToViewModel(this BranchListItemDto dto)
        {
            return new BranchListItemViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Address = dto.Address,
                Phone = dto.Phone,
                InstockCount = dto.InstockCount
            };
        }

        //把分店資料轉成編輯頁的表單
        public static BranchFormViewModel ToFormViewModel(this BranchDetailDto dto)
        {
            return new BranchFormViewModel
            {
                Id = dto.Id,
                Name = dto.Name,
                Address = dto.Address,
                Phone = dto.Phone
            };
        }

        //把表單轉成要寫入的資料，新增與編輯共用
        public static BranchSaveDto ToSaveDto(this BranchFormViewModel vm)
        {
            return new BranchSaveDto
            {
                Name = vm.Name,
                Address = vm.Address,
                Phone = vm.Phone
            };
        }
    }
}
