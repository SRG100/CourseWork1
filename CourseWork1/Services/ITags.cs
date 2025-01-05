using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseWork1.Models;

namespace CourseWork1.Services
{
    public interface ITags
    {
        Task AddTagAsync(Tags tag);
        Task<List<Tags>> GetTagsAsync();
    }
}
