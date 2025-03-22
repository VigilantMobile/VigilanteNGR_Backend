using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.AlertCategories
{
    public class SecurityTipCategoryTypeWithCategoriesViewModel
    {
        public List<CategoryTypeWithCategoriesDto> AlertCategoryTypes { get; set; } = new();
    }

    public class CategoryTypeWithCategoriesDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<CategoryDto> Categories { get; set; } = new();
    }

    public class CategoryDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
