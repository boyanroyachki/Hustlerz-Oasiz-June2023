using HustlerzOasiz.Web.ViewModels.Category;
using MarauderzOasiz.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HustlerzOasiz.Services.Data.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<ChooseACategoryFormModel>> GetCategoriesAsync(); //
        Task<bool> ExistsByIdAsync(int id); //
    }
}
