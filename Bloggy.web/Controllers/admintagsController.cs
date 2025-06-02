using Bloggy.web.Data;
using Bloggy.web.Models.Domain;
using Bloggy.web.Models.ViewModels;
using Bloggy.web.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bloggy.web.Controllers
{
    public class admintagsController : Controller
    {
        private readonly ITagRepository tagRepository;

        public admintagsController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddTagRequest a)
        {

            var tag = new Tag
            {
                Name = a.name,
                Displayname = a.displayname

            };
            await tagRepository.AddAsync(tag);
            return RedirectToAction("List");
        }
        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            var t = await tagRepository.GetAllAsync();

            return View(t);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var tag = await tagRepository.GetAsync(id);
            if (tag!=null)
            {
                var vm = new EditTagRequest
                {
                    Id = tag.Id,
                    Name = tag.Name,
                    Displayname = tag.Displayname
                };
                return View(vm);
            }

            if (tag == null)
            {
                return View(null);
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest e)
        {
            var tag = new Tag
            {
                Id = e.Id,
                Name = e.Name,
                Displayname = e.Displayname
            };
            var updatetag = await  tagRepository.UpdateAsync(tag);
            if (updatetag != null)
            {
                return RedirectToAction("List");
            }
            else
            {

            }
            return RedirectToAction("Edit", new { id = e.Id });
        }
        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest E)
        {
            var tag = await tagRepository.DeleteAsync(E.Id);
            if (tag != null)
            {
                return RedirectToAction("List");
            }
            else
            {
                //notifications
            }
                return RedirectToAction("Edit", new { id = E.Id });
        }
    }
}