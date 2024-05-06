using Content.Models;
using System.Threading.Tasks;

namespace DropDown
{
    public interface IPageSectionText
    {
        Task<SectionText> Get(string ID);
        string GetURL();
    }
}