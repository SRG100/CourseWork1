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
        //to add tags
        Task AddTagAsync(Tags tag);
        //to get tags
        Task<List<Tags>> GetTagsAsync();
    }
}
